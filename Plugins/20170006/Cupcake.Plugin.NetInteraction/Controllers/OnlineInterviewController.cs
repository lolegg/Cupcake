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
    public class OnlineInterviewController : Controller
    {
        private readonly IOnlineInterviewService service;



        private readonly IOnlineInterviewQuestionAskService IOnlineInterviewQuestionAskService;

        public OnlineInterviewController(IOnlineInterviewService OnlineInterviewService, IOnlineInterviewQuestionAskService IOnlineInterviewQuestionAskService)
        {
            this.service = OnlineInterviewService;
            this.IOnlineInterviewQuestionAskService = IOnlineInterviewQuestionAskService;
        }
        public ActionResult Index(OnlineInterviewCondition condition)
        {


            var news = service.SearchAllOnlineInterview(condition);
            var models = new PagedList<OnlineInterview>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create()
        {

            OnlineInterviewModel model = new OnlineInterviewModel();


            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
     
        [HttpPost]
        public ActionResult Create(OnlineInterviewModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineInterview info = new OnlineInterview();
                info.Id = Guid.NewGuid();

                info.theme = model.theme;
                info.Guest = model.Guest;
                info.shijian = model.shijian;
                info.Address = model.Address;
                //是否开启网友提问
                info.IsEnabled = model.IsEnabled;

                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            OnlineInterviewModel model = new OnlineInterviewModel();
            var info = service.GetById(id);




            model.Id = info.Id;


            model.theme = info.theme;
            model.Guest = info.Guest;
            model.shijian = info.shijian;
            model.Address = info.Address;
            //是否开启网友提问
            model.IsEnabled = info.IsEnabled;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        
        [HttpPost]
        public ActionResult Edit(OnlineInterviewModel model)
        {
            if (ModelState.IsValid)
            {



                OnlineInterview info = service.GetById(model.Id);

                info.Id = model.Id;


                info.theme = model.theme;
                info.Guest = model.Guest;
                info.shijian = model.shijian;
                info.Address = model.Address;
                //是否开启网友提问
                info.IsEnabled = model.IsEnabled;

                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            OnlineInterviewModel model = new OnlineInterviewModel();
            var info = service.GetById(id);

            model.Id = info.Id;


            model.theme = info.theme;
            model.Guest = info.Guest;
            model.shijian = info.shijian;
            model.Address = info.Address;
            //是否开启网友提问
            model.IsEnabled = info.IsEnabled;


            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            OnlineInterviewModel model = new OnlineInterviewModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View();


        }



        public ActionResult Display(Guid id, OnlineInterviewQuestionAskCondition condition)
        {
            OnlineInterviewModel model = new OnlineInterviewModel();
            var info = service.GetById(id);


            ViewBag.Id = info.Id;


            ViewBag.theme = info.theme;
            ViewBag.Guest = info.Guest;
            ViewBag.shijian = info.shijian;
            ViewBag.Address = info.Address;
            //是否开启网友提问
            ViewBag.IsEnabled = info.IsEnabled;
         
            condition.OnlineInterviewId=info.Id;
            var news = IOnlineInterviewQuestionAskService.SearchAllOnlineInterviewQuestionAsk(condition);
            var models = new PagedList<OnlineInterviewQuestionAsk>(news, news.Paging);
    
       
       
      
            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), models);
        }


        public JsonResult getOnlineInterview(Guid Id)
        {
            OnlineInterviewModel model = new OnlineInterviewModel();
            var info = service.GetById(Id);

            model.Id = info.Id;


            model.theme = info.theme;
            model.Guest = info.Guest;
            model.shijian = info.shijian;
            model.Address = info.Address;
            //是否开启网友提问
            model.IsEnabled = info.IsEnabled;

          
            return Json(new { list = model });
        }


     


    }
}
