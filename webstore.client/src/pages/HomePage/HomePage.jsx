import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";
import Select from "react-select";
import "../../pages/HomePage/HomePage.css";
import { useAuth } from "../../Context/useAuth";



function HomePage() {
  const [books, setBooks] = useState({});
  const [categories, setCategories] = useState();
  const [authors, setAuthors] = useState();
  const [currentPage, setCurrentPage] = useState(1);
  const [booksPerPage] = useState(8);
  const [selectedGenres, setSelectedGenres] = useState([]);
  const [selectedAuthors, setSelectedAuthors] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const { addCart } = useAuth();

  const handleAddToCart = (id) => {
    addCart(id);
  };

  const fetchData = async () => {
    setIsLoading(true);
    try {
      const [booksResponse, genresResponse, authorsResponse] =
        await Promise.all([
          BookStoreApi.getAll("Book"),
          BookStoreApi.getAll("Category"),
          BookStoreApi.getAll("Author")
        ]);
      setBooks(booksResponse.data);
      setCategories(
        genresResponse.data.map((genre) => ({ value: genre, label: genre }))
      );
      setAuthors(
        authorsResponse.data.map((author) => ({ value: author, label: author }))
      );
    } catch (error) {
      console.error("Error fetching data:", error);
    }
    setIsLoading(false);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const filteredBooks =
    books.length > 0 &&
    books.filter(
      (book) =>
        (selectedGenres.length === 0 || selectedGenres.every((genre) => book.categoryIDs.includes(genre.value))) &&
        (selectedAuthors.length === 0 || selectedAuthors.every((author) => book.authorIDs.includes(author.value))) &&
        (searchTerm === "" || book.name.toLowerCase().includes(searchTerm.toLowerCase()))
    );

  const indexOfLastBook = currentPage * booksPerPage;
  const indexOfFirstBook = indexOfLastBook - booksPerPage;
  const currentBooks = filteredBooks.length > 0 && filteredBooks.slice(indexOfFirstBook, indexOfLastBook);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);


  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        Loading...
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-100 py-8">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="mb-8 flex flex-wrap gap-4">
          <input
            type="text"
            placeholder="Search by title"
            className="p-2 border rounded w-full sm:w-64"
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <div className="w-full sm:w-64">
            <Select
              isMulti
              name="genres"
              options={categories}
              className="basic-multi-select"
              classNamePrefix="select"
              placeholder="Select Genres"
              onChange={setSelectedGenres}
            />
          </div>
          <div className="w-full sm:w-64">
            <Select
              isMulti
              name="authors"
              options={authors}
              className="basic-multi-select"
              classNamePrefix="select"
              placeholder="Select Authors"
              onChange={setSelectedAuthors}
            />
          </div>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
          {currentBooks.length > 0 &&
            currentBooks.map((book) => (
              <div
                key={book.id}
                className="bg-white rounded-lg shadow-md overflow-hidden flex flex-col"
              >
                <div className="h-80 p-4 ">
                  {book.cover ? (
                    <img
                      src={`data:image/png;base64,${book.cover}`}
                      alt={book.name}
                      className="w-full h-full object-contain"
                    />
                  ) : (
                    <img
                      src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                      alt={book.name}
                      className="w-full h-full object-contain"
                    />
                  )}
                </div>
                <div className="p-4 flex-grow flex flex-col justify-between">
                  <div>
                    <h2 className="text-xl font-semibold text-gray-800 mb-2">
                      {book.name}
                    </h2>
                    <p className="text-sm text-gray-600 mb-2">
                      {book.authors.join(", ")}
                    </p>
                    <p className="text-xs text-gray-500 mb-2">
                      {book.categories.join(", ")}
                    </p>
                  </div>
                  <div>
                    <p className="text-lg font-bold text-green-600 mb-4">
                      ${book.price.toFixed(2)}
                    </p>
                    <div className="flex flex-col space-y-2">
                      <button
                        onClick={() => handleAddToCart(book.id)}
                        className="w-full px-4 py-2 bg-blue-600 text-white text-sm font-semibold rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out text-center"
                      >
                        Add to Cart
                      </button>
                      <Link
                        to={`/book/${book.id}`}
                        className="w-full px-4 py-2 bg-gray-200 text-gray-800 text-sm font-semibold rounded-md hover:bg-gray-300 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 transition duration-300 ease-in-out text-center"
                      >
                        More Details
                      </Link>
                    </div>
                  </div>
                </div>
              </div>
            ))}
        </div>

        <div className="mt-8 flex justify-center">
          {Array.from(
            { length: Math.ceil(filteredBooks.length / booksPerPage) },
            (_, i) => (
              <button
                key={i}
                onClick={() => paginate(i + 1)}
                className={`mx-1 px-4 py-2 border ${
                  currentPage === i + 1
                    ? "bg-blue-500 text-white"
                    : "bg-white text-blue-500"
                } rounded`}
              >
                {i + 1}
              </button>
            )
          )}
        </div>
      </div>
    </div>
  );
}

export default HomePage;
