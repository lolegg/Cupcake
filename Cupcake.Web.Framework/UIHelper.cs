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
    public static class UIHelper
    {
        public static MvcHtmlString LinkButton(this HtmlHelper helper, string text, string onClick, string iconClass = "fa-dot-circle-o", string style = "primary", string size = "xs")
        {
            string html = "<a href=\"#\" class=\"btn btn-{0} btn-{1}\" onclick=\"{2}\"><i class=\"fa {3}\"></i> {4}</a>";
            return MvcHtmlString.Create(string.Format(html, style, size, onClick, iconClass, text));
        }
    }
}
