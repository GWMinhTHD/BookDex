using Microsoft.EntityFrameworkCore;
using WebStore.Server.Data;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;

namespace WebStore.Server.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateOrder(Order order)
        {
            _context.Order.Add(order);
        }

        public List<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public Order GetById(int? id)
        {
            return _context.Order.Include("OrderBooks").Include("User").FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetOfUser(string currentUserID)
        {
            return _context.Order.Where(o => o.UserId == currentUserID).ToList();
        }

        public void Update(Order order)
        {
            _context.Order.Update(order);
        }

    }
}
