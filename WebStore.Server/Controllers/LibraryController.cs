using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Server.Extensions;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using static System.Reflection.Metadata.BlobBuilder;
using WebStore.Server.Models.DTOs;

namespace WebStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public LibraryController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            var getBooks = await _unitOfWork.Library.GetUserBooks(user);
            var userLib = new List<BookDTO>();
            foreach (var book in getBooks)
            {
                var bookDTO = new BookDTO();
                bookDTO.Id = book.Id;
                bookDTO.Name = book.Name;
                bookDTO.Cover = book.Cover;
                foreach (var category in book.BookCategories)
                {
                    bookDTO.CategoryIDs.Add(category.Category.Id);
                    bookDTO.Categories.Add(category.Category.Name);
                }
                foreach (var author in book.BookAuthors)
                {
                    bookDTO.AuthorIDs.Add(author.Author.Id);
                    bookDTO.Authors.Add(author.Author.Alias);
                }
                userLib.Add(bookDTO);
            }
            return Ok(userLib);
        }

        [HttpGet("reader/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Book>>> GetUserBook(int id)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var checkLib = await _unitOfWork.Library.CheckOwnership(user, id);
            if (checkLib == null)
            {
                return BadRequest("Book not exist or not owned");
            }
            var book = await _unitOfWork.Book.GetById(id);
            var bookDto = new BookDTO();
            bookDto.Id = book.Id;
            bookDto.Name = book.Name;
            bookDto.FileLocation = book.FileLocation;
            return Ok(bookDto);
        }
    }
}
