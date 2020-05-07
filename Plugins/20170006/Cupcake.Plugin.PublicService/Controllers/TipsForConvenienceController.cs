using AutoMapper;
using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.PublicService.Domain;
using Cupcake.Plugin.PublicService.Models;
using Cupcake.Plugin.PublicService.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.PublicService.Controllers
{
    public class TipsForConvenienceController : Controller
    {
        private readonly ITipsForConvenienceService service;

        public TipsForConvenienceController(ITipsForConvenienceService tipsForConvenienceService)
        {
            this.service = tipsForConvenienceService;
        }
        public ActionResult Index(TipsForConvenienceCondition condition)
        {
            var tipsForConveniences = service.SearchTipsForConvenience(condition);
            var models = new PagedList<TipsForConvenienceInfo>(tipsForConveniences, tipsForConveniences.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Create()
        {
            var model = new TipsForConvenienceModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TipsForConvenienceModel model)
        {
            if (ModelState.IsValid)
            {
                service.Add(Mapper.Map<TipsForConvenienceModel, TipsForConvenienceInfo>(model));
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            var model = Mapper.Map<TipsForConvenienceInfo, TipsForConvenienceModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TipsForConvenienceModel model)
        {
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    //info = Mapper.Map<TipsForConvenienceModel, TipsForConvenience>(model);
                    info.Title = model.Title;
                    info.Content = model.Content;
                    info.UpdateDate = DateTime.Now;
                    service.Update(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = Mapper.Map<TipsForConvenienceInfo, TipsForConvenienceModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            TipsForConvenienceModel model = new TipsForConvenienceModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }
    }
}
