using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.SystemMessage.Domain
{
   public class SystemMessageInfo : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
   public class SystemMessageInfoCondition : Condition
   {
       public string Title { get; set; }
   }
}
