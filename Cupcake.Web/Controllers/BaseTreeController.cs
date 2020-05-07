using Cupcake.Core.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public abstract class BaseTreeController<T> : BasePublicController
    {

        /// <summary>
        /// 默认模板,如果功能出入大，可以重写此方法 
        /// </summary>
        /// <returns></returns>
        //public virtual ActionResult SimpleTreeModal()
        //{
        //    return View("SimpleTreeModal");
        //}

        public abstract JsonResult InsertTree();

        public abstract JsonResult EditTree();

        public abstract JsonResult DeleteTree();

        public abstract JsonResult CheckTree();

        /// <summary>
        /// 获取选择的树节点ID集合，空则表示取消全部结点,如数据库有记录可删除相关的关联
        /// </summary>
        /// <returns></returns>
        public virtual List<Guid> GetCheckedNodeIds()
        {
            string checkedNodeIds = Request["checkednodes"];
            var list = new List<Guid>();
            if (!string.IsNullOrEmpty(checkedNodeIds))
            {
                var tmpList = checkedNodeIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                list = tmpList.Select(t =>
                {
                    return new Guid(t);
                }).ToList();
            }
            return list;
        }

        public abstract JsonResult LoadTree();

        public abstract ActionResult NodeView(T condition);

        /// <summary>
        /// 加载树的设置配置，默认全部功能可用
        /// </summary>
        /// <returns></returns>
        public virtual JsonResult GetTreeSetting()
        {
            TreeSetting tf = new TreeSetting();
            tf.TreeName = "列表";
            return Json(tf, JsonRequestBehavior.AllowGet);
        }



    }
}
