using Cupcake.Web.Models;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Cupcake.Web.Controllers
{
    public class LocalController : Controller
    {
        public ActionResult Index()
        {
            string loginAction = ConfigurationManager.AppSettings["LoginAction"];

            if (loginAction != null && loginAction != ""&& loginAction!="Index")
            {
                return RedirectToAction(loginAction);
            }
            else { 
            return View();
            }
        }
        [HttpPost]
        public ActionResult Index(LoginUserModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserService service = new UserService();
                var user = service.Verify(model.UserName, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    Session["User"] = user;
                    //roleIds in session
                    List<Guid> roleIds = new List<Guid>();
                    foreach (var role in user.Roles)
                    {
                        roleIds.Add(role.Id);
                    }
                    Session["RoleIds"] = roleIds;

                    Session["IsSupper"] = user.UserType == UserType.Supper;
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("UserName", "用户名或密码错误");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpCookie cookie = Request.Cookies.Get("Reload_UserTokenSign");
            if (cookie!=null)
            {
cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            }

            cookie = Request.Cookies.Get("om");
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-999);
                Response.Cookies.Add(cookie);
            }

            cookie = Request.Cookies.Get("active");
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-999);
                Response.Cookies.Add(cookie);
            }
            
            //Request.Cookies.Remove("Reload_UserTokenSign");
            return RedirectToAction("Index", "LoginCenter");
            //return Redirect("http://192.168.0.117/UniARIRCenter/Portal/UARC/Logout.aspx?appid=46&reurl=http://192.168.0.73:8888");
        }


        #region
        public ActionResult Index2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index2(LoginUserModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserService service = new UserService();
                var user = service.Verify(model.UserName, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    Session["User"] = user;
                    Session["IsSupper"] = user.UserType == UserType.Supper;
                    //roleIds in session
                    List<Guid> roleIds = new List<Guid>();
                    foreach (var role in user.Roles)
                    {
                        roleIds.Add(role.Id);
                    }
                    Session["RoleIds"] = roleIds;

                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("UserName", "用户名或密码错误");
            }
            return View(model);
        }
        #endregion
    }
}
