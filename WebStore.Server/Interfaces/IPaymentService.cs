using Stripe;

namespace WebStore.Server.Interfaces
{
    public interface IPaymentService
    {
        PaymentIntent CreatePaymentIntent(long amount, string currency);
        PaymentIntent GetPaymentIntent(string key);
    }
}
