using GismeteoParser.ServiceClient;
using GismeteoParser.WebClient.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GismeteoParser.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherClient _weatherClient;

        public HomeController(IWeatherClient weatherClient)
        {
            _weatherClient = weatherClient;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Cities = await _weatherClient.GetPopCitiesAsync();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetDates(string city)
        {
            ViewBag.CityDates = new CityDates()
            {
                CityName = city,
                Dates = await _weatherClient.GetDatesAsync(city)
            };  
            return View("Dates");
        }

        [HttpGet]
        public async Task<ActionResult> GetWeather(string city, string date)
        {
            var dt = DateTime.Parse(date);
            ViewBag.Weather = await _weatherClient.GetOneDayWeatherAsync(city, dt);
            return View("Weather");
        }
    }
}
