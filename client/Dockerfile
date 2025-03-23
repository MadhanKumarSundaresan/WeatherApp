# Use Node.js to build the React app
FROM node:22.14 AS build
WORKDIR /app

# Install dependencies and build the app
COPY ./package.json ./
COPY ./package-lock.json ./
RUN npm install
COPY . .

RUN npm run build

# Use NGINX to serve the React app
FROM nginx:1.25
COPY --from=build /app/build /usr/share/nginx/html

# Expose port 80
EXPOSE 80
