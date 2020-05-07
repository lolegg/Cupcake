using Cupcake.Data.Mappings;
using Cupcake.Plugin.ImportantNews.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.ImportantNews.Data
{
    public partial class ImportantNewsMap : PluginBaseEntityMapping<ImportantNewsInfo>
    {
        public ImportantNewsMap()
        {
            this.ToTable("ImportantNews_News");
        }
    }
}