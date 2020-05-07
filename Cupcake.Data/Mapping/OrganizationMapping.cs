using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class OrganizationMapping : FrameworkBaseEntityMapping<Organization>
    {
        public OrganizationMapping()
        {
            this.Property(m => m.UARCId).IsOptional();
            this.Property(m => m.Name).IsRequired();

            //this.HasMany(m => m.Children).WithOptional();
            this.HasMany(o => o.Users).WithOptional(u => u.Organization).HasForeignKey(u => u.OrganizationId);
        }
    }
}
