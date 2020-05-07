using System.Collections.Generic;
using System.Web.Mvc;

namespace Cupcake.Web.Framework
{
    public static class SelectListHelper
    {
        public static IList<SelectListItem> GetPageSizeList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "15", Value = "15" });
            selectList.Add(new SelectListItem { Text = "30", Value = "30" });
            selectList.Add(new SelectListItem { Text = "50", Value = "50" });
            selectList.Add(new SelectListItem { Text = "100", Value = "100" });
            return selectList;
        }
    }
}
