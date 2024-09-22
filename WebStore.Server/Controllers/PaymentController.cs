using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebStore.Server.Extensions;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;
using WebStore.Server.Models.DTOs;

namespace WebStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(IPaymentService paymentService, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [HttpPost("create-payment-intent")]
        [Authorize]
        public async Task<ActionResult> CreatePaymentIntent([FromBody] PaymentIntentRequest request)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            var getCart = await _unitOfWork.Cart.GetUserCart(user);
            double total = 0;
            foreach (var cart in getCart)
            {
                Book book = await _unitOfWork.Book.GetById(cart.BookID);
                total =  total + book.Price;
            }
            total = total * 100;
            long amount = Convert.ToInt64(total);
            var paymentIntent = _paymentService.CreatePaymentIntent(amount, request.Currency, request.PaymentMethodTypes);
            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }

        [HttpPost("check-payment-confirm")]
        [Authorize]
        public async Task<ActionResult> ConfirmPaymentStatus([FromBody] ClientKeyDTO dto)
        {
            PaymentIntent payment = _paymentService.GetPaymentIntent(dto.Key);
            if (payment.Status == "succeeded")
            {
                var username = User.GetUserName();
                var user = await _userManager.FindByNameAsync(username);
                var getCart = await _unitOfWork.Cart.GetUserCart(user);
                Order order = new Order();
                order.UserId = user.Id;
                order.Total = 0;
                order.OrderBooks = new List<OrderBook>();
                foreach (var cart in getCart)
                {
                    await _unitOfWork.Library.Insert(cart.UserID, cart.BookID.GetValueOrDefault());
                    Book book = await _unitOfWork.Book.GetById(cart.BookID);
                    order.Total = order.Total + book.Price;
                    OrderBook orderBook = new OrderBook();
                    orderBook.BookId = book.Id;
                    orderBook.BookName = book.Name;
                    orderBook.BookPrice = book.Price;
                    order.OrderBooks.Add(orderBook);
                    _unitOfWork.Cart.Delete(cart);
                }
                _unitOfWork.Order.CreateOrder(order);
                await _unitOfWork.Save();
            }
            return Ok();
        }
    }
}
