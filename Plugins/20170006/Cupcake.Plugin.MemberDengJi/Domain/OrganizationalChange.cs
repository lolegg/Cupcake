using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MemberDengJi.Domain
{
    public class OrganizationalChange : PluginBaseEntity
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; }
        public string MailBox { get; set; }
        public string OrganizationalIntroduce { get; set; }
    }
    public class OrganizationalChangeCondition : Condition
    {
        public string UserName { get; set; }
    }
}
