using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject.Parameters;
using Ninject.Syntax;
using Ninject;

namespace NinjectResolver
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IResolutionRoot resolutionRoot)
        {
            if (resolutionRoot == null)
                throw new ArgumentNullException("resolution root");

            _resolutionRoot = resolutionRoot;
        }

        public object GetService(Type serviceType)
        {
            var request = _resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return _resolutionRoot.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType).ToList();
        }
    }
}
