using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.CalendarAndScheduling.Domain
{
    public class CalendarAndSchedulingInfo : PluginBaseEntity
    {
        public string Title { get; set; }

        public DateTime? DateT { get; set; }
        public int dayNum { get; set; }

        public int month { get; set; }
        public string Content { get; set; }
        public Guid ImportantLevel { get; set; }

        public Guid UserId { get; set; }
    }
    public class CalendarAndSchedulingCondition : Condition
    {
        public string Title { get; set; }

        public DateTime? DateT { get; set; }
        
        public Guid? ImportantLevel { get; set; }
        public Guid UserId { get; set; }
    }
}
