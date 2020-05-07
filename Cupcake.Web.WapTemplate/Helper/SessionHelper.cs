

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cupcake.Web.WapTemplate.Helper
{

    public static class SessionHelper
    {
        public const string keyuserid = "user_id";
        public const string keyusername = "user_name";

        public const string keyname = "_name";

        public const string keyusermobile = "user_mobile";
        public const string keyuserface = "user_face";
        public const string keyusertype = "user_type";

        public const string keyuserisvalidcode = "user_is_valid_code";
        public const string keyuservalidcode = "user_valid_code";
        public const string keyuservalidmobile = "user_valid_mobile";

        public const string keyuserauthrealtype = "user_auth_real";
        public const string keyuserauthvolunteertype = "user_auth_volunteer";
        public const string keyuserauthtuanyuantype = "user_auth_tuanyuan";

        public const string keyuserCommittee = "user_auth_Committee";

        public static bool islogin
        {
            get
            {
                return CommonHelper.ToGuid(userid).HasValue ? true : false;
            }
        }
        public static Guid useridguid
        {
            get
            {
                return CommonHelper.ToGuid(userid).Value;
            }
        }
        public static string usertype
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyusertype]);
            }
            set
            {
                HttpContext.Current.Session[keyusertype] = value;
            }
        }
        public static string userid
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuserid]);
            }
            set
            {
                HttpContext.Current.Session[keyuserid] = value;
            }
        }

        public static string userface
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuserface]);
            }
            set
            {
                HttpContext.Current.Session[keyuserface] = value;
            }
        }
        public static string userCommittee
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuserCommittee]);
            }
            set
            {
                HttpContext.Current.Session[keyuserCommittee] = value;
            }
        }
        public static string username
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyusername]);
            }
            set
            {
                HttpContext.Current.Session[keyusername] = value;
            }
        }


        public static string name
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyname]);
            }
            set
            {
                HttpContext.Current.Session[keyname] = value;
            }
        }

        public static string usermobile
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyusermobile]);
            }
            set
            {
                HttpContext.Current.Session[keyusermobile] = value;
            }
        }
        public static string uservalidcode
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuservalidcode]);
            }
            set
            {
                HttpContext.Current.Session[keyuservalidcode] = value;
            }
        }
        public static string uservalidmobile
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuservalidmobile]);
            }
            set
            {
                HttpContext.Current.Session[keyuservalidmobile] = value;
            }
        }

        public static bool userisvalidcode
        {
            get
            {
                return CommonHelper.ToBool(HttpContext.Current.Session[keyuserisvalidcode]);
            }
            set
            {
                HttpContext.Current.Session[keyuserisvalidcode] = value;
            }
        }
        public static string userauthrealtype
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuserauthrealtype]);
            }
            set
            {
                HttpContext.Current.Session[keyuserauthrealtype] = value;
            }
        }
        public static string userauthvolunteertype
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuserauthvolunteertype]);
            }
            set
            {
                HttpContext.Current.Session[keyuserauthvolunteertype] = value;
            }
        }
        public static string userauthtuanyuantype
        {
            get
            {
                return CommonHelper.ToStr(HttpContext.Current.Session[keyuserauthtuanyuantype]);
            }
            set
            {
                HttpContext.Current.Session[keyuserauthtuanyuantype] = value;
            }
        }    
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
        /// <summary>
        /// 验证状态保留
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool ValidCodeStatus(string code)
        {
            bool revalue = false;
            if (!string.IsNullOrEmpty(code))
            {
                if (userisvalidcode || uservalidcode == code)
                {
                    return true;
                }
            }
            return revalue;
        }
        public static bool ValidCode(string code)
        {
            bool revalue = false;
            if (!string.IsNullOrEmpty(code))
            {
                //if (uservalidcode == code)
                //{
                //    uservalidcode = "";
                //    return true;
                //}
                return true;
            }
            return revalue;
        }
        public static bool ValidMobile(string code)
        {
            bool revalue = false;
            if (!string.IsNullOrEmpty(code))
            {
                if (uservalidmobile == code)
                {
                    uservalidmobile = "";
                    return true;
                }
            }
            return revalue;
        }
    }
}
