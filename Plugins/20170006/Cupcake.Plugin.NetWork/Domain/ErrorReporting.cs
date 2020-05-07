using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetWork.Domain
{
    public class ErrorReporting : PluginBaseEntity
    {
        public string Title {get;set;}
        public Guid? TheTitle { get; set;}
        public string Content { get; set;}
    }
    public class ErrorReportingCondition : Condition
    {
        public string Title { get; set; }
        public Guid? TheTitle { get; set; }
    }
}
