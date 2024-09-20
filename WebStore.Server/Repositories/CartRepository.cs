using Microsoft.EntityFrameworkCore;
using WebStore.Server.Data;
using WebStore.Server.Interfaces;
using WebStore.Server.Models;

namespace WebStore.Server.Repositories
{
    public class CartRepository : ICart
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Cart>> GetUserCart(ApplicationUser user)
        {
            return await _context.Cart.Where(u => u.UserID == user.Id).ToListAsync();
        }

        public async Task AddToCart(string userId, int? bookId)
        {
            Cart cart = new Cart();
            cart.UserID = userId;
            cart.BookID = bookId;
            await _context.Cart.AddAsync(cart);
        }

        public async Task<Cart> GetById(int? id)
        {
            Cart query = await _context.Cart.FirstOrDefaultAsync(c=> c.Id == id);
            return query;
        }

        public void Delete(Cart cart)
        {
            _context.Cart.Remove(cart);
        }

        public int GetNumbersOfItems(string userId)
        {
            int count = _context.Cart.Count(c => c.UserID == userId);
            return count;
        }

        public async Task<Cart> CheckCart(ApplicationUser user, int bookId)
        {
            return await _context.Cart.FirstOrDefaultAsync(c => c.UserID == user.Id && c.BookID == bookId);
        }
    }
}
