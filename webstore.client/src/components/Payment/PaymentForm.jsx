import {
  CardCvcElement,
  CardExpiryElement,
  CardNumberElement,
  useElements,
  useStripe,
} from "@stripe/react-stripe-js";
import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

const PaymentForm = ({ total }) => {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  const stripe = useStripe();
  const elements = useElements();
  const [errorMessage, setErrorMessage] = useState();
  const [loading, setLoading] = useState(false);

  const elementOptions = {
    style: {
      base: {
        fontSize: "16px",
        color: "#ffffff",
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

    const res = await axios.post(
      "https://localhost:7216/api/Payment/create-payment-intent",
      {
        currency: "usd", // Currency type
        paymentMethodTypes: ["card"],
      },
      { headers: { Authorization: `Bearer ${token}` } }
    );

    const cardNumberElement = elements.getElement(CardNumberElement);
    const clientSecretResponse = res.data.clientSecret;

    const { paymentIntent, error } = await stripe.confirmCardPayment(
      clientSecretResponse,
      {
        payment_method: {
          card: cardNumberElement,
        },
      }
    );

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
    <div className="bg-gray-800 shadow-md rounded-lg overflow-hidden mb-8">
      <form onSubmit={handleSubmit} className="p-6 space-y-6">
        <h2 className="text-2xl font-semibold text-gray-100 mb-4">
          Payment Details
        </h2>
        <div className="space-y-4">
          <div>
            <label
              htmlFor="card-number"
              className="block text-sm font-medium text-gray-300 mb-2"
            >
              Card Number
            </label>
            <div className="border border-gray-600 rounded-md p-3 bg-gray-700">
              <CardNumberElement id="card-number" options={elementOptions} />
            </div>
          </div>
          <div className="flex space-x-4">
            <div className="flex-1">
              <label
                htmlFor="card-expiry"
                className="block text-sm font-medium text-gray-300 mb-2"
              >
                Expiration Date
              </label>
              <div className="border border-gray-600 rounded-md p-3 bg-gray-700">
                <CardExpiryElement id="card-expiry" options={elementOptions} />
              </div>
            </div>
            <div className="flex-1">
              <label
                htmlFor="card-cvc"
                className="block text-sm font-medium text-gray-300 mb-2"
              >
                CVC
              </label>
              <div className="border border-gray-600 rounded-md p-3 bg-gray-700">
                <CardCvcElement id="card-cvc" options={elementOptions} />
              </div>
            </div>
          </div>
        </div>
        <button
          type="submit"
          disabled={!stripe || loading}
          className="w-full mt-6 bg-blue-600 hover:bg-blue-700 text-white font-bold py-3 px-4 rounded-md transition duration-300 ease-in-out disabled:opacity-50 disabled:cursor-not-allowed"
        >
          Pay ${total.toFixed(2)}
        </button>
        {errorMessage && (
          <div className="mt-4 text-red-500 text-center">{errorMessage}</div>
        )}
      </form>
    </div>
  );
};

export default PaymentForm;
