using Demo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [OutputCache(Duration = 10000)]
        public ActionResult Index()
        {
            Response.Cache.SetOmitVaryStar(true); //加上这个，才是稳定的304状态码；否则，状态码无规律

            var employee = new Employee() { ID = 1, Name = "ares", Gender = "Male", Birthday = DateTime.Now };
            return View(employee);
        }

        [CustomedOutputCache(Duration = 10000, VaryByParam = "Param1")]
        public ActionResult Index2()
        {
            var employee = new Employee() { ID = 1, Name = "ares", Gender = "Male", Birthday = DateTime.Now };
            return View("Index", employee);
        }

        [CustomedOutputCache(Duration = 10000, VaryByParam = "None")]
        //VaryByParam 可能的值包括“none”, "None"、“*”以及任何有效的查询字符串或 POST 参数名
        public ActionResult Index3()
        {
            var employee = new Employee() { ID = 1, Name = "ares", Gender = "Male", Birthday = DateTime.Now };
            return View("Index", employee);
        }

        [CustomedOutputCache(Duration = 10000, VaryByParam = "None", VaryByHeader = "User-Agent")]
        public ActionResult Index4()
        {
            var employee = new Employee() { ID = 1, Name = "ares", Gender = "Male", Birthday = DateTime.Now };
            return View("Index", employee);
        }
    }

    public class CustomedOutputCacheAttribute : OutputCacheAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetOmitVaryStar(true); //加上这个，才是稳定的304状态码；否则，状态码无规律

            base.OnActionExecuting(filterContext);
        }
    }
}
