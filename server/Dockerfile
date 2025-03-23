# Stage 1: Build with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files
COPY ./ ./

# Restore NuGet packages
RUN dotnet restore

# Build and publish the server
RUN dotnet publish -c Release -o out

# Stage 2: Runtime (smaller image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime 
WORKDIR /app
COPY --from=build /app/out ./
COPY weather.db ./weather.db
# Expose the port
EXPOSE 5102
ENV ASPNETCORE_URLS=http://+:5102

# Start the app
ENTRYPOINT ["dotnet", "Banyan.Test.WeatherAPI.dll"]
