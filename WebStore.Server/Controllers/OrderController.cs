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
    public class OrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Get()
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            var getOrders = await _unitOfWork.Order.GetOfUser(user);
            var userOrder = new List<OrderDTO>();
            foreach (var order in getOrders)
            {
                var orderDTO = new OrderDTO();
                orderDTO.Id = order.Id;
                orderDTO.Total = order.Total;
                userOrder.Add(orderDTO);
            }
            return Ok(userOrder);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetail(int id)
        {
            var username = User.GetUserName();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var checkLib = await _unitOfWork.Order.CheckOwnership(user, id);
            if (checkLib == null)
            {
                return BadRequest("Order not exist or not from this user");
            }
            var dto = new OrderDetailDTO();
            dto.Total = 0;
            dto.orderItems = new List<OrderItemDTO>();
            var getItems = await _unitOfWork.Order.GetById(id);
            foreach (var item in getItems)
            {
                var itemDTO = new OrderItemDTO();
                itemDTO.BookName = item.BookName;
                itemDTO.BookPrice = item.BookPrice;
                dto.Total = dto.Total + item.BookPrice;
                dto.orderItems.Add(itemDTO);
            }
            return Ok(dto);
        }
    }
}
