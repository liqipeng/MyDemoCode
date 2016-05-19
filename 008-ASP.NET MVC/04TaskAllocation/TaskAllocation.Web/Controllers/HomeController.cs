using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAllocation.Core.DAL;

namespace TaskAllocation.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        [Authorize]
        public ActionResult Index()
        {
            using (TaskAllocationDBContext ctx = new TaskAllocationDBContext())
            {
                var r2 = ctx.TaskItems.ToList();
                var r3 = ctx.Users.ToList();
                var r4 = ctx.Tasks.ToList();
            }

            ViewBag.IsLogon = IsLogon();

            return View();
        }

    }
}
