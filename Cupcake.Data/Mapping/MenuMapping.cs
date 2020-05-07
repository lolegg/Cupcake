using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class MenuMapping : FrameworkBaseEntityMapping<Menu>
    {
        public MenuMapping()
        {
            this.ToTable("Menu");
            this.Property(m => m.Name).IsRequired();

            //this.HasMany(m => m.Children).WithOptional();
            //this.HasMany(m => m.Permissions).WithRequired(p=>p.Menu).HasForeignKey(p => p.MenuId);
        }
    }
}
