using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using System.Web.Mvc;
using TaskAllocation.Core.Models;
using TaskAllocation.Web.Models;
using TaskAllocation.Web.Mvc;

namespace TaskAllocation.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}