using AutoMapper;
using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.MapMessage.Domain;
using Cupcake.Plugin.MapMessage.Models;
using Cupcake.Plugin.MapMessage.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.MapMessage.Controllers
{


    public class MapMessageController : Controller
    {
        private readonly IMapMessageService service;


        public MapMessageController(IMapMessageService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(MapMessageCondition condition)
        {
            var activity = service.SearchNews(condition);
            var models = new PagedList<MapMessageInfo>(activity, activity.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        public ActionResult Create()
        {
            var model = new MapMessageModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MapMessageModel model)
        {
            if (ModelState.IsValid)
            {

                service.Add(Mapper.Map<MapMessageModel, MapMessageInfo>(model));
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            var model = Mapper.Map<MapMessageInfo, MapMessageModel>(info);
           
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MapMessageModel model)
        {
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    //info = Mapper.Map<MapMessageModel, MapMessageInfo>(model);
                    info.Name = model.Name;
                    info.RegisterOrganization = model.RegisterOrganization;
                    info.SortModual = model.SortModual;
                    info.SortType = model.SortType;
                    info.lat = model.lat;
                    info.lng = model.lng;
                    info.Address = model.Address;
                  
                    service.Update(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = Mapper.Map<MapMessageInfo, MapMessageModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            MapMessageModel model = new MapMessageModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }

        //[HttpPost]
        //public ActionResult UpIsTop(Guid id, bool IsTop)
        //{
        //    MapMessageModel model = new MapMessageModel();
        //    var info = service.GetById(id);
        //    if (info != null)
        //    {

        //        info.UpdateDate = DateTime.Now;
        //        service.Update(info);
        //        return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
        //    }
        //    return View(PluginHelper.GetViewPath(this.GetType(), "UpIsTop"), model);
        //}

      
    }
}
