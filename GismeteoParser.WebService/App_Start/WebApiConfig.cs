using Autofac;
using Autofac.Integration.WebApi;
using GismeteoParser.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;
using System.Web.Http;

namespace GismeteoParser.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var builder = new ContainerBuilder();

            builder.RegisterType<CitiesWeatherRepository>().As<IRepository<CityWeather>>();
            builder.RegisterType<OneDayWeatherRepository>().As<IRepository<OneDayWeather>>();
            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<CityWeatherDbContext>();
                opt.UseMySql(ConfigurationManager.ConnectionStrings["mysql"].ConnectionString);
                return new CityWeatherDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
