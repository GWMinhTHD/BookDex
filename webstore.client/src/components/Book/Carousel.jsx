import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { useAuth } from "../../Context/useAuth";

const Carousel = ({ books }) => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const { addCart } = useAuth();

  const handleAddToCart = (id) => {
    addCart(id);
  };
  const nextSlide = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % books.length);
  };

  const prevSlide = () => {
    setCurrentIndex(
      (prevIndex) => (prevIndex - 1 + books.length) % books.length
    );
  };

  useEffect(() => {
    const interval = setInterval(nextSlide, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div className="w-full max-w-6xl mx-auto">
      <div className="relative overflow-hidden rounded-lg shadow-lg">
        <div className="relative h-[400px] sm:h-[500px]">
          {books.map((book, index) => (
            <div
              key={book.id}
              className={`absolute top-0 left-0 w-full h-full transition-opacity duration-500 ease-in-out ${
                index === currentIndex
                  ? "opacity-100"
                  : "opacity-0 pointer-events-none"
              }`}
            >
              <Link to={`/book/${book.id}`} className="block w-full h-full">
                <div className="relative h-full">
                  {book.cover ? (
                    <img
                      src={`data:image/png;base64,${book.cover}`}
                      alt={book.name}
                      className="w-full h-full object-cover"
                    />
                  ) : (
                    <img
                      src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                      alt={book.name}
                      className="w-full h-full object-cover"
                    />
                  )}
                  <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center p-4 sm:p-6">
                    <div className="flex flex-col sm:flex-row w-full max-w-5xl mx-auto">
                      {book.cover ? (
                        <img
                          src={`data:image/png;base64,${book.cover}`}
                          alt={book.name}
                          className="w-48 h-72 sm:w-1/3 sm:h-full mr-auto ml-auto object-contain sm:mr-6 sm:ml-0"
                        />
                      ) : (
                        <img
                          src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                          alt={book.name}
                          className="w-48 h-72 sm:w-1/3 sm:h-full object-contain bg-white rounded-md shadow-md mr-6"
                        />
                      )}
                      <div className="w-full sm:w-2/3 flex flex-col justify-between">
                        <div>
                          <h2 className="text-xl sm:text-3xl font-bold text-white mb-0 sm:mb-2">
                            {book.name}
                          </h2>
                          <p className="text-sm sm:text-xl text-gray-300 sm:mb-2">
                            {book.authors.join(", ")}
                          </p>
                          <div className="flex flex-wrap gap-2 sm:mb-4">
                            {book.categories.map((category, idx) => (
                              <span
                                key={idx}
                                className="px-2 py-1 bg-gray-700 text-white text-xs sm:text-sm rounded-full"
                              >
                                {category}
                              </span>
                            ))}
                          </div>
                          <p className="hidden sm:block text-white text-sm sm:text-base mb-4 line-clamp-3 sm:line-clamp-4">
                            {book.description}
                          </p>
                        </div>
                        <div className="flex items-center justify-between">
                          <p className="text-xl sm:text-2xl font-bold text-white">
                            ${book.price.toFixed(2)}
                          </p>
                          <button
                            onClick={() => handleAddToCart(book.id)}
                            className="px-4 py-2 bg-blue-600 text-white text-sm sm:text-base font-semibold rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                          >
                            Add to Cart
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </Link>
            </div>
          ))}
        </div>
      </div>
      <div className="flex justify-between mt-4">
        <button
          onClick={prevSlide}
          className="bg-gray-800 text-white rounded-full p-2 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
        >
          <ChevronLeft className="w-6 h-6" />
        </button>
        <button
          onClick={nextSlide}
          className="bg-gray-800 text-white rounded-full p-2 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
        >
          <ChevronRight className="w-6 h-6" />
        </button>
      </div>
    </div>
  );
};

export default Carousel;
