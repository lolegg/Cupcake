using AutoMapper;
using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.PublicService.Domain;
using Cupcake.Plugin.PublicService.Models;
using Cupcake.Plugin.PublicService.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Plugin.PublicService.Controllers
{
    public class RegistrationAuthorityController : Controller
    {
        private readonly IRegistrationAuthorityService service;

        public RegistrationAuthorityController(IRegistrationAuthorityService registrationAuthorityService)
        {
            this.service = registrationAuthorityService;
        }

        public ActionResult Index(RegistrationAuthorityCondition condition)
        {
            var registrationAuthoritys = service.SearchRegistrationAuthority(condition);
            var models = new PagedList<RegistrationAuthorityInfo>(registrationAuthoritys, registrationAuthoritys.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Create()
        {
            var model = new RegistrationAuthorityModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RegistrationAuthorityModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.RegistrationAuthorityType.ToString().ToLower()=="f9dfd136-3c5d-e711-9354-0023248142a5"&&model.ExecutiveStreet==null)
                {
                    ModelState.AddModelError("ExecutiveStreet","行政街道必填");
                    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
                }
                service.Add(Mapper.Map<RegistrationAuthorityModel, RegistrationAuthorityInfo>(model));
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            var model = Mapper.Map<RegistrationAuthorityInfo, RegistrationAuthorityModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RegistrationAuthorityModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.RegistrationAuthorityType.ToString().ToLower() == "f9dfd136-3c5d-e711-9354-0023248142a5" && model.ExecutiveStreet == null)
                {
                    ModelState.AddModelError("ExecutiveStreet", "行政街道必填");
                    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
                }
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info.RegistrationAuthorityType = model.RegistrationAuthorityType;
                    info.ExecutiveStreet = model.ExecutiveStreet;
                    info.Address = model.Address;
                    info.lng = model.lng;
                    info.lat = model.lat;
                    info.Telephone = model.Telephone;
                    info.WorkingHours = model.WorkingHours;
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
            var model = Mapper.Map<RegistrationAuthorityInfo, RegistrationAuthorityModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            RegistrationAuthorityModel model = new RegistrationAuthorityModel();
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
