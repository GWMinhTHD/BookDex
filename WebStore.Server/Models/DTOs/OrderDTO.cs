using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Server.Models.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public double Total { get; set; }
    }
}
