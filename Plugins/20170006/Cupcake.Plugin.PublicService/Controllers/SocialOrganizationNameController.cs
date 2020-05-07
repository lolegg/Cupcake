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
    public class SocialOrganizationNameController : Controller
    {
          private readonly ISocialOrganizationNameService service;

          public SocialOrganizationNameController(ISocialOrganizationNameService socialOrganizationNameService)
          {
            this.service = socialOrganizationNameService;
          }
          public ActionResult Index(SocialOrganizationNameCondition condition)
          {
              var searchSocialOrganizationNames = service.SearchSocialOrganizationName(condition);
              var models = new PagedList<SocialOrganizationNameInfo>(searchSocialOrganizationNames, searchSocialOrganizationNames.Paging);

              return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
          }
          public ActionResult Create()
          {
              var model = new SocialOrganizationNameModel();
              return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

          }
          [HttpPost]
          [ValidateInput(false)]
          public ActionResult Create(SocialOrganizationNameModel model)
          {
              if (ModelState.IsValid)
              {

                  service.Add(Mapper.Map<SocialOrganizationNameModel, SocialOrganizationNameInfo>(model));
                  return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
              }
              return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
          }
          public ActionResult Edit(Guid id)
          {
              var info = service.GetById(id);
              var model = Mapper.Map<SocialOrganizationNameInfo, SocialOrganizationNameModel>(info);
              return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
          }
          [HttpPost]
          [ValidateInput(false)]
          public ActionResult Edit(SocialOrganizationNameModel model)
          {
              if (ModelState.IsValid)
              {
                  var info = service.GetById(model.Id);
                  if (info != null)
                  {
                      info.Name = model.Name;
                      info.Address = model.Address;
                      info.lng = model.lng;
                      info.lat = model.lat;
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
              var model = Mapper.Map<SocialOrganizationNameInfo, SocialOrganizationNameModel>(info);
              return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
          }

          [HttpPost]
          public ActionResult Delete(Guid id)
          {
              SocialOrganizationNameModel model = new SocialOrganizationNameModel();
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
