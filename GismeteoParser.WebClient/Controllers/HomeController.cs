using GismeteoParser.Service;
using GismeteoParser.WebClient.Models;
using System;
using System.Web.Mvc;

namespace GismeteoParser.WebClient.Controllers
{
    public class HomeController : Controller
    {
        const string _mySqlConnString = "server=localhost;user=root;password=root;database=GismeteoParser.db;";
        const string _mySqlServerVersion = "5.7.36";
        GismeteoDataService gismeteoService = new GismeteoDataService();

        public HomeController()
        {
            gismeteoService.ConnectToMySql(_mySqlConnString, _mySqlServerVersion);
        }

        public ActionResult Index()
        {
            ViewBag.Cities = gismeteoService.GetPopCities();
            return View();
        }

        [HttpGet]
        public ActionResult GetDates(string city)
        {
            ViewBag.CityDates = new CityDates()
            {
                CityName = city,
                Dates = gismeteoService.GetDates(city)
            };  
            return View("Dates");
        }

        [HttpGet]
        public ActionResult GetWeather(string city, string date)
        {
            var dt = DateTime.Parse(date);
            ViewBag.Weather = gismeteoService.GetOneDayWeather(city, dt);
            return View("Weather");
        }
    }
}
