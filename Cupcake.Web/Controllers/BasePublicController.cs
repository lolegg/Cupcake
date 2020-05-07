using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cupcake.Web.Controllers
{
    public abstract class BasePublicController : BaseController
    {
        public BasePublicController()
        {
            //form认证是永久cookies票据，但session会过期，这里重新给session赋值
            AuthorizeHelper.InitiateLoginUser();
        }

        public User CurrentUser
        {
            get
            {
                return Session["User"] as User;
            }
        }

        public List<Role> CurrentRoles
        {
            get
            {
                //if (Session["Roles"]==null) {
                bool isSuper = (bool)Session["IsSupper"];
                RoleService service = new RoleService();
                List<Role> roles = new List<Role>();
                if (isSuper)
                {
                    roles = service.GetAllRole().ToList();
                }
                else
                {
                    roles = service.GetRoleByUserName(CurrentUser.UserName).ToList();
                }
                Session["Roles"] = roles;
                //}
                return (List<Role>)Session["Roles"];

            }
        }

        public bool IsSuper
        {
            get
            {
                bool isSuper = (bool)Session["IsSupper"];
                return isSuper;
            }
        }        
    }
}
