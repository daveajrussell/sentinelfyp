using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using System.Configuration;
using DomainModel.Interfaces.Services;
using DomainModel.Interfaces.Repositories;
using DomainModel.Services;
using SqlRepositories;

namespace WebServices
{
    public class WcfNinjectModule : NinjectModule
    {
        public override void Load()
        {
            var _connectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            /* SERVICES */
            Bind<IPointService>().To<PointService>();
            Bind<IGISService>().To<GISService>();
            Bind<ISecurityService>().To<SecurityService>();
            Bind<IGeoTaggedDeliveryService>().To<GeoTaggedDeliveryService>();

            /* REPOSITORIES */
            Bind<IPointRepository>().To<SqlPointRepository>().WithConstructorArgument("connectionString", _connectionString);
            Bind<IGISRepository>().To<SqlGISRepository>().WithConstructorArgument("connectionString", _connectionString);
            Bind<ISecurityRepository>().To<SqlSecurityRepository>().WithConstructorArgument("connectionString", _connectionString);
            Bind<IGeoTaggedDeliveryRepository>().To<SqlGeoTaggedDeliveryRepository>().WithConstructorArgument("connectionString", _connectionString);
        }
    }
}