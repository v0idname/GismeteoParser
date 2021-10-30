using GismeteoParser.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GismeteoParser.Grabber
{
    class Program
    {
        private static IHost _host = CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        static void Main(string[] args)
        {
            _host.Services.GetRequiredService<CityWeatherDbContext>().Database.Migrate();
            _host.Start();

            var gp = new GismeteoParser();
            var w = gp.GetTopCitiesWeather();
            Console.WriteLine(string.Join("\r\n\r\n", w));

            Console.ReadKey();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<CityWeatherDbContext>(optAction =>
            {
                optAction.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GismeteoParser.db;Integrated Security=True");
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}
