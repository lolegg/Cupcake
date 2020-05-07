using AutoMapper;
using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.MembersForWeb.Domain;
using Cupcake.Plugin.MembersForWeb.Models;
using Cupcake.Plugin.MembersForWeb.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Helper;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.MembersForWeb.Controllers
{
    public class MembersForWebController : Controller
    {
        private readonly IMembersService service;

        public MembersForWebController(IMembersService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(MembersCondition condition)
        {
            var news = service.SearchNews(condition);
            var models = new PagedList<Members>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        [HttpGet]
        public ActionResult Create()
        {

            MembersModel model = new MembersModel();
       

            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
        [HttpPost]
        public ActionResult Create(MembersModel model)
        {
           
            if (ModelState.IsValid)
            {
                if (model.Password != model.Password2)
                {
                    ModelState.AddModelError("Password", "两次输入的密码不一致！");
                       return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
                }


                var info = new Members();
                info = Mapper.Map<MembersModel, Members>(model);
                if (info != null)
                {
                    service.Add(info);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }

            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            MembersModel model = new MembersModel();
            var info = service.GetById(id);

            model = Mapper.Map<Members, MembersModel>(info);


            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [HttpPost]
        public ActionResult Edit(MembersModel model)
        {
            model.Status = DataDictionaryHelper.GetId("用户状态>启用");
            if (ModelState.IsValid)
            {
                
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info.UserName = model.UserName;
                    info.UserRealName = model.UserRealName;
                    info.UserType = model.UserType;
                    info.ImageUrl = model.ImageUrl;
                    info.Tel = model.Tel;
                    info.Email = model.Email;

                   
                    service.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpGet]
        public ActionResult PasswordEdit(Guid id)
        {

            MembersModel model = new MembersModel();
            var info = service.GetById(id);

            model = Mapper.Map<Members, MembersModel>(info);


            return View(PluginHelper.GetViewPath(this.GetType(), "PasswordEdit"), model);


        }
        [HttpPost]
        public ActionResult PasswordEdit(MembersModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.Password2)
                {
                    ModelState.AddModelError("Password", "两次输入的密码不一致！");
                    return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
                }


                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info.Password = model.Password;

                    service.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }





        [HttpGet]
        public ActionResult Details(Guid id)
        {

            MembersModel model = new MembersModel();
            var info = service.GetById(id);

            model = Mapper.Map<Members, MembersModel>(info);

            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            MembersModel model = new MembersModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"));


        }

        public ActionResult Disable(Guid id)
        {

            MembersModel model = new MembersModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.Status = DataDictionaryHelper.GetId("用户状态>禁用");
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"));


        }
        public ActionResult Enable(Guid id)
        {

            MembersModel model = new MembersModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.Status = DataDictionaryHelper.GetId("用户状态>启用");
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"));


        }
    }
}
