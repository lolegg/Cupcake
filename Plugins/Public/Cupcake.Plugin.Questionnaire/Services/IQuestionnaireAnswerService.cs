using System;
using System.Collections.Generic;
namespace Cupcake.Plugin.Questionnaire.Services
{
    public interface IQuestionnaireAnswerService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswer> SearchQuestionnaireAnswer(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswerCondition condition);


        Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswer GetQuestionInfoId(Guid QuestionInfoId);


        void DeleteDaan(Guid? id);

        void Add(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswer info);
        void Update(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswer info);

        Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswer GetById(Guid Id);
        void Delete(Cupcake.Plugin.Questionnaire.Domain.QuestionnaireAnswer info);

        
       
    }
}
