using System;

namespace Cupcake.Core.Domain
{
    public class HasMenus : FrameworkBaseEntity
    {
        public Guid OwnerId { get; set; }
        public Guid MenuId { get; set; }
    }
}
