using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public double Total { get; set; }
        public List<OrderBook> OrderBooks { get; set; } = new List<OrderBook>();
        public DateTime DateCreated { get; set; }
    }
}
