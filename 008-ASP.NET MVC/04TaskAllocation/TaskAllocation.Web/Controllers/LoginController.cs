using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskAllocation.Core.DAL;
using TaskAllocation.Core.DomainModel;

namespace TaskAllocation.Web.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (IsLogon()) 
            {
                return RedirectToAction("Index", "Home");
            }

            //如果是跳转过来的，则返回上一页面ReturnUrl
            //if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
            //{
            //    string returnUrl = Request["ReturnUrl"];
            //    TempData["ReturnUrl"] = returnUrl; 
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            using (TaskAllocationDBContext ctx = new TaskAllocationDBContext())
            {
                User loginUser = ctx.Users.Where(u => u.Name == user.Name && u.Password == user.Password).FirstOrDefault();

                if (loginUser != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Name, false);
                    //存入Session
                    Session["LoginUserName"] = user.Name;

                    return SuccessMessage();
                }
                else 
                {
                    return FailedMessage("用户名或者密码错误。");
                }
            }
        }

        public ActionResult Exit() 
        {
            //取消Session会话
            Session.Abandon();

            //删除Forms验证票证
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");
        }

    }
}
