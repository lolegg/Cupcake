using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class CustomPermissionsMapping: FrameworkBaseEntityMapping<CustomPermissions>
    {
        public CustomPermissionsMapping()
        {
            this.ToTable("CustomPermissions");
            this.Property(p => p.Name).IsRequired();
        }
    }
}
