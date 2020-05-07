using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class Organization : FrameworkBaseEntity
    {
        public int UARCId { get; set; }

        public int UARCPId { get; set; }

        public OrganizationType Type { get; set; }

        
        public string Name { get; set; }

        public Guid Pid { get; set; }

        public virtual IList<User> Users { get; set; }
    }

    public class OrganizationExt : FrameworkBaseEntity 
    {
        public int UARCId { get; set; }
        public int UARCPId { get; set; }
        public OrganizationType type { get; set; }

        public Guid? Pid { get; set; }
        public string Name { get; set; }

        public int SubMenuNums { get; set; }

    }


    public class OrganizationCondition : Condition
    {
        public string Name { get; set; }
        public Guid? OrganizationId { get; set; }
        public OrganizationType? Type { get; set; }
    }
}
