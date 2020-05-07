using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Helper;
using Cupcake.Web.WapTemplate.Helper.Ioc;
using Cupcake.Web.WapTemplate.Models;
using Cupcake.Web.WapTemplate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.WapTemplate.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [HttpGet]
        public ActionResult Login()
        {
            Guid? cookieuserid = CommonHelper.ToGuid(CookieHelper.UserId);
            string cookieusertemppwd = CookieHelper.UserTempPwd;
            if (cookieuserid.HasValue && !string.IsNullOrEmpty(cookieusertemppwd))
            {
                AccountServices service = new AccountServices();
                var info = service.LoginTempPwd(cookieuserid.Value, cookieusertemppwd);
                if (info != null)
                {
                    string temppwd = Guid.NewGuid().ToString();
                    CookieHelper.UserId = info.Id.ToString();
                    CookieHelper.UserTempPwd = temppwd;
                    info.TempPwd = temppwd;
                    service.UpdateLogin(info);
                    loadsession(info);
                    string tempurl = "/";
                    if (!string.IsNullOrEmpty(CookieHelper.tempurlNoAuth))
                        tempurl = CookieHelper.tempurlNoAuth;
                    //   return Redirect("/common/msg?msg=登录成功&tourl=" + tempurl);
                    return Redirect("/Account/Temp");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == null || model.UserName == null)
                {
                    ModelState.AddModelError("messageValue", "请输入账号和密码");
                    return View(model);
                }

                string memberpwd = EncryptHelper.Lower32(model.Password);
                AccountServices service = new AccountServices();

                var info = service.Verify(model.UserName, memberpwd);

                if (info != null)
                {
                    if (model.RememberMe)
                    {
                        string temppwd = "";
                        temppwd = Guid.NewGuid().ToString();
                        CookieHelper.UserId = info.Id.ToString();
                        CookieHelper.UserTempPwd = temppwd;
                        info.TempPwd = temppwd;
                    }
                    service.UpdateLogin(info);
                    loadsession(info);
                    string tempurl = "/";
                    if (!string.IsNullOrEmpty(CookieHelper.tempurlNoAuth))
                        tempurl = CookieHelper.tempurlNoAuth;
                    //  return Redirect("/common/msg?msg=登录成功&tourl=" + tempurl);
                    return Redirect("/Account/Temp");

                }
                else
                {
                    CheckValidUserAndLock(model.UserName);

                    ModelState.AddModelError("messageValue", "账号或密码错误");
                }
            }
            return View(model);
        }

        private void loadsession(Members_Members info)
        {
            AccountServices service = new AccountServices();
            LogAccountLogin infolog = new LogAccountLogin();
            infolog.LoginIP = System.Web.HttpContext.Current.Request.UserHostAddress;
            infolog.LoginName = info.UserName;
            infolog.Mid = info.Id;
            LogService logservice = new LogService();

            logservice.Add(infolog);

            SessionHelper.userid = info.Id.ToString();
            SessionHelper.username = info.UserName;
            SessionHelper.usertype = info.UserType.ToString();
            SessionHelper.name = info.UserRealName;
            SessionHelper.usermobile = info.Tel;
            SessionHelper.userface = info.ImageUrl;
            //SessionHelper.islogin==true;
        }

        public void CheckValidUserAndLock(string username)
        {
            AccountServices service = new AccountServices();
            var infosingle = service.Get(p=>p.UserName==username);
            if (infosingle != null)
            {
                infosingle.failLoginCount += 1;
                service.UpdateLogin(infosingle);

            }

        }

        [HttpPost]
        public int getvalidmobile(string mobile, string validcode = "jk")
        {
            int revalue = 0;
            if (SessionHelper.ValidCode(validcode))
            {
                SessionHelper.userisvalidcode = true;
                ValidCodeHelper vCode = new ValidCodeHelper();
                string code = vCode.CreateValidateCode(5);
                SessionHelper.uservalidmobile = code;
                new SmsService().SendCode(mobile, code);
                revalue = 1;
            }
            return revalue;
        }
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (SessionHelper.uservalidmobile == model.ValidCode)
                {
                    string memberpwd = EncryptHelper.Lower32(model.Password);
                    AccountServices service = new AccountServices();

                    //   var temp = service.Get(m => m.UserName == model.UserName);

                    if (service.Get(p=>p.UserName==model.UserName) == null)
                    {

                        Members_Members info = new Members_Members();

                        info.LoginCount = 0;
                        info.ValidCode = model.ValidCode;
                        info.Password = memberpwd;
                        info.Tel = model.Tel;
                        info.UserName = model.UserName;
                        info.UserRealName = model.Name;
                        info = service.AddReturn1(info);

                        service.UpdateLogin(info);
                        loadsession(info);
                        return Redirect("/Account/Temp");
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "用户名已存在");
                    }
                }
                else
                {
                    ModelState.AddModelError("ValidCode", "验证码错误");
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult ForGotPassWord()
        {

            return View(new ForgetPassModel());
        }
        [HttpPost]
        public ActionResult ForGotPassWord(ForgetPassModel model)
        {
            if (ModelState.IsValid)
            {
                AccountServices service = new AccountServices();
                var info = service.Get(m => m.UserName == model.UserName && m.Tel == model.Tel);
                if (info == null)
                {
                    ModelState.AddModelError("UserName", "用户名或手机号错误，请确认");
                }
                else
                {
                    ValidCodeHelper vCode = new ValidCodeHelper();
                    string code = vCode.CreateValidateCode(6);
                    var tempass = EncryptHelper.Lower32(code);
                    info.Password = tempass;
                    service.UpdatePassword(info.Id,tempass);
                    new SmsService().SendCode(model.Tel, code);
                    return Redirect("/Account/Temp");

                }

            }

            return View();
        }

        public ActionResult LogOut()
        {
            CookieHelper.UserId = "";
            CookieHelper.UserTempPwd = "";
            SessionHelper.Clear();
            return Redirect("/Home/Index");

        }

        public ActionResult Temp()
        {
            return View();
        }

	}
}