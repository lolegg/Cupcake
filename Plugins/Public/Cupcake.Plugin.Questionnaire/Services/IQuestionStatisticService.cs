using System;
using System.Collections.Generic;
namespace Cupcake.Plugin.Questionnaire.Services
{
    public interface IQuestionStatisticService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Questionnaire.Domain.QuestionStatistic> SearchQuestionStatistic(Cupcake.Plugin.Questionnaire.Domain.QuestionStatisticCondition condition);

        //void Add(Cupcake.Plugin.Questionnaire.Domain.QuestionStatistic info);
        //void Update(Cupcake.Plugin.Questionnaire.Domain.QuestionStatistic info);

        Cupcake.Plugin.Questionnaire.Domain.QuestionStatistic GetById(Guid Id);
        //void Delete(Cupcake.Plugin.Questionnaire.Domain.QuestionStatistic info); 

        List<Cupcake.Plugin.Questionnaire.Domain.QuestionStatisticResult> GetResult(Cupcake.Plugin.Questionnaire.Domain.QuestionStatisticCondition condition);
    }
}
