using Cupcake.Data.Mappings;
using Cupcake.Plugin.Comments.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.Comments.Data
{
    public class Comments_PraiseMap : PluginBaseEntityMapping<Comments_PraiseInfo>
    {
      public Comments_PraiseMap()
        {
            this.ToTable("Comments_Praise");
        }
    }
}
