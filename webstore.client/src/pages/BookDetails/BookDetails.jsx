import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";
import { useAuth } from "../../Context/useAuth";

function BookDetails() {
  const [book, setBook] = useState(null);
  const { id } = useParams();
  const { addCart } = useAuth();

  const handleAddToCart = (id) => {
    addCart(id);
  };

  useEffect(() => {
    BookStoreApi.getById("Book", id).then((item) => setBook(item.data));
  }, [id]);

  return (
    <div className="min-h-screen bg-gray-900 py-8">
      <div className="container mx-auto px-4">
        <div className="mb-8">
          <Link to="/">
            <button className="px-4 py-2 bg-gray-700 text-white font-semibold rounded-md shadow-md hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out">
              Back to Home
            </button>
          </Link>
        </div>
        {book ? (
          <div className="grid md:grid-cols-2 gap-8 items-start">
            <div className="space-y-4">
              {book.cover ? (
                <img
                  src={`data:image/png;base64,${book.cover}`}
                  alt={book.name}
                  className="w-full max-w-md mx-auto rounded-lg shadow-lg object-contain h-[500px]"
                />
              ) : (
                <img
                  src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                  alt={book.name}
                  className="w-full max-w-md mx-auto rounded-lg shadow-lg object-contain h-[500px]"
                />
              )}
            </div>
            <div className="space-y-4">
              <h1 className="text-3xl font-bold text-gray-100">{book.name}</h1>
              <p className="text-xl text-gray-300">
                by {book.authors.join(", ")}
              </p>
              <p className="text-lg font-semibold text-gray-400">
                {book.categories.join(", ")}
              </p>
              <p className="text-2xl font-bold text-green-400">
                ${book.price.toFixed(2)}
              </p>
              <button
                onClick={() => handleAddToCart(book.id)}
                className="w-full md:w-auto px-6 py-2 bg-blue-600 text-white font-semibold rounded-md shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
              >
                Add to Cart
              </button>
              <div className="space-y-2">
                <h2 className="text-xl font-semibold text-gray-200">
                  Description
                </h2>
                <p className="text-gray-300">{book.description}</p>
              </div>
            </div>
          </div>
        ) : (
          <div className="text-center text-gray-300">Loading...</div>
        )}
      </div>
    </div>
  );
}

export default BookDetails;
