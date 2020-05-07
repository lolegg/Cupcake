using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class MapModel : BaseModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Color { get; set; }
        public string CenterId { get; set; }

        public Guid ChildrenId { get; set; }

        public Guid ParentId { get; set; }

        public string lng { get; set; }
        public string lat { get; set; }
        public string Coordinate { get; set; }

        public string CenterParentId { get; set; }

        public string newCenterLng { get; set; }
        public string newCenterLat { get; set; }

        public List<MapOverlayPointsModel> listMap { get; set; }
    }
    public class MapOverlayPointsModel : BaseModel
    {
        public Guid MapOverlayId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}