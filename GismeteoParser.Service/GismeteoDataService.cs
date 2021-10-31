using GismeteoParser.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GismeteoParser.Service
{
    public class GismeteoDataService : IDisposable
    {
        private CityWeatherDbContext _dbContext;

        public void ConnectToMySql(string connectionString, string serverVersion)
        {
            _dbContext = new CityWeatherDbContext(new DbContextOptionsBuilder<CityWeatherDbContext>().UseMySql(
                connectionString, s => s.ServerVersion(new ServerVersion(serverVersion))).Options);
        }

        public IEnumerable<string> GetPopCities()
        {
            return _dbContext.CitiesWeather
                .Select(s => s.CityName);
        }

        public IEnumerable<DateTime> GetDates(string cityName)
        {
            return _dbContext.OneDayWeathers
                .Where(s => s.CityWeather.CityName == cityName)
                .Select(s => s.Date);
        }

        public OneDayWeather GetOneDayWeather(string cityName, DateTime date)
        {
            return _dbContext.OneDayWeathers.Include(s => s.CityWeather)
                .Where(s => s.CityWeather.CityName == cityName)
                .Where(s => s.Date.Day == date.Day && s.Date.Month == date.Month)
                .FirstOrDefault();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
