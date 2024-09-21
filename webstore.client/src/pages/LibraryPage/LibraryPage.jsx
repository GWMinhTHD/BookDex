import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";


function LibraryPage() {
  const [books, setBooks] = useState({});

  useEffect(() => {
    BookStoreApi.getUserLib().then((item) => setBooks(item.data));
  }, []);

  return (
    <div className="min-h-screen bg-gray-100 py-8 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-3xl font-bold text-gray-900 mb-8">My Library</h1>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
          {books.length > 0 &&
            books.map((book) => (
              <div
                key={book.id}
                className="bg-white rounded-lg shadow-md overflow-hidden"
              >
                <img
                  src={"https://localhost:7216/img/bookcover/" + book.cover}
                  alt={`Cover of ${book.name}`}
                  className="w-full h-64 object-cover"
                />
                <div className="p-4">
                  <h2 className="text-xl font-semibold text-gray-800 mb-2">
                    {book.name}
                  </h2>
                  <p className="text-gray-600 mb-4">
                    {book.authors.join(", ")}
                  </p>
                  <div className="flex flex-col space-y-2">
                    <Link to={`/reader/${book.id}`} >
                      <button className="bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 transition duration-300 ease-in-out">
                        Read Now
                      </button>
                    </Link>
                    <button className="bg-gray-200 text-gray-800 py-2 px-4 rounded-md hover:bg-gray-300 transition duration-300 ease-in-out">
                      More Details
                    </button>
                  </div>
                </div>
              </div>
            ))}
        </div>
      </div>
    </div>
  );
}

export default LibraryPage;
