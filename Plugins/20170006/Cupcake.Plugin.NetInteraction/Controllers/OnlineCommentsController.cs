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
    public partial class OnlineCommentsController : Controller
    {
        private readonly IOnlineCommentsService service;
        private readonly IOnlineCommentsOpinionService IOnlineCommentsOpinionService;
        public OnlineCommentsController(IOnlineCommentsService OnlineCommentsService, IOnlineCommentsOpinionService IOnlineCommentsOpinionService)
        {
            this.service = OnlineCommentsService;
            this.IOnlineCommentsOpinionService = IOnlineCommentsOpinionService;
        }
        public ActionResult Index(OnlineCommentsCondition condition)
        {


            var news = service.SearchAllOnlineComments(condition);
            var models = new PagedList<OnlineComments>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create()
        {

            OnlineCommentsModel model = new OnlineCommentsModel();


            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(OnlineCommentsModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineComments info = new OnlineComments();
                info.Id = Guid.NewGuid();

                info.Title = model.Title;

                info.Content = model.Content;


                ////背景介绍
                //info.BackgroundIntroduction = model.BackgroundIntroduction;
                ////草案全文
                //info.DraftFullText = model.DraftFullText;
                ////公众意见与建议
                //info.Opinion = model.Opinion;
                ////公众意见采纳情况反馈
                //info.Feedback = model.Feedback;
                //是否结束
                info.IsEnd = model.IsEnd;

                info.StartTime = model.StartTime;
                info.EndTime = model.EndTime;


                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            OnlineCommentsModel model = new OnlineCommentsModel();
            var info = service.GetById(id);


            model.Content = info.Content;

            model.Id = info.Id;
            model.Title = info.Title;



            ////背景介绍
            //model.BackgroundIntroduction = info.BackgroundIntroduction;
            ////草案全文
            //model.DraftFullText = info.DraftFullText;
            ////公众意见与建议
            //model.Opinion = info.Opinion;
            ////公众意见采纳情况反馈
            //model.Feedback = info.Feedback;
            //是否结束
            model.IsEnd = info.IsEnd;

            model.StartTime = info.StartTime;
            model.EndTime = info.EndTime;

            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(OnlineCommentsModel model)
        {
            if (ModelState.IsValid)
            {



                OnlineComments info = service.GetById(model.Id);

                info.Title = model.Title;

                info.Content = model.Content;


                ////背景介绍
                //info.BackgroundIntroduction = model.BackgroundIntroduction;
                ////草案全文
                //info.DraftFullText = model.DraftFullText;
                ////公众意见与建议
                //info.Opinion = model.Opinion;
                ////公众意见采纳情况反馈
                //info.Feedback = model.Feedback;
                //是否结束
                info.IsEnd = model.IsEnd;
                info.StartTime = model.StartTime;
                info.EndTime = model.EndTime;


                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            OnlineCommentsModel model = new OnlineCommentsModel();
            var info = service.GetById(id);


            model.Content = info.Content;
            model.Title = info.Title;


            ////背景介绍
            //model.BackgroundIntroduction = info.BackgroundIntroduction;
            ////草案全文
            //model.DraftFullText = info.DraftFullText;
            ////公众意见与建议
            //model.Opinion = info.Opinion;
            ////公众意见采纳情况反馈
            //model.Feedback = info.Feedback;
            //是否结束
            model.IsEnd = info.IsEnd;
            model.StartTime = info.StartTime;
            model.EndTime = info.EndTime;
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            OnlineCommentsModel model = new OnlineCommentsModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View();


        }


        public ActionResult Display(Guid id)
        {
            OnlineCommentsModel model = new OnlineCommentsModel();
            var info = service.GetById(id);


            model.Id = info.Id;


            model.Title = info.Title;
            model.Content = info.Content;
            model.IsEnd = info.IsEnd;
            model.StartTime = info.StartTime;
            //是否开启网友提问
            model.EndTime = info.EndTime;


            OnlineCommentsOpinionCondition condition = new OnlineCommentsOpinionCondition();
            condition.OnlineCommentsId = info.Id;
            var news = IOnlineCommentsOpinionService.SearchAllOnlineCommentsOpinion(condition);
            var models = new PagedList<OnlineCommentsOpinion>(news, news.Paging);

            model.OnlineCommentsOpinionInfo = models;
            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);
        }


    }
}
