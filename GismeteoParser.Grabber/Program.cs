using GismeteoParser.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GismeteoParser.Grabber
{
    class Program
    {
        private static IHost _host = Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        static void Main(string[] args)
        {
            _host.Services.GetRequiredService<CityWeatherDbContext>().Database.MigrateAsync();
            //await scopedServices.ServiceProvider.GetRequiredService<DbInitializer>().InitAsync();
            _host.Start();

            //_host.Services.

            var gp = new GismeteoParser();
            var w = gp.GetTopCitiesWeather();
            Console.WriteLine(string.Join("\r\n\r\n", w));

            Console.ReadKey();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host_builder = Host.CreateDefaultBuilder(args);
            host_builder.UseContentRoot(Environment.CurrentDirectory);
            host_builder.ConfigureAppConfiguration((host, cfg) =>
            {
                cfg.SetBasePath(Environment.CurrentDirectory);
            });
            host_builder.ConfigureServices(ConfigureServices);
            return host_builder;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddDbContext<CityWeatherDbContext>(optAction =>
            {
                optAction.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GismeteoParser.db;Integrated Security=True");
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}
