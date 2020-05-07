using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class HasPermissionsMapping : FrameworkBaseEntityMapping<HasPermissions>
    {
        public HasPermissionsMapping()
        {
            this.ToTable("HasPermissions");
        }
    }
}
