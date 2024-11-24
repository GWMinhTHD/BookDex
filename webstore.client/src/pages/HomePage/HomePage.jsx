import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import { Link } from "react-router-dom";
import Select from "react-select";
import { useAuth } from "../../Context/useAuth";
import Carousel from "../../components/Book/Carousel";
import { ShoppingCart } from "lucide-react";

function HomePage() {
  const [books, setBooks] = useState([]);
  const [featuredBooks, setFeaturedBooks] = useState([]);
  const [categories, setCategories] = useState();
  const [authors, setAuthors] = useState();
  const [currentPage, setCurrentPage] = useState(1);
  const [booksPerPage] = useState(9);
  const [selectedGenres, setSelectedGenres] = useState([]);
  const [selectedAuthors, setSelectedAuthors] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const { addCart } = useAuth();

  const handleAddToCart = (e, id) => {
    e.preventDefault();
    e.stopPropagation();
    addCart(id);
  };

  const fetchData = async () => {
    setIsLoading(true);
    try {
      const [booksResponse, genresResponse, authorsResponse] =
        await Promise.all([
          BookStoreApi.getAll("Book"),
          BookStoreApi.getAll("Category"),
          BookStoreApi.getAll("Author"),
        ]);
      setBooks(booksResponse.data);
      setFeaturedBooks(booksResponse.data.filter((book) => book.isFeatured));
      setCategories(
        genresResponse.data.map((category) => ({
          value: category.id,
          label: category.name,
        }))
      );
      setAuthors(
        authorsResponse.data.map((author) => ({
          value: author.id,
          label: author.name,
        }))
      );
    } catch (error) {
      console.error("Error fetching data:", error);
    }
    setIsLoading(false);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const filteredBooks = books.filter(
    (book) =>
      (selectedGenres.length === 0 ||
        selectedGenres.every((genre) =>
          book.categoryIDs.includes(genre.value)
        )) &&
      (selectedAuthors.length === 0 ||
        selectedAuthors.every((author) =>
          book.authorIDs.includes(author.value)
        )) &&
      (searchTerm === "" ||
        book.name.toLowerCase().includes(searchTerm.toLowerCase()))
  );

  const indexOfLastBook = currentPage * booksPerPage;
  const indexOfFirstBook = indexOfLastBook - booksPerPage;
  const currentBooks = filteredBooks.slice(indexOfFirstBook, indexOfLastBook);

  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-100 flex items-center justify-center">
        Loading...
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-900 text-gray-100">
      <div className="w-full">
        {featuredBooks.length > 0 && <Carousel books={featuredBooks} />}
      </div>

      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-8 py-4">
        <div className="mb-8 flex flex-wrap gap-4">
          <input
            type="text"
            placeholder="Search by title"
            className="p-2 border rounded w-full sm:w-64 bg-gray-800 text-gray-100 border-gray-700 placeholder-gray-400"
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
              styles={{
                control: (base) => ({
                  ...base,
                  backgroundColor: "#1f2937",
                  borderColor: "#374151",
                }),
                menu: (base) => ({
                  ...base,
                  backgroundColor: "#1f2937",
                }),
                option: (base, state) => ({
                  ...base,
                  backgroundColor: state.isFocused ? "#374151" : "#1f2937",
                  color: "#f3f4f6",
                }),
                multiValue: (base) => ({
                  ...base,
                  backgroundColor: "#374151",
                }),
                multiValueLabel: (base) => ({
                  ...base,
                  color: "#f3f4f6",
                }),
                multiValueRemove: (base) => ({
                  ...base,
                  color: "#f3f4f6",
                  ":hover": {
                    backgroundColor: "#4b5563",
                    color: "#f3f4f6",
                  },
                }),
              }}
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
              styles={{
                control: (base) => ({
                  ...base,
                  backgroundColor: "#1f2937",
                  borderColor: "#374151",
                }),
                menu: (base) => ({
                  ...base,
                  backgroundColor: "#1f2937",
                }),
                option: (base, state) => ({
                  ...base,
                  backgroundColor: state.isFocused ? "#374151" : "#1f2937",
                  color: "#f3f4f6",
                }),
                multiValue: (base) => ({
                  ...base,
                  backgroundColor: "#374151",
                }),
                multiValueLabel: (base) => ({
                  ...base,
                  color: "#f3f4f6",
                }),
                multiValueRemove: (base) => ({
                  ...base,
                  color: "#f3f4f6",
                  ":hover": {
                    backgroundColor: "#4b5563",
                    color: "#f3f4f6",
                  },
                }),
              }}
            />
          </div>
        </div>

        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
          {currentBooks.map((book) => (
            <Link
              key={book.id}
              to={`/book/${book.id}`}
              className="block bg-gray-800 rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow duration-200"
            >
              <div className="flex p-2">
                <div className="w-[120px] h-[180px] flex-shrink-0">
                  {book.cover ? (
                    <img
                      src={`data:image/png;base64,${book.cover}`}
                      alt={book.name}
                      className="w-full h-full object-cover rounded-md"
                    />
                  ) : (
                    <img
                      src={`https://localhost:7216/img/bookcover/placeholder.jpg`}
                      alt={book.name}
                      className="w-full h-full object-cover rounded-md"
                    />
                  )}
                </div>
                <div className="flex-grow ml-6 flex flex-col">
                  <div className="flex-grow">
                    <h2 className="text-xl font-semibold text-gray-100 mb-1">
                      {book.name}
                    </h2>
                    <p className="text-gray-300 mb-1">
                      {book.authors.join(", ")}
                    </p>
                    <p className="text-gray-400 text-sm mb-2">
                      {book.categories.join(", ")}
                    </p>
                  </div>
                  <div className="flex items-center justify-between">
                    <span className="text-lg font-bold text-gray-100">
                      ${book.price.toFixed(2)}
                    </span>
                    <button
                      onClick={(e) => handleAddToCart(e, book.id)}
                      className="p-2 bg-blue-600 text-white rounded-full hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-300 ease-in-out"
                    >
                      <ShoppingCart className="w-5 h-5" />
                      <span className="sr-only">Add to Cart</span>
                    </button>
                  </div>
                </div>
              </div>
            </Link>
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
                    ? "bg-blue-600 text-white"
                    : "bg-gray-800 text-gray-100 hover:bg-gray-700"
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
