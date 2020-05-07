using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Core.Log4;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Plugin.MessageOpen.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Plugin.MessageOpen.Helper;
using Cupcake.Plugin.MessageOpen.Models;

namespace Cupcake.Plugin.MessageOpen.Controllers
{
   public class MessageOpenRecentlyController:Controller
    {

       private readonly IMessageOpenRecentlyService service;
       public MessageOpenRecentlyController(IMessageOpenRecentlyService baseService)
       {
           this.service = baseService;
       }
       public ActionResult Index(MessageOpenRecentlyCondition condition)
        {
            var info = service.SearchNews(condition);
            var models = new PagedList<MessageOpenRecentlyInfo>(info, info.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }


        public ActionResult Create()
        {
            var model = new MessageOpenRecentlyModel();
            ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
            var name = Session["User"] as User;
            model.publicName = name.Name;
            model.publicTime = DateTime.Now;
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MessageOpenRecentlyModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OrganizationList == null || model.OrganizationList.Count <= 0)
                {
                    ModelState.AddModelError("Department", "所属组织不能为空");
                    ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
                    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

                }
                string department = string.Empty;
                if (model.OrganizationList != null && model.OrganizationList.Count > 0)
                {
                    var OrganizationId = DataDictionaryHelper.GetOrganizationByName("所属组织");
                    for (int i = 0; i < model.OrganizationList.Count; i++)
                    {
                        if (model.OrganizationList[i] != OrganizationId)
                        {
                            if (i > 0)
                            {
                                department += ",";
                            }
                            department += model.OrganizationList[i].ToString();
                        }
                    }
                }
                model.Department = department;
                service.Add(model.ToInfo());
                return Json(new AjaxResult() { Result = Result.Success });
            }
            ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }


        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
            var model = new MessageOpenRecentlyModel().ToModel(info);
            var name = Session["User"] as User;
            model.publicName = name.Name;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MessageOpenRecentlyModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OrganizationList == null || model.OrganizationList.Count <= 0)
                {
                    ModelState.AddModelError("Department", "所属组织不能为空");
                    ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
                    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

                }
                string department = string.Empty;
                if (model.OrganizationList != null && model.OrganizationList.Count > 0)
                {
                    var OrganizationId = DataDictionaryHelper.GetOrganizationByName("所属组织");
                    for (int i = 0; i < model.OrganizationList.Count; i++)
                    {
                        if (model.OrganizationList[i] != OrganizationId)
                        {
                            if (i > 0)
                            {
                                department += ",";
                            }
                            department += model.OrganizationList[i].ToString();
                        }
                    }
                }
                var info = service.GetById(model.Id);
                model.Department = department;
                if (info != null)
                {
                    info = model.FormData(info);
                    service.Update(info);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }



        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new MessageOpenRecentlyModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }


        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
            }
            return Json(new AjaxResult() { Result = Result.Success });
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsTop(Guid id)
        {
            var info = service.GetById(id);
            if (ModelState.IsValid)
            {
                if (info.IsTop == false)
                {
                    info.IsTop = true;
                }
                else
                {
                    info.IsTop = false;
                }
                service.Update(info);
            }
            return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
        }
    }
}
