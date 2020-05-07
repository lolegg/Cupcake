using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.MapMessage
{
    public static class PluginHelper
    {
        public static string GetViewPath(Type type, string viewName)
        {
            string controllerName = type.FullName.Substring(type.FullName.LastIndexOf(".") + 1).Replace("Controller", "");
            var nameSpace = type.Module.Name.Replace(".dll", "");

            return string.Format("~/Plugins/{0}/Views/{1}/{2}.cshtml", nameSpace, controllerName, viewName);
        }
        public static string GetDateFormat(object dtime)
        {

            string returnvalue;
            try
            {
                returnvalue = Convert.ToDateTime(dtime).ToString("yyyy-MM");
            }
            catch
            {
                return "";
            }
            return returnvalue;
        }
        public static string GetDateFormatMYD(object dtime)
        {

            string returnvalue;
            try
            {
                if (dtime != null)
                {
                    returnvalue = Convert.ToDateTime(dtime).ToString("yyyy-MM-dd");
                }
                else {
                    returnvalue = "";
                }
                }
            catch
            {
                return "";
            }
            return returnvalue;
        }
        public static string GetDateFormatHHMM(object dtime)
        {

            string returnvalue;
            try
            {
                returnvalue = Convert.ToDateTime(dtime).ToString("HH:mm");
            }
            catch
            {
                return "";
            }
            return returnvalue;
        }
    }
}
