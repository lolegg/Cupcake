
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Web.WapTemplate.Helper
{
    public static class CookieHelper
    {
        public const string cookieuserid = "cookie_userid";
        public const string cookietemppwd = "cookie_temppwd";
        public const string cookietempurl = "cookie_tempurl";

        public static string UserId
        {
            get
            {
                return CookieBase.GetCookieValue(cookieuserid);
            }
            set
            {
                CookieBase.SetCookie(cookieuserid, value);
            }
        }
        public static string UserTempPwd
        {
            get
            {
                return CookieBase.GetCookieValue(cookietemppwd);
            }
            set
            {
                CookieBase.SetCookie(cookietemppwd, value);
            }
        }
        public static string tempurlNoAuth
        {
            get
            {
                return CookieBase.GetCookieValue(cookietempurl);
            }
            set
            {
                CookieBase.SetCookie(cookietempurl, value);
            }
        }
    }
}
