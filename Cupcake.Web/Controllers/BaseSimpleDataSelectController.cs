using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public abstract class BaseSimpleDataSelectController : BasePublicController
    {
        public virtual ActionResult DataTemplate()
        {
            return View("SimpleDataSelectModal");
        }

        /// <summary>
        /// 默认的列表风格
        /// </summary>
        /// <returns></returns>
        public abstract PartialViewResult ListView();



        /// <summary>
        /// 开启图文显示后,实现这个视图,即可实现列表风格和图片风格切换
        /// </summary>
        /// <returns></returns>
        public abstract PartialViewResult PicView();

        /// <summary>
        /// 返回已选择的节点列表
        /// </summary>
        /// <returns></returns>
        public virtual JsonResult ReturnCheckedNodes()
        {
            List<DataSelectNode> nodes = new List<DataSelectNode>();
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetSetting()
        {
            DataSelectSetting setting = new DataSelectSetting();
            return Json(setting, JsonRequestBehavior.AllowGet);
        }

    }
}
