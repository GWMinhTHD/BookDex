using StoreManagement.Data;
using StoreManagement.Interfaces;
using StoreManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Repositories
{
    public class CartRepository : ICart
    {
        private readonly AppDBContext _context;
        public CartRepository(AppDBContext context)
        {
            _context = context;
        }
        public List<Cart> GetCartByUser(string userId)
        {
            return _context.Cart.Include("Book").Where(c => c.UserID == userId).ToList();
        }

        public void AddBookToCart(Cart cart)
        {
            Cart? oldCart = _context.Cart.FirstOrDefault(c => c.UserID == cart.UserID && c.BookID == cart.BookID);
            if (oldCart == null)
            {
                _context.Cart.Add(cart);
            }
            else
            {
                
            }
        }

        public Cart GetById(int? id)
        {
            Cart query = _context.Cart.FirstOrDefault(c=> c.Id == id);
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
    }
}
