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
    public class QuestionnaireAnswerController : Controller
    {
        private readonly IQuestionnaireProblemService service;
        private readonly IQuestionnaireAnswerService QuestionnaireAnswerService;
        public QuestionnaireAnswerController(IQuestionnaireProblemService newsService, IQuestionnaireAnswerService AnswerService)
        {
            this.service = newsService;
            this.QuestionnaireAnswerService = AnswerService;
        }
     
        #region 答案
        public ActionResult Index(Guid QuestionInfoId, Guid QuestionType, Guid? QuestionSurveyId)
        {

            QuestionnaireAnswerService.GetQuestionInfoId(QuestionInfoId);
            QuestionnaireAnswerCondition condition = new QuestionnaireAnswerCondition() { QuestionInfoId = QuestionInfoId, QuestionType = QuestionType };
            var news = QuestionnaireAnswerService.SearchQuestionnaireAnswer(condition);
            var models = new PagedList<QuestionnaireAnswer>(news, news.Paging);
            ViewBag.QuestionInfoId = QuestionInfoId;
            ViewBag.QuestionType = QuestionType;
            ViewBag.ProblemTitle = service.GetById(QuestionInfoId).Title;
            ViewBag.QuestionSurveyId = QuestionSurveyId;
         
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }


        [HttpGet]
        public ActionResult Create(Guid QuestionInfoId,Guid QuestionType)
        {
            QuestionnaireAnswerModel model = new QuestionnaireAnswerModel() { QuestionInfoId = QuestionInfoId, QuestionType = QuestionType };
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [HttpPost]
        public ActionResult Create(QuestionnaireAnswerModel model)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireAnswer info = new QuestionnaireAnswer();
                info.Id = Guid.NewGuid();
                info.Title = model.Title;
                info.QuestionType = model.QuestionType;
                info.QuestionInfoId = model.QuestionInfoId;
                QuestionnaireAnswerService.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
            
        }
     

        public ActionResult Edit(Guid id, Guid QuestionInfoId)
        {
            QuestionnaireAnswerModel model = new QuestionnaireAnswerModel();
            var info = QuestionnaireAnswerService.GetById(id);
            info.QuestionInfoId = QuestionInfoId;
            model.Title = info.Title;
            model.QuestionType = info.QuestionType;
            model.QuestionInfoId = info.QuestionInfoId;
            model.Id = info.Id;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

  

        [HttpPost]
        public ActionResult Edit(QuestionnaireAnswerModel model)
        {
            if (ModelState.IsValid)
            {
                var info = QuestionnaireAnswerService.GetById(model.Id);
                if (info != null)
                {
                    info.Id = model.Id;
                    info.Title = model.Title;
                    info.QuestionType = model.QuestionType;
                    info.QuestionInfoId = model.QuestionInfoId;
                    QuestionnaireAnswerService.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
           
        }

       
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var info = QuestionnaireAnswerService.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                QuestionnaireAnswerService.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View();

        }

       

        #endregion
    }
}
