import React from "react";
import { Navigate } from "react-router-dom";
import { getToken } from "../utils";

const ProtectedRoute = ({ children }) => {
  const token = getToken();
  return token ? children : <Navigate to="/" />;
};

export default ProtectedRoute;
