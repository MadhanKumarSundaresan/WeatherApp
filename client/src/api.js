import axios from "axios";

const API_URL = "http://localhost:5102/api";  // Your backend URL

const api = axios.create({
  baseURL: API_URL + "/v1",
  headers: {
    "Content-Type": "application/json"  // Ensures JSON format
  }
});

export const login = async (username, password) => {
  const requestBody = {
    username: username,
    password: password
  };

  const response = await api.post("/auth/login", requestBody);
  return response.data;
};

export default api;
