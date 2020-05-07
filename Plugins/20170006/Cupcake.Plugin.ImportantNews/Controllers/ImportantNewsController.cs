using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.ImportantNews.Domain;
using Cupcake.Plugin.ImportantNews.Models;
using Cupcake.Plugin.ImportantNews.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.ImportantNews.Controllers
{
    public class ImportantNewsController : Controller
    {
        private readonly IImportantNewsService service;

        public ImportantNewsController(IImportantNewsService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(NewsCondition condition)
        {
            var news = service.SearchNews(condition);
            var models = new PagedList<ImportantNewsInfo>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ImportantNewsModel model = new ImportantNewsModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(ImportantNewsModel model)
        {
            if (ModelState.IsValid)
            {
                ImportantNewsInfo info = new ImportantNewsInfo();
                info.Id = Guid.NewGuid();
                info.Title = model.Title;
                info.Content = model.Content;
                info.NewsType = model.NewsType;
                info.ReleaseTime = model.ReleaseTime;
                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            ImportantNewsModel model = new ImportantNewsModel();
            var info = service.GetById(id);
            model.Title = info.Title;
            model.Content = info.Content;
            model.NewsType = info.NewsType;
            model.ReleaseTime = info.ReleaseTime;
            model.Id = info.Id;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(ImportantNewsModel model)
        {
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info.Id = model.Id;
                    info.Title = model.Title;
                    info.Content = model.Content;
                    info.NewsType = model.NewsType;
                    info.ReleaseTime = model.ReleaseTime;
                    service.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            ImportantNewsModel model = new ImportantNewsModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }
        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new ImportantNewsModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }
    }
}
