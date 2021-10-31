using GismeteoParser.Service;
using System.Web.Mvc;

namespace GismeteoParser.WebClient.Controllers
{
    public class HomeController : Controller
    {
        const string _mySqlConnString = "server=localhost;user=root;password=root;database=GismeteoParser.db;";
        const string _mySqlServerVersion = "5.7.36";

        public ActionResult Index()
        {
            var gismeteoService = new GismeteoDataService();
            gismeteoService.ConnectToMySql(_mySqlConnString, _mySqlServerVersion);
            ViewBag.Cities = gismeteoService.GetPopCities();
            return View();
        }
    }
}
