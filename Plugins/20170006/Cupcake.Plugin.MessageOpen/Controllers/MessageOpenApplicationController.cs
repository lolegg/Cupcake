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
    public class MessageOpenApplicationController:Controller
    {
       private readonly IMessageOpenApplicationService service;
       public MessageOpenApplicationController(IMessageOpenApplicationService baseService)
       {
           this.service = baseService;
       }
       public ActionResult Index(MessageOpenApplicationCondition condition)
       {
           var info = service.SearchNews(condition);
           var models = new PagedList<MessageOpenApplicationInfo>(info, info.Paging);
           return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
       }
       public ActionResult Details(Guid id) {
           var info = service.GetById(id);
           var model = new MessageOpenApplicationModel().ToModel(info);
           return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
       }

       //是否公开
       [HttpPost]
       public ActionResult IsPublic(Guid id) {
           var info = service.GetById(id);
               if (info.IsPublic == false)
               {
                   info.IsPublic = true;
               }
               else
               {
                   info.IsPublic = false;
               }
               service.Update(info);
           return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
       }

       //反馈页面
       public ActionResult Feedback(Guid id) {
           var info = service.GetById(id);
           var model = new MessageOpenApplicationModel().ToModel(info);
           return View(PluginHelper.GetViewPath(this.GetType(), "Feedback"), model);
       }
       [HttpPost]
       public ActionResult Feedback() {
           Guid Id =Guid.Parse(Request.Form["Id"]);
           string Feedback = Request.Form["Feedback"];
           var info = service.GetById(Id);
           MessageOpenApplicationModel Model = new MessageOpenApplicationModel();
           if(info!=null){
             Model.Feedback = Feedback;
             info= Model.FormData(info);
             service.Update(info);
             return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
           }
           return View(PluginHelper.GetViewPath(this.GetType(), "Feedback"), Model);
       }
    }
}
