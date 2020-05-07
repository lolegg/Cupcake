using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cupcake.Plugin.Questionnaire.Services
{
    public partial class QuestionnaireAnswerService : IQuestionnaireAnswerService
    {
        private readonly IRepository<QuestionnaireAnswer> repository;
        public QuestionnaireAnswerService(IRepository<QuestionnaireAnswer> repository)
        {
            this.repository = repository;
        }

        public IPagedList<QuestionnaireAnswer> SearchQuestionnaireAnswer(QuestionnaireAnswerCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.QuestionInfoId != null)
                query = query.Where(t => t.QuestionInfoId == condition.QuestionInfoId);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<QuestionnaireAnswer>(query, condition.PageIndex, condition.PageSize);
        }


        public void Add(QuestionnaireAnswer info)
        {
            var query = repository.Table;
            repository.Insert(info);

        }
        public void Update(QuestionnaireAnswer info)
        {
            var query = repository.Table;
            repository.Update(info);

        }

        public QuestionnaireAnswer GetById(Guid Id)
        {
            var query = repository.GetById(Id);
            return query;
        }


        public virtual QuestionnaireAnswer GetQuestionInfoId(Guid QuestionInfoId)
        {
            var query = from gp in repository.Table
                        where gp.QuestionInfoId == QuestionInfoId
                        orderby gp.Id
                        select gp;
            var record = query.FirstOrDefault();
            return record;
        }

        //public int BatchDelete(Expression<Func<QuestionnaireAnswer, bool>> filterExpression)
        //{
        //    using (var repository = new Repository<QuestionnaireAnswer>())
        //    {
        //        return repository.BatchDelete(filterExpression);
        //    }
        //}

        public void Delete(QuestionnaireAnswer info)
        {
            repository.Delete(info);
        }

        public void DeleteDaan(Guid? id)
        {
            StringBuilder str = new StringBuilder();
            str = str.Append(" delete from dbo.Questionnaire_QuestionResult where QuestionInfoId='"+id+"' ");
            string sql = str.ToString();
            DataSet ds = DBHelper.ExecuteQuery(sql, DBHelper.connectionstring);
            return;
        }

    }
}
