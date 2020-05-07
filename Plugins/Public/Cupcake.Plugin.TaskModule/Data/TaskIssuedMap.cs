using Cupcake.Data.Mappings;
using Cupcake.Plugin.TaskModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.TaskModule.Data
{
    public partial class TaskIssuedMap : PluginBaseEntityMapping<TaskIssuedInfo>
    {
        public TaskIssuedMap()
        {
            this.ToTable("TaskModule_TaskIssued");
        }
    }
}
