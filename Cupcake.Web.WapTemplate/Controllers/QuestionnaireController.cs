using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Helper;
using Cupcake.Web.WapTemplate.Models;
using Cupcake.Web.WapTemplate.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Cupcake.Web.WapTemplate.Controllers
{

    public class QuestionnaireController : Controller
    {
        //
        // GET: /Questionnaire/

        public ActionResult Index(QuestionnaireCondition condition)
        {
            ServiceQuestionnair service = new ServiceQuestionnair();          
            var list = service.GetAll();         
            QuestionSurveyListModel model = new QuestionSurveyListModel(list);
            return View(model);
        }

        public ActionResult QuestionInfo(Guid QuestionSurveyId)
        {
            ServiceQuestionnaireProblem qis = new ServiceQuestionnaireProblem();
            ServiceQuestionnaireAnswer qrs = new ServiceQuestionnaireAnswer();
            ServiceQuestionnair qss = new ServiceQuestionnair();
            List<QuestionnaireProblemCondition> QuestionInfoList = new List<QuestionnaireProblemCondition>();
            var list = qis.GetListByQuestionSurveyId(QuestionSurveyId);
            for (int i = 0; i < list.Count; i++)
            {
                var questionResultList = qrs.GetListByQuestionInfoId(list[i].Id);
                QuestionnaireProblemCondition info = new QuestionnaireProblemCondition() { Id = list[i].Id, Title = list[i].Title, QuestionSurveyId = list[i].QuestionSurveyId, QuestionType = list[i].QuestionType, IsRequired = list[i].IsRequired, QuestionResultList = questionResultList };
                QuestionInfoList.Add(info);
            }
           ViewBag.QuestionSurveyDesc = qss.GetQuestionSurveyDesc(QuestionSurveyId);
           ViewBag.QuestionSurveyTitle = qss.GetQuestionSurveyTitle(QuestionSurveyId);
            ViewBag.QuestionInfoList = QuestionInfoList;
            return PartialView();
        }

        public JsonResult SaveResult(string[] array, Guid QuestionSurveyId)
        {
            //if (SessionHelper.useridguid == null)
            //    return Json(new { result = "false" });
            bool r = false;
            ServiceQuestionStatistic service = new ServiceQuestionStatistic();
            //var data = service.GetListById(QuestionSurveyId, SessionHelper.useridguid);
            //if (data.Count > 0)
            //    return Json(new { result = "exists" });
            for (int i = 0; i < array.Length; i++)
            {
                var list = JsonConvert.DeserializeObject<List<Questionnaire_QuestionStatistic>>(array[i]);
                for (int j = 0; j < list.Count; j++)
                {
                    Questionnaire_QuestionStatistic info = new Questionnaire_QuestionStatistic() { Id = Guid.NewGuid(), QuestionSurveyId = QuestionSurveyId, QuestionInfoId = list[j].QuestionInfoId, QuestionResultId = list[j].QuestionResultId, QuestionType = list[j].QuestionType, Result = list[j].Result, UserId = Guid.NewGuid(),  CreateDate = DateTime.Now, UpdateDate = DateTime.Now };//, UserId = SessionHelper.useridguid 
            
                   r= service.Add(info);
                   
                }
            }
            if (r == true)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }
        //[HttpPost]
        //public JsonResult IndexPageSync(QuestionnaireCondition condition)
        //{
        //    ServiceQuestionnair service = new ServiceQuestionnair();
        //    condition.IsRelease = IsRelease.yes;
        //   // Paging paging = new Paging(condition);
        //    var modellist = service.GetListByCondition(condition, ref paging);
        //    QuestionSurveyListModel model = new QuestionSurveyListModel(modellist);
        //    model.Paging = paging;
        //    return Json(new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(model), JsonRequestBehavior.AllowGet);
        //}
	}
}