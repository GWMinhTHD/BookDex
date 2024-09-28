using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface IOrder
    {
        void CreateOrder (Order order);
        void Update(Order order);
        Task<IEnumerable<Order>> GetOfUser(ApplicationUser user);
        Task<Order> CheckOwnership(ApplicationUser user, int id);
        //List<Order> GetAll();
        Task<IEnumerable<OrderBook>> GetById(int? id);

    }
}
