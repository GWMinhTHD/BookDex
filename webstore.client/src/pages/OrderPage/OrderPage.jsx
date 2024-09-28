import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import BookStoreApi from "../../api/BookStoreApi";

function OrderPage() {
  const [orders, setOrders] = useState({});

  useEffect(() => {
    BookStoreApi.getUserOrder().then((item) => setOrders(item.data));

  }, []);


  return (
    <>
      {orders.length > 0 && (
        <div className="min-h-screen bg-gray-100 py-8">
          <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8">
            <h1 className="text-3xl font-bold text-gray-900 mb-8">My Orders</h1>
              <ul className="space-y-4">
                {orders.map((order) => (
                  <li
                    key={order.id}
                    className="bg-white shadow-md rounded-lg overflow-hidden"
                  >
                    <div className="p-6 flex flex-col sm:flex-row justify-between items-start sm:items-center">
                      <div className="mb-4 sm:mb-0">
                        <h2 className="text-xl font-semibold text-gray-800">
                          Order #{order.id}
                        </h2>
                      </div>
                      <div className="flex flex-col sm:flex-row items-start sm:items-center gap-4">
                        <p className="text-lg font-bold text-green-600">
                          ${order.total.toFixed(2)}
                        </p>
                        <Link
                          to={`/order/${order.id}`}
                          className="px-4 py-2 bg-blue-600 text-white text-sm font-semibold rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                        >
                          View Details
                        </Link>
                      </div>
                    </div>
                  </li>
                ))}
              </ul>
          </div>
        </div>
      )}
    </>
  );
}

export default OrderPage;
