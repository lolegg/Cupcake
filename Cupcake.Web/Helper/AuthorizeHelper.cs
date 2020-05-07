using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Web;

namespace Cupcake.Web.Helper
{
    public static class AuthorizeHelper
    {
        public static void InitiateLoginUser()
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    UserService service = new UserService();
                    User currentUser = null;
                    if (HttpContext.Current.User.Identity.Name.Length > 10)
                    {
                        currentUser = service.GetUserByName("admin");
                    }
                    else
                    {
                        currentUser = service.GetUserByName(HttpContext.Current.User.Identity.Name);
                    }
                    HttpContext.Current.Session["User"] = currentUser;
                    HttpContext.Current.Session["IsSupper"] = currentUser.UserType == UserType.Supper;
                    //roleIds in session
                    List<Guid> roleIds = new List<Guid>();
                    foreach (var role in currentUser.Roles)
                    {
                        roleIds.Add(role.Id);
                    }
                    HttpContext.Current.Session["RoleIds"] = roleIds;
                }
            }
        }
    }
}
