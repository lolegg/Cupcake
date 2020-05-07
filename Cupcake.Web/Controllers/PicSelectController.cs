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
    public class PicSelectController : BaseSimpleDataSelectController
    {

        /// <summary>
        /// 选择新的模板
        /// </summary>
        /// <returns></returns>
        public override ActionResult DataTemplate()
        {
            return View("PicSelect");
        }

        public override PartialViewResult ListView()
        {
            return PartialView();
        }

        public override PartialViewResult PicView()
        {

            string type = Request["type"];
            string name = Request["name"];
            string pageindex = Request["pageindex"];
            MediaInfoService service = new MediaInfoService();
            MediaCondition pc = new MediaCondition();
            if(!string.IsNullOrEmpty(type))
            {
                pc.MediaTypeId = new Guid(type);
            }
            pc.FileName = name;
            pc.MediaClass = MediaClass.图片;
            
            pc.PageIndex = int.Parse(pageindex);

            Paging page = new Paging(pc);
            page.PageSize = 3*6;
            List<Media> list = service.GetListByMediaTypeId(pc, ref page).ToList();

            MediaInfoListModel model = new MediaInfoListModel(list);
            model.Paging = page;
            return PartialView("_PicView", model);
        }

        /// <summary>
        /// 获取资源分类
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMediaType()
        {
            MediaTypeService service = new MediaTypeService();
            return Json(service.GetAll(), JsonRequestBehavior.AllowGet);
        }


        public override JsonResult GetSetting()
        {
            MediaSelectSetting setting = new MediaSelectSetting();
            setting.ShowMutileSelect = true;            
            setting.ShowPicSelect = false;
            setting.DefaultIsListView = false;
            return Json(setting, JsonRequestBehavior.AllowGet);
        }

    }
}
