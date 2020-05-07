using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Domain
{
   public class OnlineCommentsOpinion : PluginBaseEntity
    {
       public Guid OnlineCommentsId { get; set; }
       public Guid UserId { get; set; }
       public string UserName { get; set; }
       public string Content { get; set; }
    }


   public class OnlineCommentsOpinionCondition : Condition
   {
       public string Content { get; set; }
       public Guid OnlineCommentsId { get; set; }
   }
}
