using Cupcake.Data.Mappings;
using Cupcake.Plugin.Template.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Template.Data
{
    public partial class NewsMap : PluginBaseEntityMapping<News>
    {
        public NewsMap()
        {
            this.ToTable("Template_News");
        }
    }
}