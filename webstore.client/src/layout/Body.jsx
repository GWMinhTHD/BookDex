import React from "react";
import { Route, Routes } from "react-router-dom";
import Author from "../pages/Author";
import Category from "../pages/Category";
import EditAuthor from "../pages/EditAuthor";
import EditCategory from "../pages/EditCategory";
import HomePage from "../pages/HomePage/HomePage";
import NotFound from "../pages/NotFound";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import LibraryPage from "../pages/LibraryPage/LibraryPage";
import ProtectedRoute from "../Routes/ProtectedRoute";
import BookDetails from "../pages/BookDetails/BookDetails";
import CartPage from "../pages/CartPage/CartPage";
import PaymentPage from "../pages/PaymentPage/PaymentPage";

function Body() {
  return (
    <div>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/book/:id" element={<BookDetails />} />
        <Route path="/authors" element={<Author />} />
        <Route path="/edit/author/:id" element={<EditAuthor />} />
        <Route path="/categories" element={<Category />} />
        <Route path="/edit/category/:id" element={<EditCategory />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route element={<ProtectedRoute />}>
          <Route path="/payment" element={<PaymentPage />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/library" element={<LibraryPage />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
}

export default Body;
