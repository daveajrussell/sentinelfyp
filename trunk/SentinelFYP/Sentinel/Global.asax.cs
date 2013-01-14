using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using DomainModel.Interfaces.Services;
using DomainModel.Services;
using DomainModel.Interfaces.Repositories;
using SqlRepositories;
using DomainModel.Interfaces;
using NinjectResolver;
using Ninject;
using Sentinel.Helpers;
using Microsoft.AspNet.SignalR;

namespace Sentinel
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "Login", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            // Register the default hubs route: ~/signalr/hubs
            RouteTable.Routes.MapHubs();         

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            SetupDependencyInjection();
        }

        private void SetupDependencyInjection()
        {
            var baseDirectory = HttpRuntime.AppDomainAppPath;
            baseDirectory = baseDirectory + "Content\\etc\\";

            var _connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            var context = HttpContext.Current;

            IKernel kernel = new StandardKernel();

            kernel.Bind<ISettingsService>().To<SettingsService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISecurityService>().To<SecurityService>();
            kernel.Bind<IGHeatService>().To<GHeatService>();
            kernel.Bind<IPointService>().To<PointService>();
            kernel.Bind<IGISService>().To<GISService>();
            kernel.Bind<IWeightHandler>().To<IWeightHandler>();

            kernel.Bind<ISettingsRepository>().To<SettingsRepository>().WithConstructorArgument("baseDirectory", baseDirectory);
            kernel.Bind<IGHeatRepository>().To<GHeatRepository>();
            kernel.Bind<IPointRepository>().To<SqlPointRepository>().WithConstructorArgument("connectionString", _connectionString);
            kernel.Bind<IUserRepository>().To<SqlUserRepository>().WithConstructorArgument("connectionString", _connectionString);
            kernel.Bind<ISecurityRepository>().To<SqlSecurityRepository>().WithConstructorArgument("connectionString", _connectionString);
            kernel.Bind<IGISRepository>().To<SqlGISRepository>().WithConstructorArgument("connectionString", _connectionString);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}