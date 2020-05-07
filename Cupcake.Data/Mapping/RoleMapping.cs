using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class RoleMapping : FrameworkBaseEntityMapping<Role>
    {
        public RoleMapping()
        {
            this.ToTable("Roles");
            this.Property(p => p.Name).IsRequired();            
        }
    }
}
