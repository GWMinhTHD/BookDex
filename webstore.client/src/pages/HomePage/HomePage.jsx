import React, { useEffect, useState } from "react";
import BookStoreApi from "../../api/BookStoreApi";
import BookList from "../../components/Book/BookList";
import "../../pages/HomePage/HomePage.css";



function HomePage() {
  const [books, setBooks] = useState({});
  useEffect(() => {
    BookStoreApi.getAll("Book").then((item) => setBooks(item.data));
  }, []);

  return (
    <>
      {books.length > 0 && <BookList books={books} />}
    </>
  );
}

export default HomePage;
