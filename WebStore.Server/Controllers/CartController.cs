using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Extensions;
using WebStore.Server.Interfaces;
using WebStore.Server.Models.DTOs;
using WebStore.Server.Models;

namespace WebStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetUserCart()
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            var getCart = await _unitOfWork.Cart.GetUserCart(user);
            var userCart = new CartListDTO();
            userCart.Cart = new List<CartDTO>();
            foreach (var cart in getCart)
            {
                var dto = new CartDTO();
                Book book =  await _unitOfWork.Book.GetById(cart.BookID);
                var bookauthors = new List<string>();
                foreach (var author in book.BookAuthors)
                {
                    bookauthors.Add(author.Author.Alias);
                }
                dto.Id = cart.Id;
                dto.BookName = book.Name;
                dto.BookCover = book.Cover;
                dto.BookPrice = book.Price;
                dto.BookAuthors = bookauthors;
                userCart.Cart.Add(dto);
                userCart.Total = userCart.Total + book.Price;
            }
            return Ok(userCart);
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult> AddToCart(int? id)
        {
            Book book = await _unitOfWork.Book.GetById(id);
            if (book == null) {
                return NotFound();
            }
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var checkCart = await _unitOfWork.Cart.CheckCart(user, book.Id);
            if (checkCart != null)
            {
                return BadRequest("Item already in Cart");
            }
            var checkLib = await _unitOfWork.Library.CheckOwnership(user, book.Id);
            if (checkLib != null)
            {
                return BadRequest("Book Owned");
            }
            await _unitOfWork.Cart.AddToCart(user.Id, book.Id);
            await _unitOfWork.Save();
            return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> RemoveFromCart(int? id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest("Invalid cart item remove request");
            }
            Cart cart = await _unitOfWork.Cart.GetById(id);
            if (cart == null)
            {
                return NotFound("Cart item not exist");
            }
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if ((user == null) || (cart.UserID != user.Id))
            {
                return Unauthorized();
            }
            _unitOfWork.Cart.Delete(cart);
            await _unitOfWork.Save();
            return Ok();
        }
    }
}
