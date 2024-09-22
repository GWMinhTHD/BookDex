using Stripe;

namespace WebStore.Server.Interfaces
{
    public interface IPaymentService
    {
        PaymentIntent CreatePaymentIntent(long amount, string currency, List<string> paymentMethodTypes);
        PaymentIntent GetPaymentIntent(string key);
    }
}
