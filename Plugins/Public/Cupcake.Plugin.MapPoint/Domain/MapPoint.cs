using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;

namespace Cupcake.Plugin.MapPoint.Domain
{
    public class Map : PluginBaseEntity
    {
        public string Name { get; set; }
        public virtual IList<MapPoint> MapPoints { get; set; }
    }
    public class MapCondition : Condition
    {
        public string Name { get; set; }  
    }

    public class MapPoint : PluginBaseEntity
    {
        public string Name { get; set; }
        public string InfoWindow { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Guid MapId { get; set; }
        public virtual Map Map { get; set; }
    }
}
