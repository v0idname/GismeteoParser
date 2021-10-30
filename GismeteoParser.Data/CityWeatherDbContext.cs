using Microsoft.EntityFrameworkCore;

namespace GismeteoParser.Data
{
    public class CityWeatherDbContext : DbContext
    {
        public DbSet<CityWeather> CitiesWeather { get; set; }
        public DbSet<OneDayWeather> OneDayWeathers { get; set; }

        public CityWeatherDbContext(DbContextOptions<CityWeatherDbContext> options) : base(options)
        {

        }
    }
}
