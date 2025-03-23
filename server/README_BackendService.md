
# 🌦 Weather API Backend

This is the **.NET 8 Web API** for managing weather data using **SQLite** as the database.

## 📌 Features
- 🔑 **Authentication** endpoint for user login
- 🌍 CRUD operations for **weather data**
- 📊 **OpenAPI** documentation via Swagger
- 🚀 **Dockerized** for easy deployment

---

## 🚀 **How to Run Locally (Without Docker)**

### ✅ **1. Prerequisites**
Ensure you have:
- **.NET SDK 8.0+** installed → [Download .NET 8](https://dotnet.microsoft.com/en-us/download)
- **SQLite** installed (or use the provided `weather.db`)

### ✅ **2. Clone the Repository**
```bash
git clone https://github.com/yourusername/weather-api.git
cd weather-api
```

### ✅ **3. Restore and Build the App**
```bash
dotnet restore
dotnet build
```

### ✅ **4. Apply Database Migrations**
```bash
dotnet ef database update
```

### ✅ **5. Run the API**
```bash
dotnet run --project Banyan.Test.WeatherAPI.csproj
```
- **API available at:** `http://localhost:5102`
- **Swagger UI:** `http://localhost:5102/swagger`

---

## 🐳 **Run with Docker**
You can containerize the application using Docker.

### ✅ **1. Install Docker**
- Download and install **Docker** → [Docker Official Site](https://www.docker.com/)

### ✅ **2. Build and Run the Docker Container**
```bash
docker build -t weather-api .
docker run -d -p 5102:5102 --name weather-api-container weather-api
```
- **API available at:** `http://localhost:5102`

---

## 🔥 **API Endpoints**
### 🔑 **Authentication**
- `POST /api/v1/Auth/login`
  - Authenticate user by providing username and password.
  - **Request Body:**  
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```
  - **Response:** `200 OK`

---

### 🌦 **Weather Data**
- `GET /api/v1/Weather`
  - Get all weather records.
  - **Response:** `200 OK`

- `POST /api/v1/Weather`
  - Add a new city weather.
  - **Request Body:**  
    ```json
    {
      "city": "string"
    }
    ```
  - **Response:** `200 OK`

- `GET /api/v1/Weather/{city}`
  - Get weather by city name.
  - **Parameters:**
    - `city` (path param) → `string` (required)
  - **Response:** `200 OK`

- `PUT /api/v1/Weather/{city}`
  - Update weather for a specific city.
  - **Parameters:**
    - `city` (path param) → `string` (required)
  - **Response:** `200 OK`

- `DELETE /api/v1/Weather/{city}`
  - Delete weather record by city name.
  - **Parameters:**
    - `city` (path param) → `string` (required)
  - **Response:** `200 OK`

---

## 🛠 **Models**
### ✅ **User**
```json
{
  "userId": 1,
  "username": "string",
  "passwordHash": "string",
  "createdAt": "2025-03-23T00:00:00Z",
  "password": "string"
}
```

### ✅ **WeatherRequestDto**
```json
{
  "city": "string"
}
```

---

## 🔥 **Environment Variables**
The API supports environment variables for customization.

| Variable                  | Description                  | Default Value           |
|---------------------------|------------------------------|-------------------------|
| `ASPNETCORE_ENVIRONMENT`  | API environment mode         | Development             |
| `ASPNETCORE_URLS`         | URL where the API runs       | `http://+:5102`         |

---

## 📌 **Database Configuration**
- The backend uses **SQLite** with the database file `weather.db`
- To reset the database:
```bash
dotnet ef database update
```

---

## 🎯 **Project Structure**
```
📂 WeatherAPI
 ┣ 📂 Controllers         # API Controllers
 ┣ 📂 Models              # Entity models
 ┣ 📂 Data                # DB Context and Migrations
 ┣ 📂 Services            # Business logic
 ┣ 📜 appsettings.json    # Configurations
 ┣ 📜 Dockerfile          # Docker configuration
 ┣ 📜 docker-compose.yml  # Docker Compose file
 ┗ 📜 Program.cs          # Entry point
```

---

## 🛠 **Troubleshooting**
### ❌ Port Already in Use?
```bash
netstat -ano | findstr :5102
```
Kill the process using:
```bash
taskkill /PID <pid> /F
```

### ❌ Error: `No .NET SDKs were found`
Ensure **.NET SDK 8+** is installed.

### ❌ Database Not Updating in Docker?
Run migrations manually inside the container:
```bash
docker exec -it weather-api-container dotnet ef database update
```

---

## 📜 **License**
MIT License.

---

## 🤝 **Contributing**
Pull requests are welcome! For major changes, open an issue first.

---

## ✨ **Author**
Developed by **Your Name**.
