using Cupcake.Data.Mappings;
using Cupcake.Plugin.SystemMessage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.SystemMessage.Data
{
   public class SystemMessageInfoMap: PluginBaseEntityMapping<SystemMessageInfo>
    {
       public SystemMessageInfoMap()
        {
            this.ToTable("SystemMessageInfo");
        }
    }
}
