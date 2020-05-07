using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceQuestionStatistic
    {
        public IList<Questionnaire_QuestionStatistic> GetListById(Guid QuestionSurveyId, Guid CreatorId)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format(" select  * from Questionnaire_QuestionStatistic where IsDelete=0 and QuestionSurveyId='{0}' and  UserId='{1}'  order by CreateDate ", QuestionSurveyId,CreatorId);
                return entity.Database.SqlQuery<Questionnaire_QuestionStatistic>(sqlstr).ToList();
            }
        }
        public bool Add(Questionnaire_QuestionStatistic model)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                entity.Questionnaire_QuestionStatistic.Add(model);
                return entity.SaveChanges()>0;
            }
        }

     
    }
}