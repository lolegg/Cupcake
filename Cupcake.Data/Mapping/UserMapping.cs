using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class UserMapping : FrameworkBaseEntityMapping<User>
    {
        public UserMapping()
        {
            this.ToTable("Users");
            this.Property(p => p.UserName).IsRequired();
            this.Property(p => p.Password).IsRequired();
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.UserType).IsRequired();
            this.Property(p => p.Status).IsRequired();

            this.HasMany(u => u.Roles).WithMany(r => r.Users)
                .Map(
                m =>
                {
                    m.ToTable("UserRoles");    
                    m.MapLeftKey("UserId");  
                    m.MapRightKey("RoleId"); 
                });
        }
    }
}
