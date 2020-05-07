using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Cupcake.Plugin.Questionnaire.Services
{
    public partial class QuestionStatisticService : IQuestionStatisticService
    {
        private readonly IRepository<QuestionStatistic> repository;
        public QuestionStatisticService(IRepository<QuestionStatistic> repository)
        {
            this.repository = repository;
        }

        public IPagedList<QuestionStatistic> SearchQuestionStatistic(QuestionStatisticCondition condition)
        {
            var query = repository.Table;


            if (!string.IsNullOrEmpty(condition.Result))
                query = query.Where(t => t.Result.Contains(condition.Result));

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<QuestionStatistic>(query, condition.PageIndex, condition.PageSize);
        }
        public IList<QuestionStatistic> GetListById(Guid QuestionSurveyId, Guid CreatorId, QuestionStatisticCondition condition)
        {
            var query = repository.Table;

            query = query.Where(u => u.IsDelete == false && u.QuestionSurveyId == QuestionSurveyId && u.UserId == CreatorId);
                return new PagedList<QuestionStatistic>(query, condition.PageIndex, condition.PageSize);
           
        }
        public List<QuestionStatisticResult> GetResult(QuestionStatisticCondition condition)
        {
            using (var repository = new Repository<QuestionStatisticResult>())
            {
                List<SqlParameter> pars = new List<SqlParameter>();
                if (condition.QuestionSurveyId.HasValue)
                {
                    pars.Add(new SqlParameter("@QuestionSurveyId", condition.QuestionSurveyId));
                }
                string sql = @"
                        SELECT qs.QuestionSurveyId,qs.QuestionInfoId,qs.QuestionResultId,qes.Title QuestionSurveyName,
                        qi.Title QuestionInfoName,ISNULL(qr.Title,qs.Result) QuestionResultName,COUNT(case when qs.QuestionResultId!='00000000-0000-0000-0000-000000000000' then 1 else null end) AS Count 
                        FROM dbo.Questionnaire_QuestionStatistic qs
                        JOIN Questionnaire_QuestionSurvey qes ON qs.QuestionSurveyId=qes.Id 
                        JOIN dbo.Questionnaire_QuestionnaireProblem qi ON qs.QuestionInfoId=qi.Id
                        LEFT JOIN dbo.Questionnaire_QuestionResult qr ON qs.QuestionResultId=qr.Id
                        WHERE qs.QuestionSurveyId=@QuestionSurveyId
                        GROUP BY qs.QuestionSurveyId,qes.Title,qi.Title,qr.Title,qs.QuestionInfoId,qs.QuestionResultId,qs.Result
                        ORDER BY COUNT,qs.QuestionInfoId DESC";
                return repository.GetwithdbSql(sql, pars.ToArray()).ToList();
            }
        }
        //public void Add(QuestionStatistic info)
        //{
        //    var query = repository.Table;
        //    repository.Insert(info);

        //}
        //public void Update(QuestionStatistic info)
        //{
        //    var query = repository.Table;
        //    repository.Update(info);

        //}

        public QuestionStatistic GetById(Guid Id)
        {
            var query = repository.GetById(Id);
            return query;
        }

        //public void Delete(QuestionnaireInfo info)
        //{
        //    repository.Delete(info);
        //}
    }
}
