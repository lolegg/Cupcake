using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Cupcake.Web.Framework
{
    public static class ValidHelper
    {
        /// <summary>
        /// 验证电话号码格式
        /// </summary>
        /// <param name="val"></param>
        public static bool ValidMobile(string val)
        {
            return Regex.IsMatch(val, @"^\d{11}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证邮箱格式
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ValidEmail(string val)
        {
            return Regex.IsMatch(val, @"^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ValidIDCard(string val)
        {
            return Regex.IsMatch(val, @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证Url地址
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ValidUrl(string val)
        {
            return Regex.IsMatch(val, @"http(s)?://([/w-]+/.)+[/w-]+(/[/w- ./?%&=]*)?", RegexOptions.IgnoreCase);
        }
    }
}