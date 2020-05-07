using Cupcake.Web.Models;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Helper
{
    public static class DropDownListHelper
    {



        /// <summary>
        /// 查找父名称
        /// </summary>
        /// <param name="objitem"></param>
        /// <returns></returns>
        public static string ByParentName(object objitem)
        {
            MenuService service = new MenuService();
            Menu model = new Menu();
            if (objitem != null)
                model = service.Get(new Guid(objitem.ToString()));
            return model.Name;

        }

        public static List<SelectListItem> ParentList(string defaultItemName = "", String selectedValue = "")
        {
            MenuCondition condtion = new MenuCondition();
            MenuService service = new MenuService();
            IEnumerable<Menu> list = service.GetAll();
            var result = list.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
            if (!string.IsNullOrEmpty(defaultItemName))
            {
                result.Insert(0, new SelectListItem { Text = "--" + defaultItemName + "--", Value = "0" });
            }

            if (!String.IsNullOrEmpty(selectedValue))
            {

                SelectListItem item = result.Find(m => m.Value == selectedValue);
                if (item != null)
                    item.Selected = true;
            }
            return result;
        }

    }
}