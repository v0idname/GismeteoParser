using Microsoft.EntityFrameworkCore;

namespace GismeteoParser.Data
{
    public class CityWeatherDbContext : DbContext
    {
        public DbSet<CityWeather> CitiesWeather { get; set; }
        //public DbSet<OneDayWeather> OneDayWeathers { get; set; }

        public CityWeatherDbContext(DbContextOptions<CityWeatherDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityWeather>()
                .HasMany(c => c.DaysWeather)
                .WithOne(x => x.CityWeather)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
