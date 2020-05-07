using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class User : FrameworkBaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public ObjectStatus Status { get; set; }
        public Guid? OrganizationId { get; set; }
        public int UARCId { get; set; }

        public int UARCOrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual IList<Role> Roles { get; set; }
    }

    public class UserCondition : Condition
    {
        public string UserName { get; set; }
        public UserType? UserType { get; set; }
    }
}
