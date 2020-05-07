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
    public class OnlineAcceptanceController : Controller
    {
        private readonly IOnlineAcceptanceService service;

        public OnlineAcceptanceController(IOnlineAcceptanceService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(OnlineAcceptanceCondition condition)
        {
            var news = service.SearchOnlineAcceptance(condition);
            var models = new PagedList<OnlineAcceptance>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        public ActionResult Create()
        {
            var model = new OnlineAcceptanceModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OnlineAcceptanceModel model)
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
            var model = new OnlineAcceptanceModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OnlineAcceptanceModel model)
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
            var model = new OnlineAcceptanceModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            
            OnlineAcceptanceModel model = new OnlineAcceptanceModel();
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
