using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Banyan.Test.WeatherAPI;
using System.Net.Http.Json;

namespace Banyan.Test.WeatherAPI
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class WeatherController : ControllerBase
    {
    private readonly OpenWeatherService _weatherService;
        private readonly IWeatherRepository _weatherRepo;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public WeatherController(IWeatherRepository weatherRepo, IConfiguration config, OpenWeatherService weatherService)
        { 
            _weatherRepo = weatherRepo;
            _config = config;
            _weatherService = weatherService;
            _httpClient = new HttpClient();

            // Read base URL and API key from appsettings.json
            _baseUrl = _config["OpenWeather:BaseUrl"]!;
            _apiKey = _config["OpenWeather:ApiKey"]!;
        }

        // Get all cities
        [HttpGet]
        public async Task<IActionResult> GetAllWeather()
        {
            var weather = await _weatherRepo.GetAllAsync();
            return Ok(weather);
        }

        // Get weather by city
        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weather = await _weatherRepo.GetByCityAsync(city);
            if (weather == null) return NotFound();
            
            return Ok(weather);
        }

        // Add new weather data (fetch from OpenWeather API)
        [HttpPost]
public async Task<IActionResult> AddWeather([FromBody] WeatherRequestDto request)
{
    if (string.IsNullOrWhiteSpace(request.City))
    {
        return BadRequest("City is required.");
    }
    
    // Map API response to Weather model
    var weather = await _weatherService.GetWeatherAsync(request.City);
if (weather == null)
    {
        return NotFound("City not found.");
    }
    // Add or update the weather data
    await _weatherRepo.AddOrUpdateAsync(weather);
    await _weatherRepo.SaveChangesAsync();

    return CreatedAtAction(nameof(GetWeather), new { city = weather.City }, weather);
}

        // Update weather data for a city
        [HttpPut("{city}")]
        public async Task<IActionResult> UpdateWeather(string city)
        {
            var weather = await _weatherRepo.GetByCityAsync(city);
            if (weather == null) return NotFound();

            var apiKey = _config["OpenWeather:ApiKey"];
            var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}");

            weather.Description = response.Weather.FirstOrDefault()?.Description ?? "No description";
            weather.Temperature = Math.Round((response.Main.Temp - 273.15), 2);
            weather.Humidity = response.Main.Humidity;
            weather.LastUpdated = DateTimeOffset.FromUnixTimeSeconds(response.Dt).UtcDateTime;


            await _weatherRepo.UpdateAsync(weather);
            await _weatherRepo.SaveChangesAsync();

            return Ok(weather);
        }

        // Delete weather data for a city
        [HttpDelete("{city}")]
        public async Task<IActionResult> DeleteWeather(string city)
        {
            var weather = await _weatherRepo.GetByCityAsync(city);
            if (weather == null) return NotFound();

            await _weatherRepo.DeleteAsync(city);
            await _weatherRepo.SaveChangesAsync();
            
            return NoContent();
        }
    }
}