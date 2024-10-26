import React from "react";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import { useLocation } from "react-router-dom";

import PaymentForm from "../../components/Payment/PaymentForm";

const stripePromise = loadStripe(
  "pk_test_51Q0egCAafQlHWfVL80TnXD0hbywqdwqxfG6s3nTopVVZS3ySWOQAV27G7dQ6nWTPqWg0Na6PyszEAONY8lOl7hb800L9MZ0F9b"
);

function PaymentPage() {
  const location = useLocation();
  const state = location.state;
  const options = {
    mode: 'payment',
    amount: state.amount*100,
    currency: 'usd',
  };

  return (
    <Elements stripe={stripePromise} options={options}>
      <PaymentForm />
    </Elements>
  );
}

export default PaymentPage;
