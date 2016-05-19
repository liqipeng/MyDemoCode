using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskAllocation.Web.Controllers
{
    public class CommonController : BaseController
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NoPermission() 
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
