using Cupcake.Data.Mappings;
using Cupcake.Plugin.MapPoint.Domain;

namespace Cupcake.Plugin.MapPoint.Data
{
    public class MapMapping : PluginBaseEntityMapping<Map>
    {
        public MapMapping()
        {
            this.ToTable("MapPoint_Map");
            this.HasMany(p => p.MapPoints).WithRequired(s => s.Map).HasForeignKey(p => p.MapId);
        }
    }
    public class MapPointMapping : PluginBaseEntityMapping<Cupcake.Plugin.MapPoint.Domain.MapPoint>
    {
        public MapPointMapping()
        {
            this.ToTable("MapPoint_MapPoint");
        }
    }
}
