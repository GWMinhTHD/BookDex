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
    <div className="flex flex-col min-h-screen">
      <div className="flex-grow overflow-auto p-4">
        <h1 className="text-3xl font-bold mb-8">Your Cart</h1>
        <div className="space-y-8 pb-32">
          {cart.length > 0 &&
            cart.map((item) => (
              <div
                key={item.id}
                className="flex flex-col md:flex-row items-center md:items-start space-y-4 md:space-y-0 md:space-x-6 border-b pb-8"
              >
                <img
                  src={`https://localhost:7216/img/bookcover/${item.bookCover}`}
                  alt={`Cover of ${item.bookName}`}
                  className="w-48 h-72 object-cover rounded-lg shadow-md"
                />
                <div className="flex-grow text-center md:text-left">
                  <h2 className="text-2xl font-semibold">{item.bookName}</h2>
                  <p className="text-xl text-gray-600 mt-2">
                    {item.bookAuthors.join(", ")}
                  </p>
                  <p className="text-2xl font-bold text-green-600 mt-4">
                    ${item.bookPrice.toFixed(2)}
                  </p>
                </div>
                <button
                  onClick={() => handleRemoveFromCart(item.id)}
                  className="px-6 py-3 bg-red-500 text-white font-semibold rounded-md shadow-md hover:bg-red-600 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                >
                  Remove
                </button>
              </div>
            ))}
        </div>
      </div>
      <div className="fixed bottom-0 left-0 right-0 bg-white border-t shadow-md p-4">
        <div className="container mx-auto flex flex-col sm:flex-row justify-between items-center space-y-4 sm:space-y-0">
          <Link to="/">
            <button className="w-full sm:w-auto px-6 py-3 bg-gray-500 text-white font-semibold rounded-md shadow-md hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out order-3 sm:order-1">
              Continue Shopping
            </button>
          </Link>
          {total ? (
            <p className="text-2xl font-bold order-1 sm:order-2">
              Total: ${total.toFixed(2)}
            </p>
          ) : (
            <></>
          )}
          <Link
            to="/payment"
            state={{ amount: total }}
          >
            <button className="w-full sm:w-auto px-6 py-3 bg-blue-600 text-white font-semibold rounded-md shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out order-2 sm:order-3">
              Checkout
            </button>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
