using GismeteoParser.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GismeteoParser.WebService.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IRepository<CityWeather> _citiesWeatherRepo;
        private readonly IRepository<OneDayWeather> _oneDayWeathersRepo;

        public ValuesController(IRepository<CityWeather> CitiesWeatherRepo, IRepository<OneDayWeather> OneDayWeathersRepo)
        {
            _citiesWeatherRepo = CitiesWeatherRepo;
            _oneDayWeathersRepo = OneDayWeathersRepo;
        }

        public IEnumerable<string> GetPopCities()
        {
            return _citiesWeatherRepo.Items
                .Select(s => s.CityName);
        }

        public IEnumerable<DateTime> GetDates(string cityName)
        {
            return _oneDayWeathersRepo.Items
                .Where(s => s.CityWeather.CityName == cityName)
                .Select(s => s.Date);
        }

        public OneDayWeather GetOneDayWeather(string cityName, DateTime date)
        {
            return _oneDayWeathersRepo.Items
                .Include(s => s.CityWeather)
                .Where(s => s.CityWeather.CityName == cityName)
                .Where(s => s.Date.Day == date.Day && s.Date.Month == date.Month)
                .FirstOrDefault();
        }
    }
}
