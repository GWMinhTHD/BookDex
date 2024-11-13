import React from "react";
import { Route, Routes } from "react-router-dom";
import HomePage from "../pages/HomePage/HomePage";
import NotFound from "../pages/NotFound";
import LoginPage from "../pages/LoginPage/LoginPage";
import RegisterPage from "../pages/RegisterPage/RegisterPage";
import LibraryPage from "../pages/LibraryPage/LibraryPage";
import ProtectedRoute from "../Routes/ProtectedRoute";
import BookDetails from "../pages/BookDetails/BookDetails";
import CartPage from "../pages/CartPage/CartPage";
import ReaderPage from "../pages/ReaderPage/ReaderPage";
import OrderPage from "../pages/OrderPage/OrderPage";
import OrderDetails from "../pages/OrderDetails/OrderDetails";
import ProfilePage from "../pages/ProfilePage/ProfilePage";

function Body() {
  return (
    <div>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/book/:id" element={<BookDetails />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route element={<ProtectedRoute />}>
          <Route path="/cart" element={<CartPage />} />
          <Route path="/library" element={<LibraryPage />} />
          <Route path="/reader/:id" element={<ReaderPage />} />
          <Route path="/order" element={<OrderPage />} />
          <Route path="/order/:id" element={<OrderDetails />} />
          <Route path="/profile" element={<ProfilePage />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
}

export default Body;
