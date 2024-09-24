using StoreManagement.Interfaces;
using StoreManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Manager.Controllers
{

    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Order> orders = _unitOfWork.Order.GetAll();
            return View(orders);
        }

        public IActionResult Detail(int? id)
        {
            Order order = _unitOfWork.Order.GetById(id);
            return View(order);
        }
    }
}

