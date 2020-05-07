using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.PluginManagement.Domain;
using Cupcake.Plugin.PluginManagement.Models;
using Cupcake.Plugin.PluginManagement.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Helper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cupcake.Plugin.PluginManagement.Controllers
{
    public class PluginManagementController : Controller
    {
        private readonly IPluginsService service;

        public PluginManagementController(IPluginsService service)
        {
            this.service = service;
        }
        public ActionResult Index(PluginsCondition condition)
        {
            //var a = service.GetById(new Guid("8D2E6410-2B20-E711-8A80-B8AC6F19DFD9"));
            var entitys = service.SearchPlugins(condition);
            var models = new PagedList<PluginsModel>(entitys.Select(p => p.ToModel()), entitys.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        [HttpGet]
        public ActionResult Create()
        {
            PluginsModel model = new PluginsModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        public ActionResult Create(PluginsModel model)
        {
            if (ModelState.IsValid)
            {
                if (service.AlreadyExists(model.SystemName, Guid.Empty))
                {
                    ModelState.AddModelError("SystemName", "系统名重复");
                }
                else
                {
                    var entity = model.ToEntity();
                    entity.Group = "Reloadsoft";
                    entity.PluginType = DataDictionaryHelper.GetId("插件状态>已注册");

                    service.InsertPlugin(entity);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var entity = service.GetPlugin(id);
            if (entity == null || entity.IsDelete == true)
            {
                throw new NopException("插件不存在");
            }
            var model = entity.ToModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpPost]
        public ActionResult Edit(PluginsModel model)
        {
            if (ModelState.IsValid)
            {
                if (service.AlreadyExists(model.SystemName, model.Id))
                {
                    ModelState.AddModelError("SystemName", "系统名重复");
                }
                else
                {
                    var entity = service.GetPlugin(model.Id);
                    entity.SystemName = model.SystemName;
                    entity.FriendlyName = model.FriendlyName;
                    entity.BigVersion = model.BigVersion;
                    entity.SmallVersion = model.SmallVersion;
                    entity.Author = model.Author;
                    entity.Description = model.Description;

                    service.UpdatePlugin(entity);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
    }
}
