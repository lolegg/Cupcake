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
  public  class OnlineInterviewQuestionAskController: Controller
    {
        private readonly IOnlineInterviewQuestionAskService service;

        public OnlineInterviewQuestionAskController(IOnlineInterviewQuestionAskService OnlineInterviewQuestionAskService)
        {
            this.service = OnlineInterviewQuestionAskService;
        }
        public ActionResult Index(OnlineInterviewQuestionAskCondition condition)
        {


            var news = service.SearchAllOnlineInterviewQuestionAsk(condition);
            var models = new PagedList<OnlineInterviewQuestionAsk>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create(Guid OnlineInterviewId)
        {


            OnlineInterviewQuestionAskModel model = new OnlineInterviewQuestionAskModel();
            model.OnlineInterviewId = OnlineInterviewId;

            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(OnlineInterviewQuestionAskModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineInterviewQuestionAsk info = new OnlineInterviewQuestionAsk();
                info.Id = Guid.NewGuid();
                info.Address = model.Address;
                info.OnlineInterviewId = model.OnlineInterviewId;
                info.Name = model.Name;
                info.Question = model.Question;

                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            OnlineInterviewQuestionAskModel model = new OnlineInterviewQuestionAskModel();
            var info = service.GetById(id);


            model.Address = info.Address;

            model.Name = info.Name;
            model.Phone = info.Phone;
            model.Id = info.Id;

            model.Question = info.Question;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(OnlineInterviewQuestionAskModel model)
        {
            if (ModelState.IsValid)
            {



                OnlineInterviewQuestionAsk info = service.GetById(model.Id);

                info.Address = model.Address;

                info.Name = model.Name;
                info.Phone = model.Phone;
                info.Question = model.Question;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            OnlineInterviewQuestionAskModel model = new OnlineInterviewQuestionAskModel();
            var info = service.GetById(id);

            model.Address = info.Address;

            model.Name = info.Name;
            model.Phone = info.Phone;
            model.Question = info.Question;
            model.Ask = info.Ask;

            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            OnlineInterviewQuestionAskModel model = new OnlineInterviewQuestionAskModel();
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
            OnlineInterviewQuestionAskModel model = new OnlineInterviewQuestionAskModel();
            var info = service.GetById(id);


      


            model.Name = info.Name;
            model.Address = info.Address;
         
            model.Phone = info.Phone;

            model.Question = info.Question;

            model.Ask = info.Ask;



            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);
        }


        [HttpPost]
        public ActionResult Display(OnlineInterviewQuestionAskModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineInterviewQuestionAsk info = service.GetById(model.Id);



             


                info.Name = model.Name;
                info.Address = model.Address;
            
                info.Phone = model.Phone;

                //是否显示到前端页面
                info.Question = model.Question;

                info.Ask = model.Ask;

                info.UpdateDate = DateTime.Now;

                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);

        }


    }
}
