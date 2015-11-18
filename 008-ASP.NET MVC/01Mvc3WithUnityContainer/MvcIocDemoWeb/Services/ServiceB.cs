using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcIocDemo.Services
{
    public class ServiceB : IServiceB
    {
        #region IServiceB Members

        public string Write()
        {
            return "I am Service B.";
        }

        #endregion
    }
}