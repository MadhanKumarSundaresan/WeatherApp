using Banyan.Test.WeatherAPI;
using Microsoft.EntityFrameworkCore;

namespace Banyan.Test.WeatherAPI
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext _context;

        public WeatherRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Weather>> GetAllAsync()
        {
    return await _context.Weather
        .GroupBy(w => w.City)                              // Group by City
        .Select(g => g.OrderByDescending(w => w.LastUpdated)  // Select latest weather record
                      .First())
        .ToListAsync();
        }

        public async Task<Weather?> GetByCityAsync(string city)
        {
            return await _context.Weather.FirstOrDefaultAsync(w => w.City.ToLower() == city.ToLower());
        }

        public async Task AddAsync(Weather weather)
        {
            await _context.Weather.AddAsync(weather);
        }

        public async Task AddOrUpdateAsync(Weather weather)
{
    // Check if the city already exists
    var existingWeather = await _context.Weather
        .FirstOrDefaultAsync(w => w.City == weather.City);

    if (existingWeather != null)
    {
        // Update existing record
        existingWeather.Description = weather.Description;
        existingWeather.Temperature = weather.Temperature;
        existingWeather.Humidity = weather.Humidity;
        existingWeather.LastUpdated = DateTime.UtcNow;  // Set the latest timestamp
    }
    else
    {
        // Add new weather record
        weather.LastUpdated = DateTime.UtcNow;  // Set the timestamp for new entry
        await _context.Weather.AddAsync(weather);
    }

    await _context.SaveChangesAsync();  // Commit changes
}

        public async Task UpdateAsync(Weather weather)
        {
            _context.Weather.Update(weather);
        }

        public async Task DeleteAsync(string city)
        {
            var weather = await GetByCityAsync(city);
            if (weather != null)
            {
                _context.Weather.Remove(weather);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
