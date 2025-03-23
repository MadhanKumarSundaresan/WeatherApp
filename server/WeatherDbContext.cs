using Microsoft.EntityFrameworkCore;

namespace Banyan.Test.WeatherAPI
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<Weather> Weather { get; set; }
    public DbSet<User> Users { get; set; }
    private readonly OpenWeatherService _weatherService;
    private readonly PasswordHasher _passwordHashService;
        
        
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options, OpenWeatherService weatherService, PasswordHasher passwordHashService)
        : base(options)
    {
        _weatherService = weatherService;
        _passwordHashService = passwordHashService;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.ConfigureWarnings(warnings => 
        warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
}
         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        var adminPassword = "admin@123";
        var adminHash = _passwordHashService.HashPassword(adminPassword);

        // Seed the admin user
        modelBuilder.Entity<User>().HasData(new User
        {
            UserId = 1,
            Username = "admin",
            PasswordHash = adminHash,
            CreatedAt = DateTime.UtcNow
        });

        // Seed data from OpenWeather API
        Task.Run(async () => await SeedWeatherDataAsync(modelBuilder)).Wait();
    }

    private async Task SeedWeatherDataAsync(ModelBuilder modelBuilder)
    {
        var cities = new List<string>
        {
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
            "London", "Paris", "Tokyo", "Sydney", "Mumbai"
        };

        var weatherList = new List<Weather>();

        foreach (var city in cities)
        {
            var weather = await _weatherService.GetWeatherAsync(city);
            if (weather != null)
            {
                weatherList.Add(weather);
            }
        }

        modelBuilder.Entity<Weather>().HasData(weatherList);
    }



    }
}
