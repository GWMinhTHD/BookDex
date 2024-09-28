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
  }, []);

  return (
    <>
      {books.length > 0 && (
        <div className="min-h-screen bg-gray-100 py-8">
          <div className="max-w-3xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="mb-8 flex items-center justify-between">
              <h1 className="text-3xl font-bold text-gray-900">
                Order Details
              </h1>
              <Link
                to="/order"
                className="px-4 py-2 bg-blue-600 text-white text-sm font-semibold rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
              >
                Back to Orders
              </Link>
            </div>

            <div className="bg-white shadow-md rounded-lg overflow-hidden">
              <div className="p-6">
                <h2 className="text-xl font-semibold text-gray-800 mb-4">
                  Order Items
                </h2>
                <ul className="divide-y divide-gray-200">
                  {books.map((item, index) => (
                    <li
                      key={index}
                      className="py-4 flex justify-between items-center"
                    >
                      <span className="text-gray-800">{item.bookName}</span>
                      <span className="text-gray-600">
                        ${item.bookPrice.toFixed(2)}
                      </span>
                    </li>
                  ))}
                </ul>
              </div>
              {total ? (<div className="bg-gray-50 px-6 py-4">
                <div className="flex justify-between items-center">
                  <span className="text-lg font-semibold text-gray-800">
                    Total
                  </span>
                  <span className="text-lg font-bold text-green-600">
                    ${total.toFixed(2)}
                  </span>
                </div>
              </div>):(<></>)}
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default OrderDetails;
