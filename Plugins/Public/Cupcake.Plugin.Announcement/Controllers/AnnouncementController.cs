using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Core.Log4;
using Cupcake.Plugin.Announcement.Domain;
using Cupcake.Plugin.Announcement.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Plugin.Announcement.Helper;
using Cupcake.Plugin.Announcement.Models;


namespace Cupcake.Plugin.Announcement.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService service;

        public AnnouncementController(IAnnouncementService newsService)
        {
            this.service = newsService;
        }
        /// <summary>
        /// 展示
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Index(AnnouncementCondition condition)
        {
            var news = service.SearchNotice(condition);
            var models = new PagedList<AnnouncementInfo>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Create() 
        {
           
            var model=new AnnouncementModel();
            ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
            var name = Session["User"] as User;
            model.Name = name.UserName;
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(AnnouncementModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OrganizationList == null ||model.OrganizationList.Count <= 0)
                {
                    ModelState.AddModelError("Department", "所属组织不能为空");
                    ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
                    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

                }
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
                    model.Department = department;
                    service.InsertGoogleProductRecord(model.ToInfo());
                    return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });

                }
             ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
             return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
            }
           
        
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            if(id!=null){
                var info = service.GetById(id);
                info.IsDelete = true;
                service.UpdateGoogleProductRecord(info);
            }
            return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id) 
        {
           
             var info = service.GetById(id);
             ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
             var model = new AnnouncementModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(AnnouncementModel model) 
        {
         
            var info = service.GetById(model.Id);
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
            ViewBag.Organization = DataDictionaryHelper.GetOrganizationList();
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid id) 
        {
            var info = service.GetById(id);
            var model = new AnnouncementModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }


        /// <summary>
        /// 是否置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Placed(Guid id)
        {
            var info = service.GetById(id);
            if (ModelState.IsValid)
            {
                if (info.IsPlaced == 0) {
                    info.IsPlaced = 1;
                } else {
                    info.IsPlaced = 0;
                }
                service.UpdateGoogleProductRecord(info);
            }
            return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
        }
    }
}
