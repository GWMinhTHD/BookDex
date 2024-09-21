import {
  CardCvcElement,
  CardExpiryElement,
  CardNumberElement,
  PaymentElement,
  useElements,
  useStripe,
} from "@stripe/react-stripe-js";
import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../Context/useAuth";

const PaymentForm = () => {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const stripe = useStripe();
  const elements = useElements();
/*   const [clientSecret, setClientSecret] = useState(""); */
  const [errorMessage, setErrorMessage] = useState();
  const [loading, setLoading] = useState(false);
  const elementOptions = {
    style: {
      base: {
        fontSize: "16px",
        color: "#32325d",
        fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
        fontSmoothing: "antialiased",
        "::placeholder": {
          color: "#aab7c4",
        },
      },
      invalid: {
        color: "#fa755a",
        iconColor: "#fa755a",
      },
    },
  };

  const handleError = (error) => {
    setLoading(false);
    setErrorMessage(error.message);
  };

  const handleSubmit = async (event) => {
    // Disable default form submission behaviior of refreshing the page.
    event.preventDefault();

    if (!stripe) {
      //Disable form submission until Stripe.js has loaded.
      return;
    }
    setLoading(true);

    // Trigger form validation and wallet collection
    const { error: submitError } = await elements.submit();
    if (submitError) {
      handleError(submitError);
      return;
    }

    const res = await axios
      .post(
        "https://localhost:7216/api/Payment/create-payment-intent",
        {
          currency: "usd", // Currency type
        },
        { headers: { Authorization: `Bearer ${token}` } }
    );

    console.log(res);

    /* const cardNumberElement = elements.getElement(CardNumberElement); */

    const clientSecretResponse = res.data.clientSecret;
    console.log("Client Secret from response:", clientSecretResponse);

/*     const { error } = await stripe.confirmCardPayment(clientSecret, {
      payment_method: {
        card: cardNumberElement,
      },
    }); */
    const { paymentIntent, error } = await stripe.confirmPayment({
      elements,
      clientSecret: clientSecretResponse,
      confirmParams: {
        return_url: "https://example.com/order/123/complete",
      },
      redirect: "if_required",
    });

    if (error) {
      // This point is only reached if there's an immediate error when
      // confirming the payment. Show the error to your customer (for example, payment details incomplete)
      handleError(error);
    } else if (paymentIntent && paymentIntent.status === "succeeded") {
      console.log(paymentIntent.id);
      await axios
        .post(
          "https://localhost:7216/api/Payment/check-payment-confirm",
          {
            Key: paymentIntent.id,
          },
          { headers: { Authorization: `Bearer ${token}` } }
        )
        .then(() => {
          navigate("/");
        });
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <PaymentElement />
      <button type="submit" disabled={!stripe || loading}>
        Submit Payment
      </button>
      {errorMessage && <div>{errorMessage}</div>}
    </form>
  );
};

export default PaymentForm;
