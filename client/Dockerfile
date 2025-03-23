# Use Node.js to build the React app
FROM node:22.14 AS build
WORKDIR /app

# Install dependencies and build the app
COPY ./package.json ./package-lock.json ./
RUN npm install
COPY . .
RUN npm run build

# Use Node.js to serve the React app
FROM node:22.14 AS static
WORKDIR /app

# Install `serve` to serve static files
RUN npm install -g serve

# Copy the build output
COPY --from=build /app/build ./build

# Serve the app on port 3000
CMD ["serve", "-s", "build", "-l", "3000"]

# Expose port 3000
EXPOSE 3000
