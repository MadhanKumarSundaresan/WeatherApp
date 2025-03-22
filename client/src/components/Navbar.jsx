import React from "react";
import { Navbar, Nav, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { removeToken } from "../utils";

const NavigationBar = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    removeToken();
    navigate("/");
  };

  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Navbar.Brand href="/">Weather Dashboard</Navbar.Brand>
      <Nav className="ml-auto">
        <Button variant="outline-light" onClick={handleLogout}>
          Logout
        </Button>
      </Nav>
    </Navbar>
  );
};

export default NavigationBar;
