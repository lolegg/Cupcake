using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class Role : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public virtual IList<User> Users { get; set; }
    }

    public class RoleCondition : Condition
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsDelete { get; set; }
    }
}
