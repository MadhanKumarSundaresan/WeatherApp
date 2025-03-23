using System.Text.Json.Serialization;

public class OpenWeatherResponse
{
    [JsonPropertyName("main")]
    public OpenWeatherMain Main { get; set; } = new();

    [JsonPropertyName("weather")]
    public List<OpenWeatherWeather> Weather { get; set; } = new();

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;  // City name

    [JsonPropertyName("dt")]
    public long Dt { get; set; }  // Timestamp

    public class OpenWeatherMain
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class OpenWeatherWeather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
