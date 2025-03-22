import React, { useState } from "react";
import { Form, Button, Alert, Container, Card, Modal } from "react-bootstrap";
import api from "../api";

const CitySearch = ({ refreshData }) => {
  const [city, setCity] = useState("");
  const [weather, setWeather] = useState(null);
  const [error, setError] = useState(null);
  const [showAddModal, setShowAddModal] = useState(false);

  const handleSearch = async () => {
    setError(null);
    setWeather(null);

    if (!city.trim()) {
      setError("Please enter a city name.");
      return;
    }

    try {
      // Fetch weather by city name
      const response = await api.get(`/weather/${city}`);
      setWeather(response.data);
    } catch (error) {
      // If city not found, show the add city modal
      setShowAddModal(true);
      setError(`City "${city}" not found! Do you want to add it?`);
    }
  };

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      e.preventDefault();
      handleSearch();
    }
  };

  const handleAddCity = async () => {
    try {
      // Send city in correct JSON format
      const requestBody = {
        city: city.trim()
      };

      await api.post("/weather", requestBody);  // Sending city as JSON
      setShowAddModal(false);
      refreshData();  // Reload data after adding city
      setWeather(null);
      setCity("");
      setError(null);
    } catch (error) {
      setError(`Failed to add city "${city}".`);
    }
  };

  return (
    <Container className="my-4">
      <Form className="d-flex">
        <Form.Control
          type="text"
          placeholder="Search for a city..."
          value={city}
          onChange={(e) => setCity(e.target.value)}
          onKeyDown={handleKeyPress}
        />
        <Button variant="primary" onClick={handleSearch} className="ms-2">
          Search
        </Button>
      </Form>

      {error && <Alert variant="warning" className="mt-3">{error}</Alert>}

      {weather && (
        <Card className="mt-4 shadow-sm">
          <Card.Body>
            <Card.Title>{weather.city}</Card.Title>
            <Card.Text>
              <strong>Description:</strong> {weather.description}
            </Card.Text>
            <Card.Text>
              <strong>Temperature:</strong> {weather.temperature}°C
            </Card.Text>
            <Card.Text>
              <strong>Humidity:</strong> {weather.humidity}%
            </Card.Text>
            <Card.Text>
              <small className="text-muted">
                Last Updated: {new Date(weather.lastUpdated).toLocaleString()}
              </small>
            </Card.Text>
          </Card.Body>
        </Card>
      )}

      {/* Modal for Adding City */}
      <Modal show={showAddModal} onHide={() => setShowAddModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Add City</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>City <strong>"{city}"</strong> not found. Do you want to add it?</p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowAddModal(false)}>
            No
          </Button>
          <Button variant="primary" onClick={handleAddCity}>
            Yes, Add City
          </Button>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};

export default CitySearch;
