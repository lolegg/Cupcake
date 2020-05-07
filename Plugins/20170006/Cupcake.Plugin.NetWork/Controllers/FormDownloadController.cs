using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Plugin.NetWork.Models;
using Cupcake.Plugin.NetWork.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.NetWork.Controllers
{
    public class FormDownloadController : Controller
    {
        private readonly IFormDownloadService service;

        public FormDownloadController(IFormDownloadService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(FormDownloadCondition condition)
        {
            var news = service.SearchFormDownload(condition);
            var models = new PagedList<FormDownload>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        public ActionResult Create()
        {
            var model = new FormDownloadModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormDownloadModel model)
        {
            if (ModelState.IsValid)
            {
                service.InsertGoogleProductRecord(model.ToInfo());
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            var model = new FormDownloadModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormDownloadModel model)
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
            var model = new FormDownloadModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            FormDownloadModel model = new FormDownloadModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }


    }
}
