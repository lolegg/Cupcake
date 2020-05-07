using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Core.Log4;
using Cupcake.Plugin.WorkNotice;
using Cupcake.Plugin.WorkNotice.Domain;
using Cupcake.Plugin.WorkNotice.Helper;
using Cupcake.Plugin.WorkNotice.Models;
using Cupcake.Plugin.WorkNotice.Services;
using Cupcake.Web.Framework;
using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Cupcake.Plugin.WorkNotice.Controllers
{
    public class WorkNoticeController : Controller
    {
        private readonly IWorkNoticeService service;

        public WorkNoticeController(IWorkNoticeService workNoticeService)
        {
            this.service = workNoticeService;
        }
        public ActionResult Index(WorkNoticeCondition condition)
        {
            var workNotice = service.SearchWorkNotices(condition);
            var models = new PagedList<WorkNotices>(workNotice, workNotice.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        public ActionResult Create()
        {
            var model = new WorkNoticeModel();
            var name = Session["User"] as User;
            model.Publisher = name.UserName;
            ViewBag.Organization = TreeHtml.GetListOrganization();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(WorkNoticeModel model)
        {
            if (model!=null)
            {
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
                WorkNotices WorkNotices = model.ToInfo();
                WorkNotices.Department = department;
                service.InsertGoogleProductRecord(WorkNotices);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            var info = service.GetById(id);
            var model = new WorkNoticeModel().ToModel(info);
            ViewBag.Organization = TreeHtml.GetListOrganization();
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(WorkNoticeModel model)
        {
            if (model != null)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
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

                    info = model.FormData(info);
                    info.Department = department; 
                    service.UpdateGoogleProductRecord(info);
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }



        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            WorkNoticeModel model = new WorkNoticeModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }

        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new WorkNoticeModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }
    }
}
