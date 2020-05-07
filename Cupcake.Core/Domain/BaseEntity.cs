using System;

namespace Cupcake.Core.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Guid? CreatorId { get; set; }
        public BaseEntity()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            IsDelete = false;
        }
    }

    public abstract class FrameworkBaseEntity : BaseEntity
    {
        public virtual User Creator { get; set; }
    }

    public abstract class PluginBaseEntity : BaseEntity
    {
    }
}
