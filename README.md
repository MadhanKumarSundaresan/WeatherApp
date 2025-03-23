
# 🌦 Weather Dashboard App

This is a **Full-Stack Weather Application** built with:
- 🌐 **Frontend:** React.js (client)
- ⚙️ **Backend:** .NET 8 Web API with SQLite
- 🐳 **Docker** for containerization

---

## 📌 Features
- 🌍 Fetches weather data using **OpenWeather API**
- 📊 Stores weather information in an **SQLite database**
- 🔑 RESTful API endpoints
- 🌦 Interactive React-based weather dashboard
- 🚀 Deployable using Docker and Docker Compose

---

## 🐳 **Run with Docker**
You can containerize the entire application using Docker.

### ✅ **1. Install Docker**
- Download and install **Docker** → [Docker Official Site](https://www.docker.com/)

### ✅ **2. Build and Run with Docker Compose**
```bash
docker-compose up --build
```
- **Frontend:** `http://localhost:3000`  
- **Backend API:** `http://localhost:5102`  

### ✅ **3. Stop the Containers**
```bash
docker-compose down
```

---

## 🔥 **Environment Variables(OPTIONAL)**
The application supports environment variables for customization.

| Variable                  | Description                  | Default Value           |
|---------------------------|------------------------------|-------------------------|
| `ASPNETCORE_ENVIRONMENT`  | API environment mode         | Development             |
| `ASPNETCORE_URLS`         | URL where the API runs       | `http://+:5102`         |
| `REACT_APP_API_URL`       | Frontend API URL             | `http://localhost:5102` |

---

## 🎯 **Project Structure**
```
📂 WeatherApp
 ┣ 📂 client              # React frontend
 ┣ 📂 server              # .NET Core backend
 ┣ 📜 docker-compose.yml  # Docker Compose file
 ┣ 📜 SERVER/Dockerfile          # Backend Docker config
 ┣ 📜 README.md           # Documentation
 ┗ 📜 weather.db          # SQLite database file(Due to some issues in EF with docker, commited the db file)
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
docker exec -it weather-api-server-1 dotnet ef database update
```

---

## 📜 **License**
MIT License.

---

## 🤝 **Contributing**
Pull requests are welcome! For major changes, open an issue first.

---

## ✨ **Author**
Developed by **Madhan Kumar S**.
# WeatherApp