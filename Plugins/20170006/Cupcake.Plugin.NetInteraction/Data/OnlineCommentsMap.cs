using Cupcake.Data.Mappings;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Data
{
    public class OnlineCommentsMap : PluginBaseEntityMapping<OnlineComments>
    {
      public OnlineCommentsMap()
        {
            this.ToTable("OnlineComments");
        }
    }
}
