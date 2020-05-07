using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class DataDictionaryMapping : FrameworkBaseEntityMapping<DataDictionary>
    {
        public DataDictionaryMapping()
        {
            this.ToTable("DataDictionary");
            this.Property(m => m.Name).IsRequired();
        }
    }
}
