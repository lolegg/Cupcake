using Cupcake.Core;
using Cupcake.Plugin.Questionnaire.Domain;
using Cupcake.Plugin.Questionnaire.Services;
using Cupcake.Plugin.Questionnaire;
using System.Web.Mvc;
using Cupcake.Plugin.Questionnaire.Models;
using Cupcake.Core.Domain;
using System;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Questionnaire.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly IQuestionnaireService service;
        private readonly IQuestionStatisticService Statisticservice;
        public QuestionnaireController(IQuestionnaireService newsService, IQuestionStatisticService Statisticservice)
        {
            this.service = newsService;
            this.Statisticservice = Statisticservice;
        }
        #region 问卷调查
        public ActionResult Index(QuestionnaireCondition condition)
        {
            var news = service.SearchQuestionnaire(condition);
            var models = new PagedList<QuestionnaireInfo>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            QuestionnaireModel model = new QuestionnaireModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [HttpPost]
        public ActionResult Create(QuestionnaireModel model)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireInfo info = new QuestionnaireInfo();
                info.Id = Guid.NewGuid();
                info.Title = model.Title;
                info.Desc = model.Desc;
                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            QuestionnaireModel model = new QuestionnaireModel();
            var info = service.GetById(id);
            model.Title = info.Title;
            model.Desc = info.Desc;
            model.Id = info.Id;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpPost]
        public ActionResult Edit(QuestionnaireModel model)
        {
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info.Id = model.Id;
                    info.Title = model.Title;
                    info.Desc = model.Desc;
                    service.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }
    
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            QuestionnaireModel model = new QuestionnaireModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }

        public ActionResult Activate(Guid id)
        {
            QuestionnaireModel model = new QuestionnaireModel();
            var info = service.GetById(id);
            if (info != null)
            {
                if (info.IsRelease == true)
                    info.IsRelease = false;
                else
                    info.IsRelease = true;
                    info.UpdateDate = DateTime.Now;

                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Activate"), model);
        } 
        #endregion
        [HttpGet]
        public ActionResult Statistical(Guid id)
        {
            QuestionStatisticCondition condition = new QuestionStatisticCondition() { QuestionSurveyId = id };
            QuestionStatisticList model = new QuestionStatisticList();
            model.List = Statisticservice.GetResult(condition);
            return View(PluginHelper.GetViewPath(this.GetType(), "Statistical"), model);
         
        }
    
    }
}
