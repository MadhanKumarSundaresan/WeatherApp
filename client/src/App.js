import React, { useState } from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import WeatherList from "./components/WeatherList";
import CitySearch from "./components/CitySearch";
import CityForm from "./components/CityForm";
import NavigationBar from "./components/Navbar";

const App = () => {
  const [refreshData, setRefreshData] = useState(false);

  const triggerRefresh = () => setRefreshData(!refreshData);

  return (
    <BrowserRouter>
      <NavigationBar />
      <Routes>
        <Route path="/" element={<Login />} />
        <Route
          path="/dashboard"
          element={
            <>
              <CitySearch refreshData={triggerRefresh} />
              <CityForm refreshData={triggerRefresh} />
              <WeatherList refreshData={refreshData} />
            </>
          }
        />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
