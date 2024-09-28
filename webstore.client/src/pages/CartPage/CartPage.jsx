import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";

function CartPage() {
  const [cart, setCart] = useState({});
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

  return (
    <div className="min-h-screen bg-gray-100 py-8">
      <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">Your Cart</h1>
        <div className="bg-white shadow-md rounded-lg overflow-hidden mb-20">
          <div className="px-4 py-5 sm:p-6">
            <div className="space-y-6">
              {cart.length > 0 &&
                cart.map((item) => (
                  <div key={item.id} className="flex items-center space-x-6">
                    {item.bookCover ? (
                      <img
                        src={`data:image/png;base64,${item.bookCover}`}
                        alt={`Cover of ${item.bookName}`}
                        className="w-24 h-36 object-cover rounded-md shadow-md"
                      />
                    ) : (
                      <img
                        src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                        alt={`Cover of ${item.bookName}`}
                        className="w-24 h-36 object-cover rounded-md shadow-md"
                      />
                    )}
                    <div className="flex-grow">
                      <h2 className="text-xl font-semibold text-gray-800">
                        {item.bookName}
                      </h2>
                      <p className="text-md text-gray-600 mt-1">
                        {item.bookAuthors.join(", ")}
                      </p>
                      <p className="text-lg font-bold text-green-600 mt-2">
                        ${item.bookPrice.toFixed(2)}
                      </p>
                    </div>
                    <button
                      onClick={() => handleRemoveFromCart(item.id)}
                      className="px-4 py-2 bg-red-500 text-white text-sm font-semibold rounded-md hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                    >
                      Remove
                    </button>
                  </div>
                ))}
            </div>
          </div>
        </div>
      </div>
      <div className="fixed bottom-0 left-0 right-0 bg-white border-t shadow-md p-4">
        <div className="max-w-3xl mx-auto">
          {total ? (
            <p className="text-2xl font-bold mb-4 text-center">
              Total: <span className="text-green-600">${total.toFixed(2)}</span>
            </p>
          ) : (
            <p className="text-2xl font-bold mb-4 text-center">No Item</p>
          )}
          <div className="flex justify-between items-center">
            <Link
              to="/"
              className="w-auto px-2 sm:px-4 md:px-6 py-2 bg-gray-500 text-white text-xs sm:text-sm md:text-base font-semibold rounded-md shadow-md hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out text-center"
            >
              Continue Shopping
            </Link>
            {total ? (
              <Link
                to="/payment"
                state={{ amount: total }}
                className="w-auto px-2 sm:px-4 md:px-6 py-2 bg-blue-600 text-white text-xs sm:text-sm md:text-base font-semibold rounded-md shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out text-center"
              >
                Checkout
              </Link>
            ) : (
              <></>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
