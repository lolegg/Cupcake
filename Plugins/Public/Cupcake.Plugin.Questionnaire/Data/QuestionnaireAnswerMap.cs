using Cupcake.Data.Mappings;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Data
{
    public class QuestionnaireAnswerMap : PluginBaseEntityMapping<QuestionnaireAnswer>
    {
        public QuestionnaireAnswerMap() {
            this.ToTable("Questionnaire_QuestionResult");
        }
    }
}
