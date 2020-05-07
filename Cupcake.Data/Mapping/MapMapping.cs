using Cupcake.Core.Domain;

namespace Cupcake.Data.Mappings
{
    public class MapOverlayMapping : FrameworkBaseEntityMapping<MapOverlay>
    {
        public MapOverlayMapping()
        {
            this.Property(p => p.Name).IsRequired();
        }
    }
    public class MapOverlayPointsMapping : FrameworkBaseEntityMapping<MapOverlayPoints>
    {
        public MapOverlayPointsMapping()
        {
            this.Property(p => p.MapOverlayId).IsRequired();
            this.Property(p => p.Longitude).IsRequired();
            this.Property(p => p.Latitude).IsRequired();
        }
    }
}
