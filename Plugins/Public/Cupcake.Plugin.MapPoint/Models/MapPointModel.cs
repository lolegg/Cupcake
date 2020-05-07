using AutoMapper;
using Cupcake.Plugin.MapPoint.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CPMD = Cupcake.Plugin.MapPoint.Domain;

namespace Cupcake.Plugin.MapPoint.Models
{
    public class MapModel : BaseModel
    {
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "图片")]
        public List<Guid> ImageIds { get; set; }
        public List<string> ImageUrls { get; set; }
        public string ImageIdsHtml { get; set; }
    }
    public class MapPointModel:BaseModel
    {
        public string Name { get; set; }
        public string InfoWindow { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class MapPoint2Model : BaseModel
    {
        public MapPoint2Model()
        {

            this.MapCoordinateListModel = new List<CPMD.MapPoint>();
        }
        [Required]
        [Display(Name = "名称")]
        public string name { get; set; }
        [Required]
        [Display(Name = "地图")]
        public string ImageUrl { get; set; }
        public string Image { get; set; }
        public List<CPMD.MapPoint> MapCoordinateListModel { get; set; }
        public CPMD.MapPoint ToInfo()
        {
            return Mapper.Map<CPMD.MapPoint>(this);
        }
    }
    public class MapInformationListModel : ListModel<MapPoint2Model>
    {
        public MapInformationListModel(IList<CPMD.MapPoint> ListModel)
        {
            List = new List<MapPoint2Model>();
            foreach (var dp in ListModel)
            {
                MapPoint2Model MModel = Mapper.Map<MapPoint2Model>(dp);
                List.Add(MModel);
            }
        }

    }

    public class MapCoordinateModel : BaseModel
    {
        public string MapId { get; set; }
        public string CoordinateName { get; set; }
        public string CoordinateAddress { get; set; }
        public string CoordinateImageUrl { get; set; }
        public string CoordinateX { get; set; }
        public string CoordinateY { get; set; }
        public string n { get; set; }
        public CPMD.MapPoint ToInfo()
        {
            return Mapper.Map<CPMD.MapPoint>(this);
        }
    }
    public class MapCoordinateListModel : ListModel<MapCoordinateModel>
    {
        public MapCoordinateListModel(IList<CPMD.MapPoint> ListModel)
        {
            List = new List<MapCoordinateModel>();
            foreach (var dp in ListModel)
            {
                MapCoordinateModel MapModel = Mapper.Map<MapCoordinateModel>(dp);
                List.Add(MapModel);
            }
        }

    }
}