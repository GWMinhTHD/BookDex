using StoreManagement.Models;

namespace StoreManagement.ViewModels.CartViewModel
{
    public class CartVM
    {
        public List<Cart> carts { get; set; }
        public double Total { get; set; }
    }
}
