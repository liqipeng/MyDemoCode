using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcIocDemo.Services
{
    public class ServiceA : IServiceA
    {
        #region IServiceA Members

        public string Say()
        {
            return "I am Service A.";
        }

        #endregion
    }
}