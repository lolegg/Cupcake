using AutoMapper;
using Cupcake.Plugin.MapPoint.Domain;
using Cupcake.Plugin.MapPoint.Models;
using Cupcake.Services;

namespace Cupcake.Plugin.MapPoint
{
    public static class MappingExtensions
    {
        public static MapModel ToModel(this Map entity)
        {
            var model = Mapper.Map<Map, MapModel>(entity);
            model.ImageUrls = MediaHelper.GetHasMediasThumbnailPath(model.Id);
            return model;
        }
        public static Map ToEntity(this MapModel model)
        {
            return Mapper.Map<MapModel, Map>(model);
        }
    }
}
