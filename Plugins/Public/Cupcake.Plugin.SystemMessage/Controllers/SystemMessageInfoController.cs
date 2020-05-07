using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.SystemMessage.Domain;
using Cupcake.Plugin.SystemMessage.Models;
using Cupcake.Plugin.SystemMessage.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Plugin.SystemMessage.Controllers
{
    public partial class SystemMessageInfoController : Controller
    {
        private readonly ISystemMessageInfoService service;

        public SystemMessageInfoController(ISystemMessageInfoService SystemMessageInfoService)
        {
            this.service = SystemMessageInfoService;
        }
        public ActionResult Index(SystemMessageInfoCondition condition)
        {


            var news = service.SearchAllSystemMessageInfo(condition);
            var models = new PagedList<SystemMessageInfo>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create()
        {

            SystemMessageInfoModel model = new SystemMessageInfoModel();


            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(SystemMessageInfoModel model)
        {
            if (ModelState.IsValid)
            {

                SystemMessageInfo info = new SystemMessageInfo();
                info.Id = Guid.NewGuid();

                info.Title = model.Title;

                info.Content = model.Content;

                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            SystemMessageInfoModel model = new SystemMessageInfoModel();
            var info = service.GetById(id);


            model.Content = info.Content;

            model.Id = info.Id;
            model.Title = info.Title;

            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(SystemMessageInfoModel model)
        {
            if (ModelState.IsValid)
            {



                SystemMessageInfo info = service.GetById(model.Id);

                info.Title = model.Title;

                info.Content = model.Content;

                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            SystemMessageInfoModel model = new SystemMessageInfoModel();
            var info = service.GetById(id);


            model.Content = info.Content;
            model.Title = info.Title;

            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            SystemMessageInfoModel model = new SystemMessageInfoModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View();


        }


    }
}
