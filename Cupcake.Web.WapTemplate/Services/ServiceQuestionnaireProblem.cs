using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceQuestionnaireProblem
    {
        public IList<Questionnaire_QuestionnaireProblem> GetListByQuestionSurveyId(Guid questionSurveyId)
        {
            using (CupcakeEntities entity = new CupcakeEntities()) 
            {
                string sqlstr = string.Format(" select  * from Questionnaire_QuestionnaireProblem where IsDelete=0 and QuestionSurveyId='{0}'  order by CreateDate ", questionSurveyId);
                return entity.Database.SqlQuery<Questionnaire_QuestionnaireProblem>(sqlstr).ToList();
            }
        }
    }
}