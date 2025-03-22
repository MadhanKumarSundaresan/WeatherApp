import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { saveToken } from "../utils";
import api from "../api";
import { Form, Button, Container, Card, Alert } from "react-bootstrap";

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const response = await api.post("/auth/login", { username, password });
      saveToken(response.data.token);
      navigate("/dashboard");
    } catch (error) {
      setError("Invalid credentials.");
    }
  };

  return (
    <Container className="vh-100 d-flex justify-content-center align-items-center">
      <Card className="p-4 shadow-lg" style={{ width: "400px" }}>
        <Card.Body>
          <h2 className="text-center">Login</h2>
          {error && <Alert variant="danger">{error}</Alert>}

          <Form>
            <Form.Group className="mb-3">
              <Form.Label>Username</Form.Label>
              <Form.Control
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Password</Form.Label>
              <Form.Control
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </Form.Group>

            <Button variant="primary" onClick={handleLogin}>
              Login
            </Button>
          </Form>
        </Card.Body>
      </Card>
    </Container>
  );
};

export default Login;
