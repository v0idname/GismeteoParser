using GismeteoParser.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GismeteoParser.Grabber
{
    class Program
    {
        const string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GismeteoParser.db;Integrated Security=True";

        static void Main(string[] args)
        {
            Console.WriteLine("Grabber запущен, идёт обновление БД...");

            var dbContext = new CityWeatherDbContext(new DbContextOptionsBuilder<CityWeatherDbContext>().UseSqlServer(_connectionString).Options);
            dbContext.Database.Migrate();

            var gp = new GismeteoParser();
            var citiesWeather = gp.GetTopCitiesWeather();
            //Console.WriteLine(string.Join("\r\n\r\n", citiesWeather));

            dbContext.CitiesWeather.RemoveRange(dbContext.CitiesWeather);
            foreach (var cityWeather in citiesWeather)
                dbContext.CitiesWeather.Add(cityWeather);
            dbContext.SaveChanges();

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
                optAction.UseSqlServer(_connectionString);
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}
