using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Banyan.Test.WeatherAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace WeatherApp.Tests
{
    public class WeatherControllerTests
    {
        private readonly Mock<IWeatherRepository> _mockRepo;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly WeatherController _controller;

        public WeatherControllerTests()
        {
            _mockRepo = new Mock<IWeatherRepository>();
            _mockConfig = new Mock<IConfiguration>();

            // Mock Configuration
            _mockConfig.Setup(c => c["OpenWeather:ApiKey"]).Returns("fake_api_key");
            _mockConfig.Setup(c => c["OpenWeather:BaseUrl"]).Returns("https://api.openweathermap.org/data/2.5/weather");

            // Setup HttpContext with fake authentication
            var httpContext = new DefaultHttpContext();
            var services = new ServiceCollection();

            // Add fake authentication scheme
            services.AddAuthentication("TestScheme")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

            // Add fake authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("TestPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            var serviceProvider = services.BuildServiceProvider();
            httpContext.RequestServices = serviceProvider;

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            _controller = new WeatherController(_mockRepo.Object, _mockConfig.Object)
            {
                ControllerContext = controllerContext
            };
        }

        // ✅ Test: Get All Weather (Bypassing Authorization)
        [Fact]
        public async Task GetAllWeather_ReturnsOk_WithWeatherData()
        {
            // Arrange
            var weatherList = new List<Weather>
            {
                new Weather { City = "New York", Temperature = 25.5, Description = "Sunny", Humidity = 60 },
                new Weather { City = "Los Angeles", Temperature = 22.3, Description = "Cloudy", Humidity = 55 }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(weatherList);

            // Act
            var result = await _controller.GetAllWeather() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);

            var model = result.Value as IEnumerable<Weather>;
            model.Should().HaveCount(2);
        }

        // ✅ Test: Get Weather by City
        [Fact]
        public async Task GetWeather_ReturnsOk_WhenCityExists()
        {
            // Arrange
            var city = "New York";
            var weather = new Weather { City = city, Temperature = 25.5, Description = "Sunny", Humidity = 60 };

            _mockRepo.Setup(repo => repo.GetByCityAsync(city)).ReturnsAsync(weather);

            // Act
            var result = await _controller.GetWeather(city) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            var model = result.Value as Weather;
            model.City.Should().Be(city);
        }
    }
}
