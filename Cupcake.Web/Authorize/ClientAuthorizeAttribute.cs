using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cupcake.Web.Authorize
{
    public class ClientAuthorizeAttribute : AuthorizeAttribute
    {
        public string Permissions { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request["CurrentMenuId"] == null)
            {
                return false;
            }
            else
            {
                AuthorizeHelper.InitiateLoginUser();
                if (httpContext.Session["IsSupper"] != null && (bool)httpContext.Session["IsSupper"])
                {
                    return true;
                }
                else
                {
                    var menuId = new Guid(httpContext.Request["CurrentMenuId"].ToString());
                    var user = httpContext.Session["User"] as User;
                    var roleIds = httpContext.Session["RoleIds"] as List<Guid>;
                    var service = new HasPermissionsService();
                    var service2 = new CustomPermissionsService();
                    var a = service2.GetCustomPermissions(menuId, Permissions);
                    if (a == null)
                    {
                        return false;
                    }
                    else
                    {
                        return service.HasPermission(menuId, a.Id, roleIds);
                    }
                    //return base.AuthorizeCore(httpContext);
                    return false;
                }
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = filterContext.HttpContext.Request.Url.Scheme + "://" + filterContext.HttpContext.Request.Url.Authority + "/Error/NoPermission.html";

            filterContext.Result = new RedirectResult("/Error/NoPermission?message=没有权限,访问被拒绝");
            //string.Concat(FormsAuthentication.LoginUrl,
            //             "?ReturnUrl=",
            //             filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.Url.AbsoluteUri)));
        }
    }
}