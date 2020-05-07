using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceQuestionnair
    {
        public List<Questionnaire_QuestionSurvey> GetAll()
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format(" select  * from Questionnaire_QuestionSurvey where IsDelete=0 and IsRelease=1  order by CreateDate ");
                return entity.Database.SqlQuery<Questionnaire_QuestionSurvey>(sqlstr).ToList();
            }
        }

        public string GetQuestionSurveyDesc(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var info = entity.Questionnaire_QuestionSurvey.Where(p => p.Id == id).SingleOrDefault();

                if (info != null)
                {
                    return info.Desc;
                }
                else
                {
                    return "";
                }
            }
        }

        public string  GetQuestionSurveyTitle(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var info= entity.Questionnaire_QuestionSurvey.Where(p => p.Id == id).SingleOrDefault();

                if (info != null)
                {
                    return info.Title;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}