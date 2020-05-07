using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Authorize
{
    public class CustomerHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string url = filterContext.HttpContext.Request.Url.Scheme + "://" + filterContext.HttpContext.Request.Url.Authority + "/Error/Error.html";
            filterContext.ExceptionHandled = true;

            filterContext.Result = new RedirectResult(url);
            //base.OnException(filterContext);
        }
    }
}