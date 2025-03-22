import React, { useEffect, useState } from "react";
import { Card, Container, Row, Col, Spinner, Alert, Button } from "react-bootstrap";
import api from "../api";

const WeatherList = ({ refreshData }) => {
  const [weatherData, setWeatherData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const loadWeatherData = async () => {
    try {
      const response = await api.get("/weather");
      setWeatherData(response.data);
      setLoading(false);
    } catch (error) {
      setError("Failed to load weather data.");
      setLoading(false);
    }
  };

  useEffect(() => {
    loadWeatherData();
  }, [refreshData]);

  const handleDelete = async (city) => {
    if (window.confirm(`Are you sure you want to delete weather data for "${city}"?`)) {
      try {
        await api.delete(`/weather/${city}`);
        loadWeatherData();
      } catch (error) {
        alert(`Failed to delete weather data for "${city}".`);
      }
    }
  };

  const handleRefresh = async (city) => {
    try {
      // Trigger backend update for the specific city
      await api.put(`/weather/${city}`);
      loadWeatherData();
    } catch (error) {
      alert(`Failed to refresh weather data for "${city}".`);
    }
  };

  if (loading) {
    return (
      <Container className="text-center mt-5">
        <Spinner animation="border" />
        <p>Loading weather data...</p>
      </Container>
    );
  }

  if (error) {
    return (
      <Container className="text-center mt-5">
        <Alert variant="danger">{error}</Alert>
      </Container>
    );
  }

  return (
    <Container>
      <h2 className="text-center my-4">City Weather</h2>
      <Row>
        {weatherData.map((weather) => (
          <Col key={weather.id} md={4} className="mb-4">
            <Card className="shadow-sm">
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

                {/* Buttons for Delete and Refresh */}
                <div className="d-flex justify-content-between mt-3">
                  <Button
                    variant="danger"
                    onClick={() => handleDelete(weather.city)}
                  >
                    Delete
                  </Button>
                  <Button
                    variant="info"
                    onClick={() => handleRefresh(weather.city)}   // Refresh button
                  >
                    Refresh
                  </Button>
                </div>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  );
};

export default WeatherList;
