import React, { useEffect, useState } from "react";
import Book from "./Book";


function BookList({books}) {
  useEffect(() => {
    //console.log(JSON.stringify(books));
  }, []);
  return (
    <>
      <div className="container mt-3 mb-3">
        <div className="row" id="book-row">
           {books.map((item) => (
            <Book
              key={item.id}
              id={item.id}
              name={item.name}
              authors={item.authors}
              categories={item.categories}
              cover={item.cover}
              //publisher={item.publisher}
              //discount={item.discount}
              price={item.price}
            />
          ))} 
        </div>
      </div>
    </>
  );
}

export default BookList;
