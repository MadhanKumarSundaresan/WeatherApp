import React from "react";
import { removeToken } from "../utils";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    removeToken();
    navigate("/");
  };

  return (
    <div>
      <h1>Welcome to the Home Page!</h1>
      <button onClick={handleLogout}>Logout</button>
    </div>
  );
};

export default Home;
