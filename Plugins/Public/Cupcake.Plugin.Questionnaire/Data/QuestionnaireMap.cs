using Cupcake.Data.Mappings;
using Cupcake.Plugin.Questionnaire.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Questionnaire.Data
{
    public partial class QuestionnaireMap : PluginBaseEntityMapping<QuestionnaireInfo>
    {
        public QuestionnaireMap()
        {
            this.ToTable("Questionnaire_QuestionSurvey");
        }
    }

   
}