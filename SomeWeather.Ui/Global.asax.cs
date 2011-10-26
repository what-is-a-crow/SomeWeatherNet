using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Document;
using SomeWeather.Core;
using SomeWeather.Db;
using SomeWeather.Ui.Utility;
using StructureMap;

namespace SomeWeather.Ui
{
    public class MvcApplication : HttpApplication
    {
        private static NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        private static DocumentStore _documentStore = new DocumentStore();

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Main", "{action}", new {controller = "Main", action = "Index"});

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Main", action = "Index", id = UrlParameter.Optional }
                );
        }

        protected void Application_Start()
        {
            _log.Info("Application starting.");

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            _documentStore.Url = ConfigurationManager.AppSettings[Constants.Config.DbUrl];
            _documentStore.Initialize();

            var container = ConfigureIoC();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }

        private static IContainer ConfigureIoC()
        {
            var configuration = new Action<ConfigurationExpression>(
                registry =>
                {
                    registry.Scan(
                        x =>
                        {
                            x.TheCallingAssembly();
                            x.AssembliesFromApplicationBaseDirectory(
                                a => a.GetCustomAttributes(typeof(SupportsIoC), true).Any());
                            x.WithDefaultConventions();
                            x.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
                        });
                    registry.For(typeof(IRepository<>)).Use(typeof(RavenRepository<>));

                    registry.For<IDocumentStore>().Use(_documentStore);
                    registry.For<IDocumentSession>().Use<DocumentSession>();
                });

            ObjectFactory.Configure(configuration);
            return new Container(configuration);
        }
    }
}