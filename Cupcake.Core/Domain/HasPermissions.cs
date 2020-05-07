using System;

namespace Cupcake.Core.Domain
{
    public class HasPermissions : FrameworkBaseEntity
    {
        public Guid OwnerId { get; set; }
        public Guid? MenuId { get; set; }
        public Guid PermissionId { get; set; }
    }

    public class HasPermissionsCondition : Condition
    {
        public Guid RoleId { get; set; }
        public Guid NodeId { get; set; }
    }
}
