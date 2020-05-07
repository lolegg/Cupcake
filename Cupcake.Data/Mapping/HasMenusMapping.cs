using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class HasMenusMapping : FrameworkBaseEntityMapping<HasMenus>
    {
        public HasMenusMapping()
        {
            this.ToTable("HasMenus");
            this.Property(e => e.OwnerId).IsRequired();
            this.Property(e => e.MenuId).IsRequired();
        }
    }
}
