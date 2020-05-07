using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Core.Domain
{
    public class CustomPermissions: FrameworkBaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        public Guid? MenuId { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }


    public class BasePermissionExt : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        public Guid? MenuId { get; set; }

        public Guid? HasPermission { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }


    public class CustomPermissionsCondition : Condition
    {
        public string Name { get; set; }
        public Guid NodeId { get; set; }
        public string Desc { get; set; }
    }
}
