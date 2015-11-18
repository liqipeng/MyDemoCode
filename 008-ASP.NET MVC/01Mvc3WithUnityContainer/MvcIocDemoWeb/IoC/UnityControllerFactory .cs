using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using System.Web.SessionState;

namespace MvcIocDemo.IoC
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        IUnityContainer container;
        public UnityControllerFactory(IUnityContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext reqContext,
            Type controllerType)
        {
            return container.Resolve(controllerType) as IController;
        }

    }

}