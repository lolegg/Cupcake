using Cupcake.Core;
using Cupcake.Plugin.MapPoint.Domain;
using System;
using System.Collections.Generic;
using CPMD = Cupcake.Plugin.MapPoint.Domain;

namespace Cupcake.Plugin.MapPoint.Services
{
    public interface IMapService : IpluginBaseService<Map>
    {
        IPagedList<Map> Search(MapCondition condition);
        bool AlreadyExists(string name, Guid id);
    }

    public interface IMapPointService : IpluginBaseService<CPMD.MapPoint>
    {
        IList<CPMD.MapPoint> GetByMapId(Guid mapId);
    }
}
