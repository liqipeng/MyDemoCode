using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAllocation.Core.Utils;

namespace TaskAllocation.Web.Controllers
{
    public class JsUtilTestController : Controller
    {
        //
        // GET: /JsUtilTest/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UseLog4Net() 
        {
            LogUtil.Info("Hello, world!");

            return View();
        }

    }
}
