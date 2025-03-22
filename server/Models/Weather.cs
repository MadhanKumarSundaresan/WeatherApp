using System.ComponentModel.DataAnnotations;

namespace Banyan.Test.WeatherAPI
{
    public class Weather
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
