using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MapPoint.Domain;
using Cupcake.Plugin.MapPoint.Services;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using CPMD = Cupcake.Plugin.MapPoint.Domain;

namespace Cupcake.Plugin.MapPoint.Services
{
    public class MapService : PluginBaseService<CPMD.Map>,IMapService
    {
        public MapService(IRepository<CPMD.Map> repository)
            : base(repository)
        {
        }
        public IPagedList<CPMD.Map> Search(MapCondition condition)
        {
            var query = repository.TableNoTracking;
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);
            return new PagedList<CPMD.Map>(query, condition.PageIndex, condition.PageSize);
        }
        public bool AlreadyExists(string name, Guid id)
        {
            return base.AlreadyExists(m => m.Name == name.Trim() && m.Id != id && m.IsDelete == false);
        }
        //public CPMD.Map GetMapImageUrl(string name)
        //{
        //    using (var repository = new Repository<CPMD.Map>())
        //    {
        //        return repository.Get(m => m.Name == name);
        //    }
        //}
    }

    public class MapPointService : PluginBaseService<CPMD.MapPoint>,IMapPointService
    {
        public MapPointService(IRepository<CPMD.MapPoint> repository)
            : base(repository)
        {
        }

        public IList<CPMD.MapPoint> GetByMapId(Guid mapId)
        {
            var query = repository.TableNoTracking;
            query = query.Where(t => t.IsDelete == false);
            query = query.Where(t => t.MapId == mapId);
            return query.ToList();
        }
    }
}
