using Cupcake.Data.Mappings;
using Cupcake.Plugin.News.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.News.Data
{
    public partial class NewsMap : PluginBaseEntityMapping<NewsInfo>
    {
        public NewsMap()
        {
            this.ToTable("News_News");
        }
    }
}