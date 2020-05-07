using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get;set;}
        public virtual IList<Role> Roles { get; set; }
                
    }

    public class PermissionCondition : Condition
    {
        public Guid? MenuId { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }
    }
}
