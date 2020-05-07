using Cupcake.Data.Mappings;
using Cupcake.Plugin.Activity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Data
{
    public partial class MessageManagementsMap : PluginBaseEntityMapping<MessageManagements>
    {
        public MessageManagementsMap()
        {
            this.ToTable("Activity_MessageManagements");
        }
    }
}
