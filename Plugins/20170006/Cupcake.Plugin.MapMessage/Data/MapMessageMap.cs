using Cupcake.Data.Mappings;
using Cupcake.Plugin.MapMessage.Domain;
using Cupcake.Data;
using Cupcake.Web.Framework;

namespace Cupcake.Plugin.MapMessage.Data
{
    public partial class MapMessageMap : PluginBaseEntityMapping<MapMessageInfo>
    {
        public MapMessageMap()
        {
            this.ToTable("MapMessage");
        }
    }
}