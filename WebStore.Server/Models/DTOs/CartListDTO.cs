namespace WebStore.Server.Models.DTOs
{
    public class CartListDTO
    {
        public List<CartDTO> Cart { get; set; }
        public double Total { get; set; } = 0;
    }
}
