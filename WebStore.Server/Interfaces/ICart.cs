using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface ICart
    {
        Task<List<Cart>> GetUserCart(ApplicationUser user);

        Task AddToCart(string userId, int? bookId);
        Task<Cart> GetById(int? id);
        Task<Cart> CheckCart(ApplicationUser user, int bookId);
        void Delete(Cart cart);
        public int GetNumbersOfItems(string userId);
   
    }
}
