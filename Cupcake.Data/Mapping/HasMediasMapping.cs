using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class HasMediasMapping : FrameworkBaseEntityMapping<HasMedias>
    {
        public HasMediasMapping()
        {
            this.ToTable("HasMedias");
        }
    }
}
