using System;

namespace Cupcake.Core.Domain
{
    public class HasMedias : FrameworkBaseEntity
    {
        public Guid OwnerId { get; set; }
        public Guid MediaId { get; set; }
        public string FieldName { get; set; }
    }
}
