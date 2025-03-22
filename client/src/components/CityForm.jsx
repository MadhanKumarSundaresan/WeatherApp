import React, { useState } from "react";
import { Form, Button, Container, Alert } from "react-bootstrap";
import api from "../api";

const CityForm = ({ refreshData }) => {
  const [city, setCity] = useState("");
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);

  const handleAddOrUpdate = async () => {
    if (!city.trim()) {
      setError("Please enter a valid city name.");
      return;
    }

    try {
      await api.post("/weather", { city });
      setSuccess(`Weather data for "${city}" added/updated successfully.`);
      setCity("");
      refreshData();
    } catch (error) {
      setError(`Failed to add/update city: "${city}".`);
    }
  };

  // Trigger Add/Update on "Enter" key press
  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      e.preventDefault();
      handleAddOrUpdate();
    }
  };

  return (
    <Container className="my-4">
      <h4 className="mb-3">Add or Update City</h4>
      <Form className="d-flex">
        <Form.Control
          type="text"
          placeholder="Enter city name..."
          value={city}
          onChange={(e) => setCity(e.target.value)}
          onKeyDown={handleKeyPress} 
        />
        <Button
          variant="success"
          onClick={handleAddOrUpdate}
          className="ms-2"
        >
          Add/Update
        </Button>
      </Form>

      {error && <Alert variant="danger" className="mt-3">{error}</Alert>}
      {success && <Alert variant="success" className="mt-3">{success}</Alert>}
    </Container>
  );
};

export default CityForm;
