using Cupcake.Core;
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
  public  class MessageOpenMechanismController:Controller
    {
      private readonly IMessageOpenMechanismService service;
      public MessageOpenMechanismController(IMessageOpenMechanismService baseService)
       {
           this.service = baseService;
       }
      public ActionResult Index(MessageOpenMechanismCondition condition)
       {
           var MessageOpen = service.SearchNews(condition);
           var models = new PagedList<MessageOpenMechanismInfo>(MessageOpen, MessageOpen.Paging);
           return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
       }


       public ActionResult Create() {
           var model = new MessageOpenMechanismModel();
           var name = Session["User"] as User;
           model.publicName = name.Name;
           return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
       }
        [HttpPost]
        [ValidateInput(false)]
       public ActionResult Create(MessageOpenMechanismModel model)
       {
           if (ModelState.IsValid)
           {
               int result = service.GetMessageOpenMechanismCount();
               if (result > 0)
               {
                   ModelState.AddModelError("Name", "只包含一条信息,您可以选择编辑修改此项");
               }
               else
               {
                   service.Add(model.ToInfo());
                   return Json(new AjaxResult() { Result = Result.Success });
               }
           }
           return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
       }



       public ActionResult Edit(Guid id) {
           var info= service.GetById(id);
           ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
           var model = new MessageOpenMechanismModel().ToModel(info);
           var name = Session["User"] as User;
           model.publicName = name.Name;
           return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
       }
        [HttpPost]
        [ValidateInput(false)]
       public ActionResult Edit(MessageOpenMechanismModel model)
       {
           if (ModelState.IsValid)
           {
               var info = service.GetById(model.Id);
               if (info!=null)
               {
                   info=model.FormData(info);
                   service.Update(info);
                   return Json(new AjaxResult() { Result = Result.Success});
               }
           }
           return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
       }



       public ActionResult Details(Guid id) 
       { 
           var info= service.GetById(id);
           var model = new MessageOpenMechanismModel().ToModel(info);
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
