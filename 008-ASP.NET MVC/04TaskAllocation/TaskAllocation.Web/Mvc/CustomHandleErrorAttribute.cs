using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAllocation.Core.Common;
using TaskAllocation.Core.Models;
using TaskAllocation.Core.Utils;

namespace TaskAllocation.Web.Mvc
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            

            if (filterContext.ExceptionHandled == false)
            {
                bool isAjax = filterContext.HttpContext.Request.IsAjaxRequest();

                if (isAjax)
                {
                    ResponseObject respObj = new ResponseObject();
                    respObj.ResponseState = ResponseState.Failed;

                    if (ex is ArgumentException || ex is BusinessException)
                    {
                        respObj.Message = filterContext.Exception.Message;
                    }
                    else if (ex is InsufficientPermissionException)
                    {
                        respObj.Message = Resources.BasicMessage.InsufficientPermission;
                    }
                    else
                    {
                        filterContext.HttpContext.Response.StatusCode = 404;
                        filterContext.HttpContext.Response.End();
                    }
                }
                else if (ex is InsufficientPermissionException)
                {
                    filterContext.HttpContext.Response.Redirect("~/Common/NoPermission");
                }
                else 
                {
                    LogUtil.Error(ex.Message);
                }

                filterContext.ExceptionHandled = true;
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}