using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;

namespace Cupcake.Web.Framework
{
    public static class EnumExtensions
    {
        private static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
            {
                return (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }
            return null;
        }
        public static T ParseEnumName<T>(this string enumName) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumName, true);
        }
        public static string GetDescription(this Enum targetEnum)
        {
            string _description = string.Empty;
            FieldInfo _fieldInfo = targetEnum.GetType().GetField(targetEnum.ToString());
            DescriptionAttribute[] _attributes = _fieldInfo.GetDescriptAttr();
            if (_attributes != null && _attributes.Length > 0)
                _description = _attributes[0].Description;
            else
                _description = targetEnum.ToString();
            return _description;
        }
        private static string GetEnumDescription(object e)
        {
            FieldInfo[] ms = e.GetType().GetFields();
            Type t = e.GetType();
            foreach (System.Reflection.FieldInfo f in ms)
            {
                if (f.Name != e.ToString()) continue;
                foreach (Attribute attr in f.GetCustomAttributes(true))
                {
                    DescriptionAttribute dscript = attr as DescriptionAttribute;
                    if (dscript != null)
                        return dscript.Description;
                }
            }
            return e.ToString();
        }

        public static IList<SelectListItem> GetSelectList(Type enumType, string selectedValue = "", string excludedValues = "")
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "请选择", Value = "" });

            foreach (object e in Enum.GetValues(enumType))
            {
                if (excludedValues.Contains(((int)e).ToString()))
                {
                    continue;
                }
                if (e.ToString() == selectedValue)
                {
                    selectList.Add(new SelectListItem { Text = GetEnumDescription(e), Value = ((int)e).ToString(), Selected = true });
                }
                else
                {
                    selectList.Add(new SelectListItem { Text = GetEnumDescription(e), Value = ((int)e).ToString() });
                }
            }

            return selectList;
        }
        public static MvcHtmlString GetSelectListHtml(Type enumType, string name, string id = "", string style = "", string selectedValue = "", string excludedValues = "")
        {
            var selectList = GetSelectList(enumType, selectedValue, excludedValues);
            string html = "<select class='form-control'{1} name='{0}'{2}>{3}</select>", tmpHtml = "";
            foreach (var item in selectList)
            {
                if (item.Selected)
                {
                    tmpHtml += string.Format("<option selected='selected' value='{0}'>{1}</option>", item.Value, item.Text);
                }
                else
                {
                    tmpHtml += string.Format("<option value='{0}'>{1}</option>", item.Value, item.Text);
                }
            }
            return MvcHtmlString.Create(string.Format(html, name, id == "" ? "" : " id='" + id + "'", style == "" ? "" : " style='" + style + "'", tmpHtml));
        }
    }
}