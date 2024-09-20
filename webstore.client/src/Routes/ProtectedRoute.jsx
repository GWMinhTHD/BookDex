import React from "react";
import { Outlet, Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth";


const ProtectedRoute = () => {
  const location = useLocation();
  const { isLoggedIn } = useAuth();
  return isLoggedIn() ? (
    <Outlet/>
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};

export default ProtectedRoute;
