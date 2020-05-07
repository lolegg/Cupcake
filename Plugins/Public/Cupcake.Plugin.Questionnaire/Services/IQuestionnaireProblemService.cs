using System;
using System.Collections.Generic;
namespace Cupcake.Plugin.Questionnaire.Services
{
    public interface IQuestionnaireProblemService
    {
         Cupcake.Core.IPagedList<Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblem> SearchQuestionnaireProblem(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblemCondition condition);


         Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblem GetQuestionSurveyId(Guid QuestionSurveyId);
       
 
         void Add(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblem info);
         void Update(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblem info);

         Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblem GetById(Guid Id);
         void Delete(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireProblem info); 
    }
}
