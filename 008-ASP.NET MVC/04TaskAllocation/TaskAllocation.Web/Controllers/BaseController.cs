using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAllocation.Core.Common;
using TaskAllocation.Core.Models;
using TaskAllocation.Web.Models;

namespace TaskAllocation.Web.Controllers
{
    public class BaseController : Controller
    {
        #region ResponseMessage

        protected ActionResult ErrorMessage(string msg, bool isGet = true)
        {
            return ResponseMessage(ResponseState.Error, msg, isGet);
        }

        protected ActionResult FailedMessage(string msg = "", bool isGet = true)
        {
            return ResponseMessage(ResponseState.Failed, msg, isGet);
        }

        protected ActionResult SuccessMessage(string msg = "", bool isGet = true)
        {
            return ResponseMessage(ResponseState.Success, msg, isGet);
        }

        private ActionResult ResponseMessage(ResponseState responseState, string msg, bool isGet)
        {
            if (isGet)
            {
                return Json(new ResponseObject() { ResponseState = responseState, Message = msg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ResponseObject() { ResponseState = responseState, Message = msg });
            }
        }
        
        #endregion

        protected ActionResult DataGrid<T>(int total, List<T> lstEntities, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet) 
        {
            return Json(new DataGridModel<T>(total, lstEntities).ToDataObject(), jsonRequestBehavior);
        }

        protected bool IsLogon() 
        {
            return Session["LoginUserName"] != null;
        }
    }
}
