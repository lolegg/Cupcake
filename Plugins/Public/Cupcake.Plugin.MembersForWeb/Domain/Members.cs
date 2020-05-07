using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MembersForWeb.Domain
{
    public class Members : PluginBaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRealName { get; set; }
        public string Tel { get; set; }
        public Guid UserType { get; set; }
        public string Email { get; set; }
      
        public Guid Status { get; set; }

        public string TempPwd { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int LoginCount { get; set; }

        public string LastLoginIP { get; set; }

        public string ImageUrl { get; set; }

        public string ValidCode { get; set; }

        public bool Remember { get; set; }
        public int failLoginCount { get; set; }
    }
    public class MembersCondition : Condition
    {
        public string UserName { get; set; }
        public Guid? UserType { get; set; }
        public DateTime? LastLoginDateBegin { get; set; }
        public DateTime? LastLoginDateEnd { get; set; }
      
    }
}
