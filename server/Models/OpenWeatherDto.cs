namespace Banyan.Test.WeatherAPI
{
    public class OpenWeatherResponse
    {
        public OpenWeatherMain Main { get; set; } = new();
        public List<OpenWeatherWeather> Weather { get; set; } = new();
        public string Name { get; set; } = string.Empty;  // City name
        public long Dt { get; set; }  // Timestamp

        public class OpenWeatherMain
        {
            public double Temp { get; set; }
            public int Humidity { get; set; }
        }

        public class OpenWeatherWeather
        {
            public string Description { get; set; } = string.Empty;
        }
    }
}
