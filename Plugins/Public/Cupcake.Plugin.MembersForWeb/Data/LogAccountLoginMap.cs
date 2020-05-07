using Cupcake.Data.Mappings;
using Cupcake.Plugin.MembersForWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.MembersForWeb.Data
{
   
    public partial class LogAccountLoginMap : PluginBaseEntityMapping<LogAccountLogin>
    {
        public LogAccountLoginMap()
        {
            this.ToTable("LogAccountLogin");
        }
    }
}
