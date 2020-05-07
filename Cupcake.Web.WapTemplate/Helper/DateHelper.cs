using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Helper
{
    public class DateHelper
    {
        public static DateTime? GetMax(params DateTime?[] dtime)
        {
            DateTime? revalue = null;
            if (dtime.Length > 0)
            {
                foreach (DateTime? item in dtime)
                {
                    if (!revalue.HasValue)
                    {
                        revalue = item;
                    }
                    else
                    {
                        if (revalue.HasValue)
                        {
                            if (item.HasValue && item.Value > revalue.Value)
                            {
                                revalue = item;
                            }
                        }
                        else
                        {
                            revalue = item;
                        }
                    }
                }
            }
            return revalue;
        }

        public static DateTime GetDateYearMin(DateTime dtime)
        {
            return Convert.ToDateTime(dtime.ToString("yyyy-01-01") + " 00:00:00");
        }
        public static DateTime GetDateYearMax(DateTime dtime)
        {
            return Convert.ToDateTime(dtime.ToString("yyyy-12-31") + " 00:00:00");
        }
        public static bool ValidDate(DateTime? dtime)
        {
            if (dtime == null || dtime == DateTime.MinValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string GetDateYYYYMMDD(DateTime dtime)
        {
            if (ValidDate(dtime) == false)
            {
                return "";
            }
            return dtime.ToString("yyyy-MM-dd");
        }

        public static string GetDateYYYYMMDDHHmm(DateTime dtime)
        {
            if (ValidDate(dtime) == false)
            {
                return "";
            }
            return dtime.ToString("yyyy-MM-dd HH:mm");
        }
        public static string GetDateYYYYMMDD(DateTime? dtime)
        {
            if (ValidDate(dtime) == false)
            {
                return "";
            }
            return dtime.Value.ToString("yyyy-MM-dd");
        }

        public static DateTime ToDate(object dtime)
        {
            DateTime tempdate = DateTime.MinValue;
            try
            {
                tempdate = Convert.ToDateTime(dtime);
            }
            catch
            {
            }
            return tempdate;
        }

        public static string GetDateFormat(object dtime, string format)
        {
            DateTime tempdate;
            try
            {
                tempdate = Convert.ToDateTime(dtime);
            }
            catch
            {
                return "";
            }
            return tempdate.ToString(format);
        }
        public static string GetDateFormat(object dtime)
        {
            DateTime tempdate;
            try
            {
                tempdate = Convert.ToDateTime(dtime);
            }
            catch
            {
                return "";
            }
            return tempdate.ToString("yyyy-MM-dd");
        }

        public static DateTime GetDateMonthMin(DateTime dtime)
        {
            return Convert.ToDateTime(dtime.AddDays(1 - dtime.Day).ToString("yyyy-MM-dd") + " 00:00:00");
            //return dtime.AddDays(1-dtime.Day);
        }

        /// <summary>
        /// yyyy-mm-dd
        /// </summary>
        /// <param name="dtime"></param>
        /// <returns></returns>
        public static string GetDateMonthMinStr(DateTime dtime)
        {
            return GetDateMonthMin(dtime).ToShortDateString();
        }
        public static DateTime GetDateMonthMax(DateTime dtime)
        {
            return Convert.ToDateTime(dtime.AddDays(-dtime.Day).AddMonths(1).ToString("yyyy-MM-dd") + " 23:59:59");
            //return dtime.AddMonths(1).AddDays(-dtime.Day);
        }

        /// <summary>
        /// yyyy-mm-dd
        /// </summary>
        /// <param name="dtime"></param>
        /// <returns></returns>
        public static string GetDateMonthMaxStr(DateTime dtime)
        {
            return GetDateMonthMax(dtime).ToShortDateString();
        }


        /// <summary>
        /// 根据DayOfWeek，返回这天是星期中第几天
        /// </summary>
        public static int GetDayOfWeekByDayiw(DayOfWeek dayOfWeek)
        {
            int dayiw = 0;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayiw = 1;
                    break;
                case DayOfWeek.Tuesday:
                    dayiw = 2;
                    break;
                case DayOfWeek.Wednesday:
                    dayiw = 3;
                    break;
                case DayOfWeek.Thursday:
                    dayiw = 4;
                    break;
                case DayOfWeek.Friday:
                    dayiw = 5;
                    break;
                case DayOfWeek.Saturday:
                    dayiw = 6;
                    break;
                case DayOfWeek.Sunday:
                    dayiw = 7;
                    break;
            }

            return dayiw;
        }

        /// <summary>
        /// 如何计算当前日期是本月的第几周
        /// </summary>
        /// <param name="dtTime"></param>
        public static void GetDayToWeek(DateTime dtTime)
        {

            //得到当前为这个月的第几天
            int day = Convert.ToInt32(dtTime.Day);

            ////Console.WriteLine("当前是这个月的第" + day + "天");

            //得到是当前年的哪一天
            int yearday = Convert.ToInt32(dtTime.DayOfYear);

            ////Console.WriteLine(yearday);

            //得到当前年
            int year = Convert.ToInt32(dtTime.Year);
            ////Console.WriteLine(year);

            //得到月份
            int month = Convert.ToInt32(dtTime.Month);

            int totalDays = 0;
            bool inRn;//是否为闰年
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
            {
                inRn = true;
            }
            else
            {
                inRn = false;//平年
            }

            for (int i = 1900; i < year; i++)
            {
                /* 判断闰年或平年，并进行天数累加 */
                if (i % 4 == 0 && i % 100 != 0 || i % 400 == 0)
                { // 判断是否为闰年
                    totalDays = totalDays + 366; // 闰年366天
                }
                else
                {
                    totalDays = totalDays + 365; // 平年365天
                }
            }

            ////Console.WriteLine(totalDays);

            int days = 0;
            int beforeDays = 0;
            for (int i = 1; i <= month; i++)
            {
                switch (i)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        days = 31;
                        break;
                    case 2:
                        days = inRn ? 29 : 28;
                        break;
                    default:
                        days = 30;
                        break;
                }
                if (i < month)
                {
                    beforeDays = beforeDays + days;
                }

            }
            //Console.WriteLine("此月份之前的天数" + beforeDays);

            totalDays = totalDays + beforeDays; // 距离1900年1月1日的天数
            int firstDayOfMonth; // 存储当月第一天是星期几：星期日为0
            int temp = 1 + totalDays % 7; // 从1900年1月1日推算
            if (temp == 7)
            { // 求当月第一天
                firstDayOfMonth = 0; // 周日
            }
            else
            {
                firstDayOfMonth = temp;
            }
            Console.WriteLine("该月第一天是星期" + firstDayOfMonth);

        }
    }
}