using Cupcake.Data.Mappings;
using Cupcake.Plugin.MembersForWeb.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MembersForWeb.Data
{
    public partial class MembersMap : PluginBaseEntityMapping<Members>
    {
        public MembersMap()
        {
            this.ToTable("Members_Members");
        }
    }
}