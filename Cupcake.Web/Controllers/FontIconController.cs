using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class FontIconController : Controller
    {
        #region 字体图标选择
        public PartialViewResult ListView()
        {
            return PartialView();
        }

        public PartialViewResult PicView()
        {
            return PartialView("_PicView");
        }

        public JsonResult GetSetting()
        {
            DataSelectSetting setting = new DataSelectSetting();
            setting.ShowMutileSelect = false;
            setting.DefaultIsSingleSelected = true;
            setting.ShowPicSelect = false;
            setting.DefaultIsListView = false;
            return Json(setting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReturnCheckedNodes()
        {
            List<DataSelectNode> nodes = new List<DataSelectNode>();
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
