using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.Activity.Domain;
using Cupcake.Plugin.Activity.Helper;
using Cupcake.Plugin.Activity.Models;
using Cupcake.Plugin.Activity.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.Activity.Controllers
{
    public class ActivityStyleController : Controller
    {
        private readonly IActivityStyleService service;
        public ActivityStyleController(IActivityStyleService ActivityStyleService)
        {
            this.service = ActivityStyleService;
        }
        public ActionResult Index(ActivityStyleCondition condition)
        {
            var activitystyle = service.SearchActivityStyle(condition);
            var models = new PagedList<ActivityStyle>(activitystyle, activitystyle.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        public ActionResult Create()
        {
            var model = new ActivityStyleModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ActivityStyleModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsTop = false;
                service.InsertGoogleProductRecord(model.ToInfo());
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            var model = new ActivityStyleModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ActivityStyleModel model)
        {
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info = model.FormData(info);
                    service.UpdateGoogleProductRecord(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new ActivityStyleModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            ActivityStyleModel model = new ActivityStyleModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }
        [HttpPost]
        public ActionResult UpIsTop(Guid id, bool IsTop)
        {
            ActivityStyleModel model = new ActivityStyleModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsTop = IsTop;
                info.UpdateDate = DateTime.Now;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "UpIsTop"), model);
        }

    }
}
