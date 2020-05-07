using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class VariablesMapping : FrameworkBaseEntityMapping<Variables>
    {
        public VariablesMapping()
        {
            this.Property(p => p.Name).IsRequired();
        }
    }
}
