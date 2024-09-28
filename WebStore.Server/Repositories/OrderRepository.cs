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

        public async Task<Order> CheckOwnership(ApplicationUser user, int id)
        {
            return await _context.Order.FirstOrDefaultAsync(c => c.UserId == user.Id && c.Id == id);
        }

        public void CreateOrder(Order order)
        {
            _context.Order.Add(order);
        }

        public List<Order> GetAll()
        {
            return _context.Order.ToList();
        }

        public async Task<IEnumerable<OrderBook>> GetById(int? id)
        {
            return await _context.OrderBook.Where(o => o.OrderId == id).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOfUser(ApplicationUser user)
        {
            return await _context.Order.Where(o => o.UserId == user.Id).ToListAsync();
        }

        public void Update(Order order)
        {
            _context.Order.Update(order);
        }

    }
}
