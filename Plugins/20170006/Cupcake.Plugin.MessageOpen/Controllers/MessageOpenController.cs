﻿using Cupcake.Core;
using Cupcake.Plugin.MessageOpen.Domain;
using Cupcake.Plugin.MessageOpen.Services;
using Cupcake.Plugin.MessageOpen.Models;
using System.Web.Mvc;
using System;
using Cupcake.Web.Framework;
using Cupcake.Core.Domain;
using Cupcake.Plugin.MessageOpen.Helper;

namespace Cupcake.Plugin.MessageOpen.Controllers
{
   public class MessageOpenController:Controller
    {
       private readonly IMessageOpenService service;
       public MessageOpenController(IMessageOpenService MessageOpenService)
       {
           this.service = MessageOpenService;
       }
       public ActionResult Index(MessageOpenCondition condition)
       {
           var MessageOpen = service.SearchNews(condition);
           var models = new PagedList<MessageOpenInfo>(MessageOpen, MessageOpen.Paging);
           return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
       }


       public ActionResult Create() {
           var model = new MessageOpenModel();
           //ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
           var name = Session["User"] as User;
           model.publicName = name.Name;
           model.publicTime = DateTime.Now;
           return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
       }
        [HttpPost]
        [ValidateInput(false)]
       public ActionResult Create(MessageOpenModel model) {
           if (ModelState.IsValid)
           {
               //if (model.OrganizationList == null || model.OrganizationList.Count <= 0)
               //{
               //    ModelState.AddModelError("Department", "所属组织不能为空");
               //    ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
               //    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

               //}
              // string department = string.Empty;
               //if (model.OrganizationList != null && model.OrganizationList.Count > 0)
               //{
               //    for (int i = 0; i < model.OrganizationList.Count; i++)
               //    {
               //        if (i > 0)
               //        {
               //            department += ",";
               //        }
               //        department += model.OrganizationList[i].ToString();
               //    }
               //}
               //model.Department = department;
             
               int result = service.GetAddMessageOpenChooseCount(model.MessageOpenChoose);
               if (result > 0)
                   ModelState.AddModelError("MessageOpenChoose", "此项已存在");
               else
               {
                   service.Add(model.ToInfo());
                   return Json(new AjaxResult() { Result = Result.Success });
               }
           }
           //ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
           return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
       }



       public ActionResult Edit(Guid id) {
           var info= service.GetById(id);
           ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
           var model = new MessageOpenModel().ToModel(info);
           var name = Session["User"] as User;
           model.publicName = name.Name;
           return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
       }
        [HttpPost]
        [ValidateInput(false)]
       public ActionResult Edit(MessageOpenModel model)
       {
           if (ModelState.IsValid)
           {
               //if (model.OrganizationList == null || model.OrganizationList.Count <= 0)
               //{
               //    ModelState.AddModelError("Department", "所属组织不能为空");
               //    ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
               //    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

               //}
               //string department = string.Empty;
               //if (model.OrganizationList != null && model.OrganizationList.Count > 0)
               //{
               //    for (int i = 0; i < model.OrganizationList.Count; i++)
               //    {
               //        if (i > 0)
               //        {
               //            department += ",";
               //        }
               //        department += model.OrganizationList[i].ToString();
               //    }
               //}
               int result = service.GetEditMessageOpenChooseCount(model.Id,model.MessageOpenChoose);
               if (result > 0)
                   ModelState.AddModelError("MessageOpenChoose", "此项已存在");
               else
               {
                   var info = service.GetById(model.Id);
                   //info.Department = department;
                   if (info != null)
                   {
                       info = model.FormData(info);
                       service.Update(info);
                       return Json(new AjaxResult() { Result = Result.Success });
                   }
               }
           }
           return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
       }



       public ActionResult Details(Guid id) 
       { 
           var info= service.GetById(id);
           var model = new MessageOpenModel().ToModel(info);
           return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
       }


        [HttpPost]
       public ActionResult Delete(Guid id) 
       {
           var info = service.GetById(id);
           if(info!=null){
               service.Delete(info);
           }
           return Json(new AjaxResult() { Result = Result.Success });
       }
    }
    
}
