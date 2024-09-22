namespace WebStore.Server.Models.DTOs
{
    public class PaymentIntentRequest
    {
        public string Currency { get; set; }
        public List<string> PaymentMethodTypes { get; set; }
    }
}
