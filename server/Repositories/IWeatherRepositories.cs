using Banyan.Test.WeatherAPI;

namespace Banyan.Test.WeatherAPI
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<Weather>> GetAllAsync();
        Task<Weather?> GetByCityAsync(string city);
        Task AddAsync(Weather weather);
        Task AddOrUpdateAsync(Weather weather);
        Task UpdateAsync(Weather weather);
        Task DeleteAsync(string city);
        Task SaveChangesAsync();
    }
}
