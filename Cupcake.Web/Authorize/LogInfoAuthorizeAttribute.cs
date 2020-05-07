using log4net;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class LogInfoAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var user = HttpContext.Current.Session["user"] as User;
            if (user != null)
            {
                var controller = filterContext.RouteData.Values["controller"].ToString();
                var action = filterContext.RouteData.Values["action"].ToString();
                var actionName = action.ToLower();
                var model = new LogInfo
                {
                    Level = "Info",
                    Logger = controller + "/" + action,
                    Message = actionName,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsDelete = false,
                    Operator = user.Name,
                    IP = ""
                };
                new LogInfoService().Add(model);
            }
        }
    }
}