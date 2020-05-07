using Cupcake.Data.Mappings;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Data
{
    public partial class QuestionStatisticMap : PluginBaseEntityMapping<QuestionStatistic>
    {
        public QuestionStatisticMap()
        {
            this.ToTable("Questionnaire_QuestionStatistic");
        }
    }

}
