using Cupcake.Data.Mappings;
using Cupcake.Plugin.Comments.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Comments.Data
{
    public partial class CommentsMap : PluginBaseEntityMapping<CommentsInfo>
    {
        public CommentsMap()
        {
            this.ToTable("Comments");
        }
    }
}