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
    public class QuestionnaireProblemController : Controller
    {
        private readonly IQuestionnaireProblemService service;
        private readonly IQuestionnaireService QuestionnaireService;
        private readonly IQuestionnaireAnswerService AnswerService;
        public QuestionnaireProblemController(IQuestionnaireProblemService newsService, IQuestionnaireService QuestionnaireService, IQuestionnaireAnswerService AnswerService)
        {
            this.service = newsService;
            this.QuestionnaireService = QuestionnaireService;
            this.AnswerService = AnswerService;
        }
     
        #region 问题
         public ActionResult Index(Guid QuestionSurveyId,string Title)
        {

            service.GetQuestionSurveyId(QuestionSurveyId);

            QuestionnaireProblemCondition condition = new QuestionnaireProblemCondition() { QuestionSurveyId = QuestionSurveyId,Title=Title };
            var news = service.SearchQuestionnaireProblem(condition);
            var models = new PagedList<QuestionnaireProblem>(news, news.Paging);
            ViewBag.QuestionSurveyId = QuestionSurveyId;
            ViewBag.QuestionTitle = QuestionnaireService.GetById(QuestionSurveyId).Title;
           
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }


        [HttpGet]
        public ActionResult Create(Guid QuestionSurveyId)
        {
            QuestionnaireProblemModel model = new QuestionnaireProblemModel() { QuestionSurveyId = QuestionSurveyId };
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [HttpPost]
        public ActionResult Create(QuestionnaireProblemModel model)
        {
            if (ModelState.IsValid)
            {
                QuestionnaireProblem info = new QuestionnaireProblem();
                info.Id = Guid.NewGuid();
                info.Title = model.Title;
                info.QuestionType = model.QuestionType;
                info.QuestionSurveyId = model.QuestionSurveyId;
                info.IsRequired = model.IsRequired;
                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
            
        }

        public ActionResult Edit(Guid id, Guid QuestionSurveyId)
        {
            QuestionnaireProblemModel model = new QuestionnaireProblemModel();
            var info = service.GetById(id);
            info.QuestionSurveyId = QuestionSurveyId;
            model.Title = info.Title;
            model.QuestionType = info.QuestionType;
            model.QuestionSurveyId = info.QuestionSurveyId;
            model.IsRequired = info.IsRequired;
            model.Id = info.Id;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpPost]
        public ActionResult Edit(QuestionnaireProblemModel model)
        {
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                var aninfo = AnswerService.GetById(model.Id);
                if (info != null)
                {
                    //如果更改答案类型，则原有答案将被删除
                    if (model.QuestionType != info.QuestionType)
                    {
                        AnswerService.DeleteDaan(model.Id);
                    }
                    info.Id = model.Id;
                    info.Title = model.Title;
                    info.QuestionType = model.QuestionType;
                    info.QuestionSurveyId = model.QuestionSurveyId;
                    info.IsRequired = model.IsRequired;
                    service.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
           
        }

       
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View();

        }

       

        #endregion
    }
}
