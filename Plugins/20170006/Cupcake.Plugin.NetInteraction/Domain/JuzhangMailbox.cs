using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Domain
{
  public  class JuzhangMailbox : PluginBaseEntity
    {
      public int nian { get; set; }
      public int yue { get; set; }

      public DateTime? shijian { get; set; }
    }
}
