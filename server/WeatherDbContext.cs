using Microsoft.EntityFrameworkCore;

namespace Banyan.Test.WeatherAPI
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<Weather> Weather { get; set; }
    }
}
