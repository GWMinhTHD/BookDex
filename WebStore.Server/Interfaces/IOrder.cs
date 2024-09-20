using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface IOrder
    {
        void CreateOrder (Order order);
        void Update(Order order);
        List<Order> GetOfUser(string currentUser);
        //List<Order> GetAll();
        Order GetById(int? id);


    }
}
