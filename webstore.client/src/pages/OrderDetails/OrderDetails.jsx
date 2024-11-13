import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";
import { useParams } from "react-router-dom";

function OrderDetails() {
  const [books, setBooks] = useState({});
  const [total, setTotal] = useState(0);
  const { id } = useParams();


  useEffect(() => {
    BookStoreApi.getOrderDetail(id).then((res) => {
      setBooks(res.data.orderItems);
      setTotal(res.data.total);
    });
  }, [id]);

  return (
    <div className="min-h-screen bg-gray-900 py-8">
      <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="mb-8 flex items-center justify-between">
          <h1 className="text-3xl font-bold text-gray-100">Order Details</h1>
          <Link
            to="/order"
            className="px-4 py-2 bg-blue-600 text-white text-sm font-semibold rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
          >
            Back to Orders
          </Link>
        </div>

        {books.length > 0 ? (
          <div className="bg-gray-800 shadow-md rounded-lg overflow-hidden">
            <div className="p-6">
              <h2 className="text-xl font-semibold text-gray-100 mb-4">
                Order Items
              </h2>
              <ul className="divide-y divide-gray-700">
                {books.map((item, index) => (
                  <li key={index} className="py-4 flex items-center">
                    <div className="flex-shrink-0 w-16 h-24 mr-4 bg-gray-700 rounded overflow-hidden">
                      {item.cover ? (
                        <img
                          src={`data:image/png;base64,${item.cover}`}
                          alt={item.bookName}
                          className="w-full h-full object-cover"
                        />
                      ) : (
                        <div className="w-full h-full flex items-center justify-center text-gray-500">
                          No Image
                        </div>
                      )}
                    </div>
                    <div className="flex-grow">
                      <span className="text-gray-100">{item.bookName}</span>
                    </div>
                    <span className="text-gray-400">
                      ${item.bookPrice.toFixed(2)}
                    </span>
                  </li>
                ))}
              </ul>
            </div>
            {total > 0 && (
              <div className="bg-gray-700 px-6 py-4">
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
        ) : (
          <p className="text-center text-gray-400">
            No order details available.
          </p>
        )}
      </div>
    </div>
  );
}

export default OrderDetails;
