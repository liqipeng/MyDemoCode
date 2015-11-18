using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace MvcIocDemo.IoC
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (!this.container.IsRegistered(serviceType))
            {
                return null;
            }
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.ResolveAll(serviceType);
        }
    }
}