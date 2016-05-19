using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAllocation.Core.Common;
using TaskAllocation.Core.DAL;
using TaskAllocation.Core.DomainModel;
using TaskAllocation.Core.Models;
using TaskAllocation.Web.Models;
using TaskAllocation.Web.Models.DTO;
using TaskAllocation.Web.Mvc;
using TaskAllocation.Web.Resources;

namespace TaskAllocation.Web.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            ViewBag.IsLogon = IsLogon();

            return View();
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserSaveObject userDTO)
        {
            SaveUser(userDTO);

            return SuccessMessage(BasicMessage.SubmitSuccess);
        }

        private static void SaveUser(UserSaveObject userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.Name))
            {
                throw new ArgumentNullException("用户名为空。");
            }

            using (TaskAllocationDBContext ctx = new TaskAllocationDBContext())
            {
                if (userDTO.Id.HasValue)
                {
                    var user = ctx.Users.Where(u => u.Id == userDTO.Id).FirstOrDefault();
                    if (user != null)
                    {
                        user.Name = userDTO.Name;
                    }
                    else
                    {
                        throw new ArgumentException("user id is not exist:" + userDTO.Id);
                    }
                }
                else
                {
                    var existCount = ctx.Users.Where(u => u.Name == userDTO.Name).Count();
                    if (existCount > 0)
                    {
                        throw new ArgumentException("用户名" + userDTO.Name + "已存在。");
                    }

                    ctx.Users.Add(new User() { Name = userDTO.Name });
                }

                ctx.SaveChanges();
            }
        }

        public ActionResult Retrieve() 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="rows">单页条数</param>
        /// <returns></returns>
        public ActionResult RetrieveByPage(int page, int rows)
        {
            int total = 0;
            var users = new List<User>();

            using (TaskAllocationDBContext ctx = new TaskAllocationDBContext())
            {
                total = ctx.Users.Count();
                users = ctx.Users.OrderByDescending(u => u.Id).Skip((page - 1) * rows).Take(rows).ToList();
            }

            return DataGrid(total, users);
        }

        [HttpPost]
        public ActionResult Update() 
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int id)
        {
            using (TaskAllocationDBContext ctx = new TaskAllocationDBContext())
            {
                var user = ctx.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    ctx.Entry<User>(user).State = EntityState.Deleted;
                }
                else
                {
                    throw new Exception("user id is not exist:" + id);
                }

                ctx.SaveChanges();
            }

            return SuccessMessage(BasicMessage.DeleteSuccess);
        }
    }
}
