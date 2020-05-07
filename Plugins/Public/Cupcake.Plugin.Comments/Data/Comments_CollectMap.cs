using Cupcake.Data.Mappings;
using Cupcake.Plugin.Comments.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Comments.Data
{
    public class Comments_CollectMap : PluginBaseEntityMapping<Comments_CollectInfo>
    {
        public Comments_CollectMap()
        {
            this.ToTable("Comments_Collect");
        }
    }
}
