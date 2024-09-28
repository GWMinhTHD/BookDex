namespace WebStore.Server.Models.DTOs
{
    public class OrderDetailDTO
    {
        public List<OrderItemDTO> orderItems {  get; set; }
        public float Total { get; set; }

    }
}
