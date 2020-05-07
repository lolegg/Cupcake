using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.NetInteraction.Domain;
using Cupcake.Plugin.NetInteraction.Models;
using Cupcake.Plugin.NetInteraction.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Plugin.NetInteraction.Controllers
{
    public partial class OnlineCommentsOpinionController : Controller
    {

         private readonly IOnlineCommentsOpinionService service;

        public OnlineCommentsOpinionController(IOnlineCommentsOpinionService OnlineCommentsOpinionService)
        {
            this.service = OnlineCommentsOpinionService;
        }
        public ActionResult Index(OnlineCommentsOpinionCondition condition)
        {


            var news = service.SearchAllOnlineCommentsOpinion(condition);
            var models = new PagedList<OnlineCommentsOpinion>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create(Guid OnlineCommentsId)
        {


            OnlineCommentsOpinionModel model = new OnlineCommentsOpinionModel();
            model.OnlineCommentsId = OnlineCommentsId;

            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
      
        [HttpPost]
        public ActionResult Create(OnlineCommentsOpinionModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineCommentsOpinion info = new OnlineCommentsOpinion();
                info.Id = Guid.NewGuid();
                info.UserId = model.UserId;
                info.OnlineCommentsId = model.OnlineCommentsId;
                info.UserName = model.UserName;
                info.Content = model.Content;

                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            OnlineCommentsOpinionModel model = new OnlineCommentsOpinionModel();
            var info = service.GetById(id);


          
            model.Id = info.Id;




            model.UserId = info.UserId;
            model.OnlineCommentsId = info.OnlineCommentsId;
            model.UserName = info.UserName;
            model.Content = info.Content;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(OnlineCommentsOpinionModel model)
        {
            if (ModelState.IsValid)
            {



                OnlineCommentsOpinion info = service.GetById(model.Id);

                info.UserId = model.UserId;
                info.OnlineCommentsId = model.OnlineCommentsId;
                info.UserName = model.UserName;
                info.Content = model.Content;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            OnlineCommentsOpinionModel model = new OnlineCommentsOpinionModel();
            var info = service.GetById(id);

            model.UserId = info.UserId;
            model.OnlineCommentsId = info.OnlineCommentsId;
            model.UserName = info.UserName;
            model.Content = info.Content;
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            OnlineCommentsOpinionModel model = new OnlineCommentsOpinionModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View();


        }


        [HttpGet]
        public ActionResult Display(Guid id)
        {
            OnlineCommentsOpinionModel model = new OnlineCommentsOpinionModel();
            var info = service.GetById(id);





            model.UserId = info.UserId;
            model.OnlineCommentsId = info.OnlineCommentsId;
            model.UserName = info.UserName;
            model.Content = info.Content;



            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);
        }

    }
}
