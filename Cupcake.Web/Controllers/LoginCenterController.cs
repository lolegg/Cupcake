
using Cupcake.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Cupcake.Web.Controllers
{
    public class LoginCenterController : Controller
    {
        //
        // GET: /LoginCenter/

        public ActionResult Index(string returnUrl)
        {
            string authenticationMode = ConfigurationManager.AppSettings["AuthenticationMode"].ToLower();
            if (authenticationMode == "local")
            {
                return RedirectToAction("Index", "Local", new RouteValueDictionary(new { ReturnUrl = returnUrl }));
            }
            else
            {
                HttpCookie cookie = Request.Cookies.Get("Reload_UserTokenSign");
                string strCheckCookieUrl = ConfigurationManager.AppSettings["UARCCheckCookieUrl"];
                if (cookie == null || string.IsNullOrEmpty(cookie.Value))
                {
                    strCheckCookieUrl += "?appid=" + ConfigurationManager.AppSettings["UARCAppId"]; ;
                    strCheckCookieUrl += "&ru=" + HttpUtility.UrlEncode(Request.Url.ToString());
                    //跳转
                    //Response.Redirect(strCheckCookieUrl);
                    return Redirect(strCheckCookieUrl);
                }
                else
                {
                    string url = ConfigurationManager.AppSettings["UARCVerifyWebService"].ToLower();
                    Hashtable ht = new Hashtable();
                    ht.Add("strTokenValue", cookie.Value);
                    var xmlDoc = WebServiceHelper.QueryGetWebService(url, "Reload_CheckIsTokenLive", ht);
                    if (xmlDoc.InnerXml.Contains("false"))
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);
                        strCheckCookieUrl += "?appid=" + ConfigurationManager.AppSettings["UARCAppId"]; ;
                        strCheckCookieUrl += "&ru=" + HttpUtility.UrlEncode(Request.Url.ToString());
                        //跳转
                        return Redirect(strCheckCookieUrl);

                    }
                    else
                    {
                        //return Redirect(strCheckCookieUrl);
                        FormsAuthentication.SetAuthCookie(cookie.Value, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }

    }
}
