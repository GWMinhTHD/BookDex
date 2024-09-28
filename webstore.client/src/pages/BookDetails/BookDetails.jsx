import React from "react";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";
import { useAuth } from "../../Context/useAuth";

//import "../../pages/BookDetails/BookDetails.css";

function BookDetails() {
  const [book, setBook] = useState(null);
  const { id } = useParams();
  const { addCart } = useAuth();

  const handleAddToCart = (id) => {
    addCart(id);
  };

  useEffect(() => {
      BookStoreApi.getById("Book", id).then((item) =>
        setBook(item.data)
      );
  }, []);

  return (
    <>
      {book ? (
        <div className="container mx-auto px-4 py-8">
          <div className="mb-8">
            <Link to="/">
              <button className="px-4 py-2 bg-gray-600 text-white font-semibold rounded-md shadow-md hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out">
                Back to Home
              </button>
            </Link>
          </div>
          <div className="grid md:grid-cols-2 gap-8 items-start">
            <div className="space-y-4">
              {book.cover ? (
                <img
                  src={`data:image/png;base64,${book.cover}`}
                  alt={book.name}
                  className="w-full max-w-md mx-auto rounded-lg shadow-lg"
                />
              ) : (
                <img
                  src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                  alt={book.name}
                  className="w-full max-w-md mx-auto rounded-lg shadow-lg"
                />
              )}
            </div>
            <div className="space-y-4">
              <h1 className="text-3xl font-bold text-gray-800">{book.name}</h1>
              <p className="text-xl text-gray-600">
                by {book.authors.join(", ")}
              </p>
              <p className="text-lg font-semibold text-gray-500">
                {book.categories.join(", ")}
              </p>
              <p className="text-2xl font-bold text-green-600">
                ${book.price.toFixed(2)}
              </p>
              <div className="space-y-2">
                <h2 className="text-xl font-semibold text-gray-800">
                  Description
                </h2>
                <p className="text-gray-600">{book.description}</p>
              </div>
              <button
                onClick={() => handleAddToCart(book.id)}
                className="w-full md:w-auto px-6 py-2 bg-blue-600 text-white font-semibold rounded-md shadow-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
              >
                Add to Cart
              </button>
            </div>
          </div>
        </div>
      ) : (
        <></>
      )}
    </>
  );
}

export default BookDetails;
