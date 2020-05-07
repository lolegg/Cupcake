using Cupcake.Core.Domain;
using System;


namespace Cupcake.Plugin.Activity.Domain
{
    public class MessageManagements : PluginBaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? SubordinateActivitiesID { get; set; }
        public string MessageName { get; set; }
        public string MessageConent { get; set; }
        public DateTime? MessageDate { get; set; }
    }
    public class MessageManagementsCondition : Condition
    {
        public string MessageConent { get; set; }
    }
}
