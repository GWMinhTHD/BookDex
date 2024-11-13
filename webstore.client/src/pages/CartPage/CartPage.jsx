import React, { useEffect, useState } from "react";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";
import { Trash2 } from "lucide-react";
import PaymentForm from "../../components/Payment/PaymentForm";

const stripePromise = loadStripe(
  "pk_test_51Q0egCAafQlHWfVL80TnXD0hbywqdwqxfG6s3nTopVVZS3ySWOQAV27G7dQ6nWTPqWg0Na6PyszEAONY8lOl7hb800L9MZ0F9b"
);

function CartPage() {
  const [cart, setCart] = useState([]);
  const [total, setTotal] = useState(0);

  const handleRemoveFromCart = (id) => {
    BookStoreApi.removeCart(id).then(() => fetchUserCart());
  };

  const fetchUserCart = async () => {
    try {
      const response = await BookStoreApi.getUserCart();
      setCart(response.data.cart);
      setTotal(response.data.total);
    } catch (error) {
      console.error("Error fetching user cart:", error);
    }
  };

  useEffect(() => {
    fetchUserCart();
  }, []);

  const options = {
    mode: "payment",
    amount: total * 100,
    currency: "usd",
  };

  return (
    <div className="min-h-screen bg-gray-900 py-8">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <h1 className="text-3xl font-bold text-gray-100 mb-8">Your Cart</h1>
        <div className="lg:grid lg:grid-cols-3 lg:gap-8">
          {/* Cart Items Section */}
          <div className="lg:col-span-2">
            <div className="bg-gray-800 shadow-md rounded-lg overflow-hidden mb-8">
              <div className="px-4 py-5 sm:p-6 max-h-[calc(100vh-200px)] overflow-y-auto">
                <div className="space-y-6">
                  {cart.length > 0 ? (
                    cart.map((item) => (
                      <div
                        key={item.id}
                        className="flex items-center space-x-6 border-b border-gray-700 pb-6 last:border-b-0 last:pb-0"
                      >
                        {item.bookCover ? (
                          <img
                            src={`data:image/png;base64,${item.bookCover}`}
                            alt={`Cover of ${item.bookName}`}
                            className="w-16 h-28 sm:w-24 sm:h-36 object-cover rounded-md shadow-md"
                          />
                        ) : (
                          <img
                            src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                            alt={`Cover of ${item.bookName}`}
                            className="w-16 h-28 sm:w-24 sm:h-36 object-cover rounded-md shadow-md"
                          />
                        )}
                        <div className="flex-grow">
                          <h2 className="text-md sm:text-xl font-semibold text-gray-100">
                            {item.bookName}
                          </h2>
                          <p className="text-sm sm:text-md text-gray-300 mt-1">
                            {item.bookAuthors.join(", ")}
                          </p>
                          <div className="flex justify-between items-center mt-2">
                            <p className="text-md sm:text-lg font-bold text-green-400">
                              ${item.bookPrice.toFixed(2)}
                            </p>
                            <button
                              onClick={() => handleRemoveFromCart(item.id)}
                              className="p-2 bg-red-600 text-white rounded-full hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                              aria-label="Remove item"
                            >
                              <Trash2 size={20} />
                            </button>
                          </div>
                        </div>
                      </div>
                    ))
                  ) : (
                    <div className="text-center py-8">
                      <p className="text-gray-300 mb-4">Your cart is empty</p>
                      <Link
                        to="/"
                        className="inline-block px-6 py-2 bg-blue-600 text-white text-sm font-semibold rounded-md shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                      >
                        Continue Shopping
                      </Link>
                    </div>
                  )}
                </div>
              </div>
              {cart.length > 0 && (
                <div className="bg-gray-700 px-4 py-4 sm:px-6">
                  <div className="flex justify-between items-center">
                    <span className="text-lg font-semibold text-gray-100">
                      Total
                    </span>
                    <span className="text-lg font-bold text-green-400">
                      ${total.toFixed(2)}
                    </span>
                  </div>
                </div>
              )}
            </div>
          </div>

          {/* Payment Section */}
          {total > 0 && (
            <div className="lg:col-span-1">
              <div className="sticky top-8">
                <Elements stripe={stripePromise} options={options}>
                  <PaymentForm total={total} />
                </Elements>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default CartPage;
