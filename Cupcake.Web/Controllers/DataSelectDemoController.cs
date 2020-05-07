using Cupcake.Web.Models;
using Cupcake.Core.Domain;

using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class DataSelectDemoController :BaseSimpleDataSelectController
    {
        
        public override PartialViewResult ListView()
        {
            string name = Request["name"];
            string desc = Request["desc"];
            string pageindex = Request["pageindex"];
            CustomPermissionsService ps = new CustomPermissionsService();
            CustomPermissionsCondition pc = new CustomPermissionsCondition();
            pc.Name = name;
            pc.Desc = desc;

            pc.PageIndex = int.Parse(pageindex);
            pc.PageSize = 8;
            Paging page = new Paging(pc);

            List<CustomPermissions> list = ps.GetListByMenuId(pc, ref page).ToList();

            BasePermissionListModel model = new BasePermissionListModel(list);
            model.Paging = page;

            model.condition = pc;

            return PartialView("_ListView", model);
        }

        public override PartialViewResult PicView()
        {

            string name = Request["name"];
            string pageindex = Request["pageindex"];
            CustomPermissionsService ps = new CustomPermissionsService();
            CustomPermissionsCondition pc = new CustomPermissionsCondition();
            pc.Name = name;
            pc.PageIndex = int.Parse(pageindex);
            Paging page = new Paging(pc);
            page.PageSize = 3*6;
            List<CustomPermissions> list = ps.GetListByMenuId(pc, ref page).ToList();

            BasePermissionListModel model = new BasePermissionListModel(list);
            model.Paging = page;
            return PartialView("_PicView", model);
        }

        public override JsonResult GetSetting()
        {
            DataSelectSetting setting = new DataSelectSetting();
            setting.ShowMutileSelect = true;
            setting.DefaultIsSingleSelected = false;
            setting.ShowPicSelect = true;
            setting.DefaultIsListView = false;
            return Json(setting,JsonRequestBehavior.AllowGet);
        }

        public override JsonResult ReturnCheckedNodes()
        {
            List<DataSelectNode> nodes = new List<DataSelectNode>();
            nodes.Add(new DataSelectNode { id = "321a6f18-e6e1-e511-8146-205066c10011", name = "测试9" });
            nodes.Add(new DataSelectNode { id = "321a6f18-e6e1-e511-8146-205066c10012", name = "测试10" });
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }
    }
}
