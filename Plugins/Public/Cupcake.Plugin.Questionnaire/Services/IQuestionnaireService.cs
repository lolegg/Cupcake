using System;
using System.Collections.Generic;
namespace Cupcake.Plugin.Questionnaire.Services
{
    public interface IQuestionnaireService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Questionnaire.Domain.QuestionnaireInfo> SearchQuestionnaire(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireCondition condition);

     

        void Add(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireInfo info);
        void Update(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireInfo info);

        Cupcake.Plugin.Questionnaire.Domain.QuestionnaireInfo GetById(Guid Id);
        void Delete(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireInfo info); 
    }
}
