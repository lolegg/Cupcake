using Cupcake.Data.Mappings;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Data
{
  public  class OnlineInterviewQuestionAskMap: PluginBaseEntityMapping<OnlineInterviewQuestionAsk>
    {
        public OnlineInterviewQuestionAskMap()
        {
            this.ToTable("OnlineInterviewQuestionAsk");
        }
    }
}
