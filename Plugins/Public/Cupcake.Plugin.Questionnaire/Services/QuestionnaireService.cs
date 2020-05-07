using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.Questionnaire.Services
{
    public partial class QuestionnaireService : IQuestionnaireService
    {
        private readonly IRepository<QuestionnaireInfo> repository;
        public QuestionnaireService(IRepository<QuestionnaireInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<QuestionnaireInfo> SearchQuestionnaire(QuestionnaireCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            //if (condition.NewsType != null)
            //    query = query.Where(t => t.NewsType == condition.NewsType);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<QuestionnaireInfo>(query, condition.PageIndex, condition.PageSize);
        }


        public void Add(QuestionnaireInfo info)
        {
            var query = repository.Table;
            repository.Insert(info);

        }
        public void Update(QuestionnaireInfo info)
        {
            var query = repository.Table;
            repository.Update(info);

        }

        public QuestionnaireInfo GetById(Guid Id)
        {
            var query = repository.GetById(Id);
            return query;
        }

        public void Delete(QuestionnaireInfo info)
        {
            repository.Delete(info);
        }
    }
}
