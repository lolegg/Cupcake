using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class MapOverlay : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Level { get; set; }
        public string Color { get; set; }
    }
    public class MapOverlayPoints : FrameworkBaseEntity
    {
        public Guid MapOverlayId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
