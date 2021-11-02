using Autofac;
using Autofac.Integration.Mvc;
using GismeteoParser.ServiceClient;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GismeteoParser.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            //.WithParameter("serviceUri", new Uri("https://localhost:44361/"));
            builder.Register(c => new WeatherClient(new Uri("https://localhost:44361/"))).As<IWeatherClient>();
            //builder.RegisterType<WeatherClient>().As<IWeatherClient>()
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
