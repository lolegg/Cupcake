using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MemberDengJi.Domain
{
    public class GovernmentPurchasing : PluginBaseEntity
    {
        public Guid? UserId { get; set; }
        public string PurchasingDepartment { get; set; }
        public string ServiceName { get; set; }
        public DateTime? PurchasingTime { get; set; }
        public string ServiceCharge { get; set; }
        public string DetailsSituation { get; set; }
    }
    public class GovernmentPurchasingCondition : Condition
    {
        public string PurchasingDepartment { get; set; }
    }
}
