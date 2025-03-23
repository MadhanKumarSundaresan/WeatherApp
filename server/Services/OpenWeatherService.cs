using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Banyan.Test.WeatherAPI;
using Microsoft.Extensions.Configuration;

public class OpenWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _apiKey;

    private readonly string _hashSecret;

    public OpenWeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        // Load API settings from appsettings.json
        _baseUrl = configuration["OpenWeather:BaseUrl"] ?? throw new ArgumentNullException("Base URL is missing");
        _apiKey = configuration["OpenWeather:ApiKey"] ?? throw new ArgumentNullException("API Key is missing");
        _hashSecret = configuration["HashSecret"] ?? throw new ArgumentNullException("API Key is missing");
    }

    /// <summary>
    /// Fetches weather data from OpenWeather API.
    /// </summary>
    public async Task<Weather?> GetWeatherAsync(string city)
    {
        try
        {
            var url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get weather for {city}: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            
            var data = JsonSerializer.Deserialize<OpenWeatherResponse>(json);

            if (data == null || data.Weather == null || data.Weather.Count == 0)
            {
                return null;
            }

            // Map API response to your Weather entity
            return new Weather
            {
                Id = new Random().Next(100000, 999999),
                City = data.Name,
                Description = data.Weather[0].Description,
                Temperature = data.Main.Temp,
                Humidity = data.Main.Humidity,
                LastUpdated = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception while fetching weather: {ex.Message}");
            return null;
        }
    }
}
