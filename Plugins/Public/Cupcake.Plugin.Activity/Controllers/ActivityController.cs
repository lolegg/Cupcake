using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.Activity.Domain;
using Cupcake.Plugin.Activity.Helper;
using Cupcake.Plugin.Activity.Models;
using Cupcake.Plugin.Activity.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.Activity.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService service;
        private readonly IMessageManagementsService serviceMM;
        private readonly IRegistrationStatusService serviceRS;

        public ActivityController(IActivityService ActivityService, IMessageManagementsService MessageManagementsService, IRegistrationStatusService RegistrationStatusService)
        {
            this.service = ActivityService;
            this.serviceMM = MessageManagementsService;
            this.serviceRS = RegistrationStatusService;
        }
        public ActionResult Index(ActivityCondition condition)
        {
            var activity = service.SearchActivity(condition);
            var models = new PagedList<Activitys>(activity, activity.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        public ActionResult Create()
        {
            var model = new ActivitysModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ActivitysModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsTop = false;
                service.InsertGoogleProductRecord(model.ToInfo());
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
           var info = service.GetById(id);
           var model = new ActivitysModel().ToModel(info);
           return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ActivitysModel model)
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
            var model = new ActivitysModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
                ActivitysModel model = new ActivitysModel();
                var info = service.GetById(id);
                if (info != null)
                {
                    info.IsDelete = true;
                    service.UpdateGoogleProductRecord(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
                }
                return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }

        [HttpPost]
        public ActionResult UpIsTop(Guid id,bool IsTop) 
        {
            ActivitysModel model = new ActivitysModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsTop = IsTop;
                info.UpdateDate = DateTime.Now;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "UpIsTop"), model);
        }

        [HttpPost]
        public ActionResult ActivitiesState(Guid id, string State)
        {
            ActivitysModel model = new ActivitysModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.ActityState = DataDictionaryHelper.GetDictionary(State).Id;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "ActivitiesState"), model);
        }

        public ActionResult MessageManagementIndex(MessageManagementsCondition Condition,Guid id)
        {
            var MessageManagement = serviceMM.SearchMessageManagements(Condition,id);
            var models = new PagedList<MessageManagements>(MessageManagement, MessageManagement.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "MessageManagementIndex"), models);
        }

        public ActionResult DetailsMessageManagement(Guid id)
        {
            var info = serviceMM.GetById(id);
            var model = new MessageManagementsModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "DetailsMessageManagement"), model);
        }


        public ActionResult RegistrationStatusIndex(Guid id)
        {
            RegistrationStatusCondition condition = new RegistrationStatusCondition();
            condition.SubordinateActivitiesID = id;
            var registrationStatus = serviceRS.SearchRegistrationStatus(condition);
            var models = new PagedList<RegistrationStatus>(registrationStatus, registrationStatus.Paging);
            ViewBag.ActivitysTitle = service.GetById(id).Title;
            return View(PluginHelper.GetViewPath(this.GetType(), "RegistrationStatusIndex"), models);
        }
    }
}
