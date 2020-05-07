
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.ComponentModel;

namespace Cupcake.Plugin.Questionnaire.Helper
{
    public class CommonHelper
    {
        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="idcardstr"></param>
        /// <returns></returns>
        public static bool IdCardValid(string idcardstr)
        {
            return false;
        }
        public static DateTime IdCardGetBirthday(string idcardstr)
        {
            DateTime birthday = DateTime.MinValue;
            if (idcardstr.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                birthday = Convert.ToDateTime(idcardstr.Substring(6, 4) + "-" + idcardstr.Substring(10, 2) + "-" + idcardstr.Substring(12, 2));
            }
            if (idcardstr.Length == 15)
            {
                birthday = Convert.ToDateTime("19" + idcardstr.Substring(6, 2) + "-" + idcardstr.Substring(8, 2) + "-" + idcardstr.Substring(10, 2));
            }
            return birthday;
        }
        public static string IdCardGetSex(string idcardstr)
        {
            string sex = "";
            int sexint = 0;
            if (idcardstr.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
            {
                sexint = ToInt(idcardstr.Substring(14, 3));
            }
            if (idcardstr.Length == 15)
            {
                sexint = ToInt(idcardstr.Substring(12, 3));
            }
            if (sexint % 2 == 0)//性别代码为偶数是女性奇数为男性
            {
                sex = "女";
            }
            else
            {
                sex = "男";
            }
            return sex;
        }
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="curDateTime">当前日期</param>
        public static string GetDateTimeFormat(object curDateTime)
        {
            string result = "";
            try {
         
                result = Convert.ToDateTime(curDateTime).ToString("yyyy-MM-dd");
                if (result == "0001-01-01")
                {
                    result = "";
                }
         
            }
            catch { }

            return result;
        }

        public static string ToYYYYMMDD(DateTime dt)
        {
            try
            {
                string ddd= dt.ToString("yyyy-MM-dd");
                if (ddd == "0001-01-01")
                {
                    return "";
                }
                else
                    return ddd;
            }
            catch
            {
                return "";
            }
        }
        public static string ToYYYYMMDD(DateTime? dt)
        {
            try
            {
                return dt.Value.ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }

        //public static string GetDateTime(object curDateTime)
        //{
        //    string result = "";
        //    try
        //    {

        //        result = Convert.ToDateTime(curDateTime).ToString("yyyy-MM");
        //        if (result == "0001-01-01")
        //        {
        //            result = "";
        //        }

        //    }
        //    catch { }

        //    return result;
        //}


        public static string GetDateTimeYYMMDDHHmm(DateTime? curDateTime)
        {
          
                string result = "";
              try
            {
                if (curDateTime.HasValue)
                {
                    result = Convert.ToDateTime(curDateTime).ToString("yyyy-MM-dd HH:mm");
                    if (result == "0001-01-01")
                    {
                        result = "";
                    }
                }
            }catch{
            
            }

            return result;
        }

        public static int ToInt(object vals)
        {
            int revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToInt32(vals) : 0;
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static decimal ToDecimalMoney(object vals)
        {
            decimal revalue = 0;
            try
            {
                string tempstr = String.Format("{0:f6}", vals);
                revalue = !string.IsNullOrEmpty(tempstr) ? Convert.ToDecimal(vals) : 0;
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static decimal ToDecimal(object vals)
        {
            decimal revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToDecimal(vals) : 0;
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static double ToDouble(object vals)
        {
            double revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToDouble(vals) : 0;
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static double ToDouble(object vals, int mathround = 1, int retentiondecimalnum = 2)
        {
            double revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToDouble(vals) : 0;
                revalue = Math.Round(revalue / mathround, retentiondecimalnum);
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static decimal ToDecimal(object vals, int mathround = 1, int retentiondecimalnum = 2)
        {
            decimal revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToDecimal(vals) : 0;
                revalue = Math.Round(revalue / mathround, retentiondecimalnum);
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }

        public static bool IsDateTime(string str)
        {
            bool isDateTime = false;
            // yyyy/MM/dd  
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})$"))
            {
                isDateTime = true;
            }
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$"))
            {
                isDateTime = true;
            }
            else if ((Regex.IsMatch(str, "^(?<year>\\d{2,4})[.](?<month>\\d{1,2})[.](?<day>\\d{1,2})$")))
            {
                isDateTime = true;
            }
            return isDateTime;
        }


        public static string ToStr(object vals)
        {
            string revalue = "";
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToString(vals) : "";
            }
            catch
            {
                ;
            }
            return revalue;
        }

    }
}