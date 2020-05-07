using Cupcake.Core.Domain;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cupcake.Data.Mappings
{
    /// <summary>
    /// 公共属性Mapping
    /// </summary>
    /// <typeparam name="TEntityType">类型</typeparam>
    public class FrameworkBaseEntityMapping<TEntityType> : EntityTypeConfiguration<TEntityType> where TEntityType : FrameworkBaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrameworkBaseEntityMapping()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.CreateDate).IsRequired();
            this.Property(p => p.UpdateDate).IsRequired();
            this.Property(p => p.IsDelete).IsRequired();

            this.HasOptional(b => b.Creator).WithMany().HasForeignKey(b => b.CreatorId);
        }
    }
}
