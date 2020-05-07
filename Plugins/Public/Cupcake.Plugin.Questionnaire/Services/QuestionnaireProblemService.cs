using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.Questionnaire.Services
{
    public partial class QuestionnaireProblemService : IQuestionnaireProblemService
    {
        private readonly IRepository<QuestionnaireProblem> repository;
        public QuestionnaireProblemService(IRepository<QuestionnaireProblem> repository)
        {
            this.repository = repository;
        }

        public IPagedList<QuestionnaireProblem> SearchQuestionnaireProblem(QuestionnaireProblemCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.QuestionSurveyId != null)
                query = query.Where(t => t.QuestionSurveyId == condition.QuestionSurveyId);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<QuestionnaireProblem>(query, condition.PageIndex, condition.PageSize);
        }


        public void Add(QuestionnaireProblem info)
        {
            var query = repository.Table;
            repository.Insert(info);

        }
        public void Update(QuestionnaireProblem info)
        {
            var query = repository.Table;
            repository.Update(info);

        }

        public QuestionnaireProblem GetById(Guid Id)
        {
            var query = repository.GetById(Id);
            return query;
        }

      
        public virtual QuestionnaireProblem GetQuestionSurveyId(Guid QuestionSurveyId)
        {
            var query = from gp in repository.Table
                        where gp.QuestionSurveyId == QuestionSurveyId
                        orderby gp.Id
                        select gp;
            var record = query.FirstOrDefault();
            return record;
        }



        public void Delete(QuestionnaireProblem info)
        {
            repository.Delete(info);
        }


    }
}
