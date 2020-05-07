using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Framework
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString IconFonts(this HtmlHelper helper, string className, string onclick = "", string style = "")
        {
            if (!string.IsNullOrEmpty(onclick))
            {
                style = "cursor:pointer;" + style;
            }
            return MvcHtmlString.Create(string.Format("<i class='{0} {1}'{2}{3}></i>", GetIconFontsType(className), className, onclick == "" ? "" : " onclick='" + onclick + "'", style == "" ? "" : " style='" + style + "'"));
        }
        public static MvcHtmlString ButtonShiny(this HtmlHelper helper, string name, string iconFontsClass, string onclick, ButtonCss buttonCss = ButtonCss.Default, string style = "")
        {
            return Button(name, string.Format("btn btn-{0} shiny", buttonCss.ToString().ToLower()), iconFontsClass, onclick, style);
        }
        public static MvcHtmlString ButtonXS(this HtmlHelper helper, string name, string iconFontsClass, string onclick, ButtonCss buttonCss = ButtonCss.Default, string style = "")
        {
            return Button(name, string.Format("btn btn-{0} btn-xs", buttonCss.ToString().ToLower()), iconFontsClass, onclick, style);
        }
        public static MvcHtmlString Button(string name, string buttonClass, string iconFontsClass, string onclick, string style = "")
        {
            if (name.Length == 2)
            {
                name = name.Insert(1, " ");
            }
            return MvcHtmlString.Create(string.Format("<button type=\"button\" class=\"{4}\" onclick=\"{2}\"{5}><i class=\"{3} {1}\"></i> {0}</button>", name, iconFontsClass, onclick, GetIconFontsType(iconFontsClass), buttonClass, style == "" ? "" : " style=\"" + style + "\""));
        }

        private static string GetIconFontsType(string className)
        {
            string type = "fa";
            if (className.Contains("glyphicon-"))
            {
                type = "glyphicon";
            }
            else if (className.Contains("typcn-"))
            {
                type = "typcn";
            }
            return type;
        }
        public static MvcHtmlString ToJSFormat(this HtmlHelper helper, MvcHtmlString html)
        {
            return MvcHtmlString.Create(html.ToString().Replace("\r\n", "").Replace("\"", "'"));
        }
    }
}
