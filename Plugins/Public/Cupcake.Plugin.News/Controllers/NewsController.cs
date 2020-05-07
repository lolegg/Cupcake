using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Core.Log4;
using Cupcake.Plugin.News.Domain;
using Cupcake.Plugin.News.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Plugin.News.Models;

namespace Cupcake.Plugin.News.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService service;

        public NewsController(INewsService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(NewsCondition condition)
        {
            var news = service.SearchNews(condition);
            var models = new PagedList<NewsInfo>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Create()
        {
            var model = new NewsModel();
            var name = Session["User"] as User;
            model.Name = name.Name;
            model.publicTime = DateTime.Now;
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(NewsModel model) 
        {
            if (ModelState.IsValid)
            {
                var name = Session["User"] as User;
               
                service.InsertGoogleProductRecord(model.ToInfo());
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id) 
        {
            if(id!=null){
                var info=service.GetById(id);
                info.IsDelete=true;
                service.UpdateGoogleProductRecord(info);
            }
            return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
        }

        public ActionResult Edit(Guid id) {
                var info = service.GetById(id);
                var model = new NewsModel().ToModel(info);
                return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
          [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(NewsModel model) 
        {
            if (ModelState.IsValid)
            {
                    var info = service.GetById(model.Id);
                    info = model.FormData(info);
                    service.UpdateGoogleProductRecord(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        public ActionResult Details(Guid id) 
        {
         
                var info = service.GetById(id);
                var model = new NewsModel().ToModel(info);
                return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }
    }
}
