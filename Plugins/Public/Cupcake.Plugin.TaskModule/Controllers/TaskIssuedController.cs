using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.TaskModule.Domain;
using Cupcake.Plugin.TaskModule.Helper;
using Cupcake.Plugin.TaskModule.Models;
using Cupcake.Plugin.TaskModule.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Plugin.TaskModule.Controllers
{
    public class TaskIssuedController : Controller
    {
        private readonly ITaskIssuedService _googleService;
        public TaskIssuedController(ITaskIssuedService googleService)
        {
            this._googleService = googleService;
        }
        public ActionResult Index(TaskIssuedCondition condition)
        {
            var taskIssueds = _googleService.SearchTaskIssued(condition);
            var models = new PagedList<TaskIssuedInfo>(taskIssueds, taskIssueds.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Create()
        {
            var model = new TaskIssuedModel();
            var user = Session["User"] as User;
            model.Publisher = user.UserName;
            ViewBag.OrganizationList = DataDictionaryHelper.GetOrganizationList();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TaskIssuedModel model)
        {
            if (ModelState.IsValid)
            {
                string department = string.Empty;
                if (model.OrganizationList!=null&&model.OrganizationList.Count>0)
                {
                    for (int i = 0; i < model.OrganizationList.Count; i++)
                    {
                        if (i>0)
                        {
                            department += ",";
                        }
                        department += model.OrganizationList[i].ToString();
                    }
                }
                TaskIssuedInfo taskInfo = model.ToInfo();
                taskInfo.Department = department;
                _googleService.Add(taskInfo);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            ViewBag.OrganizationList = DataDictionaryHelper.GetOrganizationList();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        public ActionResult Edit(Guid id)
        {
            var info = _googleService.GetById(id);
            var model = new TaskIssuedModel().ToModel(info);
            ViewBag.OrganizationList = DataDictionaryHelper.GetOrganizationList();
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(TaskIssuedModel model)
        {
            if (ModelState.IsValid)
            {
                var info = _googleService.GetById(model.Id);
                if (info != null)
                {
                    info = model.FormData(info);
                    string department = string.Empty;
                    if (model.OrganizationList != null && model.OrganizationList.Count > 0)
                    {
                        for (int i = 0; i < model.OrganizationList.Count; i++)
                        {
                            if (i > 0)
                            {
                                department += ",";
                            }
                            department += model.OrganizationList[i].ToString();
                        }
                    }
                    info.Department = department;
                    _googleService.Update(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
                }
            }
            ViewBag.OrganizationList = DataDictionaryHelper.GetOrganizationList();
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        public ActionResult Details(Guid id)
        {
            var info = _googleService.GetById(id);
            var model = new TaskIssuedModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var info = _googleService.GetById(id);
            if (info != null)
            {
                //info.IsDelete = true;
                _googleService.Delete(info);
            }
            return Json(new AjaxResult() { Result = Result.Success });
        }
    }
}
