import React from "react";
import { useAuth } from "../../Context/useAuth";
import Button from "react-bootstrap/esm/Button";
import { LinkContainer } from "react-router-bootstrap";
//import { useState } from "react";
//import { BASE_URL } from "../../Constants";

function Book({
  id,
  name,
  authors,
  categories,
  cover,
  //publishing_house,
  price,
  //discount,
}) {

  const { addCart } = useAuth();

  const handleAddToCart = (id) => {
    addCart(id);
  };

  return (
    <>
      <div className="book-card col-lg-3 col-md-4 col-sm-6 col-12 g-3">
        <div className="book-img d-flex justify-content-center align-items-center">
          <img
            src={"https://localhost:7216/img/bookcover/" + cover}
            alt="Book cover"
          />
        </div>
        <div className="book-info">
          <h4 className="book-name">{name}</h4>
          <p className="author">Author: {authors.join(", ")}</p>
          <p className="category">Category: {categories.join(", ")}</p>
          <h5>
            Price: <span className="price">${price}</span>
          </h5>

          <div className="actionButtons">
            <LinkContainer to={`/book/${id}`}>
              <Button variant="danger">See details &gt;</Button>
            </LinkContainer>
            <button
              className="add-to-cart text-center btn btn-danger mt-2"
              onClick={() => handleAddToCart(id)}
            >
              Add to cart
            </button>
          </div>
        </div>
      </div>
    </>
  );
}

export default Book;
