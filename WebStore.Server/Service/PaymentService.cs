using Stripe;
using WebStore.Server.Interfaces;

namespace WebStore.Server.Service
{
    public class PaymentService : IPaymentService
    {
        public PaymentService()
        {
            StripeConfiguration.ApiKey = "sk_test_51Q0egCAafQlHWfVLnffZpe7VTHdxYfqAuCuSQEXdJrgd8UlQtfM770gupmX0gj1Z6R6pk4uwQaOW6N8V8HJcvOvo00L4PdDBPg";
        }

        public PaymentIntent CreatePaymentIntent(long amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = currency,
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();
            return service.Create(options);
        }

        public PaymentIntent GetPaymentIntent(string key)
        {
            var service = new PaymentIntentService();
            return service.Get(key);
        }
    }
}
