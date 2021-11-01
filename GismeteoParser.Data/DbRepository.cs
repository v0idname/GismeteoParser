using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GismeteoParser.Data
{
    public abstract class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private CityWeatherDbContext _db;
        private DbSet<T> _Set;

        public virtual IQueryable<T> Items => _Set;

        public DbRepository(CityWeatherDbContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }
    }

    public class CitiesWeatherRepository : DbRepository<CityWeather>
    {
        public CitiesWeatherRepository(CityWeatherDbContext db) : base(db)
        {
        }
    }

    public class OneDayWeatherRepository : DbRepository<OneDayWeather>
    {
        public OneDayWeatherRepository(CityWeatherDbContext db) : base(db)
        {
        }

        public override IQueryable<OneDayWeather> Items => base.Items.Include(c => c.CityWeather);
    }
}
