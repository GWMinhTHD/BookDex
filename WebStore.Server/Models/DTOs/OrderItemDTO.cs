namespace WebStore.Server.Models.DTOs
{
    public class OrderItemDTO
    {
        public byte[]? Cover { get; set; }
        public string BookName { get; set; }
        public float BookPrice { get; set; }
    }
}
