using Cupcake.Core.Domain;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cupcake.Data.Mappings
{
    /// <summary>
    /// 公共属性Mapping
    /// </summary>
    /// <typeparam name="TEntityType">类型</typeparam>
    public class PluginBaseEntityMapping<TEntityType> : EntityTypeConfiguration<TEntityType> where TEntityType : PluginBaseEntity
    {
        public PluginBaseEntityMapping()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.CreateDate).IsRequired();
            this.Property(p => p.UpdateDate).IsRequired();
            this.Property(p => p.IsDelete).IsRequired();
        }
    }
}
