using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.MembersForWeb.Domain
{
 
    public class LogAccountLogin : PluginBaseEntity
    {
        public Guid Mid { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string LoginIP { get; set; }
    }
}
