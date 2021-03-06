﻿using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.Configuration;
using DomainModel.Interfaces.Services;
using DomainModel.Services;
using DomainModel.Interfaces.Repositories;
using System.Web.Mvc;
using SqlRepositories;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;
using SentinelExceptionManagement;

namespace WebServices
{
    public class Global : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new WcfNinjectModule());
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            ExceptionManager.LogException(exception);
        }
    }
}
