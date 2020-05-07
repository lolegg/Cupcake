using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Domain
{
    public class ActivityStyle : PluginBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string Address { get; set; }
        public bool IsTop { get; set; }
        public string Imgurl { get; set; }
    }
    public class ActivityStyleCondition : Condition
    {
        public string Title { get; set; }
    }
}
