using Cupcake.Data.Mappings;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Data
{

    public partial class QuestionnaireProblemMap : PluginBaseEntityMapping<QuestionnaireProblem>
        {
            public QuestionnaireProblemMap()
            {
                this.ToTable("Questionnaire_QuestionnaireProblem");
            }
        }
    
   
}
