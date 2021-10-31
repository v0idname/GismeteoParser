using GismeteoParser.Data;
using GismeteoParser.Grabber.HtmlGetters;
using GismeteoParser.Grabber.Parsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Linq;

namespace GismeteoParser.Grabber
{
    class Program
    {
        const string _mssqlConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GismeteoParser.db;Integrated Security=True";
        const string _mySqlConnString = "server=localhost;user=root;password=root;database=GismeteoParser.db;";
        const string _mySqlServerVersion = "5.7.36";
        const string URL = "https://www.gismeteo.ru/";
        const string TIME_RANGE_POSTFIX = "10-days/";

        static void Main(string[] args)
        {
            Console.WriteLine($"Получаем список популярных городов с {URL}...");

            IHtmlGetter htmlGetter = new EoWebBrowserHtmlGetter();
            var htmlString = htmlGetter.GetHtmlByUrl(URL);
            IPopCitiesParser popCitiesParser = new PopCitiesJsParser();
            var popCitiesLinks = popCitiesParser.GetPopCitiesLinks(htmlString);

            Console.WriteLine("Идёт обновление БД...");

            //var dbContext = new CityWeatherDbContext(new DbContextOptionsBuilder<CityWeatherDbContext>().UseMySql(
            //    _mySqlConnString, s => s.ServerVersion(new ServerVersion(_mySqlServerVersion))).Options);
            //dbContext.Database.Migrate();
            //dbContext.CitiesWeather.RemoveRange(dbContext.CitiesWeather);

            ICityWeatherParser cityWeatherParser = new TenDaysCityWeatherParser();
            htmlGetter = new WebClientHtmlGetter();
            var popCities10DaysLinks = popCitiesLinks.Select(l => URL + l + TIME_RANGE_POSTFIX);
            foreach (var cityLink in popCities10DaysLinks)
            {
                var cityHtml = htmlGetter.GetHtmlByUrl(cityLink);
                var cityWeather = cityWeatherParser.GetCityWeather(cityHtml);
                Console.WriteLine(cityWeather);
                //dbContext.CitiesWeather.Add(cityWeather);
            }
            //dbContext.SaveChanges();
            //dbContext.Dispose();

            Console.WriteLine("БД обновлена, нажмите любую клавишу для выхода...");
            
            Console.ReadKey();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<CityWeatherDbContext>(optAction =>
            {
                optAction.UseMySql(_mySqlConnString, s => s.ServerVersion(new ServerVersion(_mySqlServerVersion)));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}
