using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web
{
    public class DropdownListHelper
    {
        public static List<SelectListItem> GetParentMapOverlays(string selectedValue = "")
        {
            MapService service = new MapService();
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "请选择", Value = "" });
            foreach (dynamic mapOverlay in service.GetParentMapOverlays())
            {
                var item = new SelectListItem { Text = "".PadLeft(mapOverlay.Level * 2, '-') + mapOverlay.Name, Value = mapOverlay.Id.ToString() };
                if (mapOverlay.Name == selectedValue)
                {
                    item.Selected = true;
                }
                selectList.Add(item);
            }

            return selectList;
        }
        public static List<SelectListItem> GetChildMapOverlays(Guid parentId, string selectedValue = "")
        {
            MapService service = new MapService();
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "请选择", Value = "" });
            foreach (var mapOverlay in service.GetMapOverlays(parentId))
            {
                var item = new SelectListItem { Text = mapOverlay.Name, Value = mapOverlay.Id.ToString() };
                if (mapOverlay.Name == selectedValue)
                {
                    item.Selected = true;
                }
                selectList.Add(item);
            }

            return selectList;
        }
        //public static List<SelectListItem> GetChildList(string defaultItemName = "", String selectedValue = "", string parentId = "")
        //{

        //    MapService service = new MapService();
        //    Guid? parentIdTemp = Guid.NewGuid();
        //    if (!String.IsNullOrEmpty(parentId))
        //    {
        //        parentIdTemp = new Guid(parentId);
        //    }

        //    IEnumerable<MapCenter> list = service.GetMapCenter(parentIdTemp);

        //    var result = new List<SelectListItem>();

        //    if (list != null && list.Count() > 0)
        //    {
        //        foreach (MapCenter center in list)
        //        {
        //            if (center.Id == null)
        //            { continue; }
        //            result.Add(new SelectListItem { Text = center.Name.ToString(), Value = center.BaseId.ToString() });
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(defaultItemName))
        //    {
        //        result.Insert(0, new SelectListItem { Text = "--" + defaultItemName + "--", Value = "" });
        //    }
        //    if (!String.IsNullOrEmpty(selectedValue))
        //    {

        //        SelectListItem item = result.Find(m => m.Value == selectedValue);
        //        if (item != null)
        //            item.Selected = true;
        //    }
        //    return result;
        //}


    }
}