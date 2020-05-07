
using Cupcake.Core.Domain;
namespace Cupcake.Data.Mappings
{
    public class SysSetFormMapping : FrameworkBaseEntityMapping<SysSetForm>
    {
        public SysSetFormMapping()
        {
            this.ToTable("SysSetForm");
        }
    }
}
