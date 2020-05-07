using System;

namespace Cupcake.Core.Domain
{
    public abstract class PluginBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Guid? CreatorId { get; set; }
        public PluginBaseEntity()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            IsDelete = false;             
        }
    }
}
