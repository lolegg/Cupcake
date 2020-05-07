using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceQuestionnaireAnswer
    {
        public IList<Questionnaire_QuestionResult> GetListByQuestionInfoId(Guid questionInfoId)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format(" select  * from Questionnaire_QuestionResult where IsDelete=0 and QuestionInfoId='{0}'  order by CreateDate ", questionInfoId);
                return entity.Database.SqlQuery<Questionnaire_QuestionResult>(sqlstr).ToList();
            }
        }
    }
}