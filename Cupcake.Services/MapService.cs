using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace Cupcake.Services
{
    public class MapService : BaseService<MapOverlay>
    {
        public MapOverlay GetDefaultMapOverlay()
        {
            var query = new Repository<MapOverlay>().TableNoTracking;
            return query.FirstOrDefault(m => m.Name == "中国");
        }
        //public Guid InitCenter(string name)
        //{
        //    var repository = new Repository<MapCenter>();
        //    var query = repository.TableNoTracking;
        //    var center = query.FirstOrDefault(p => p.Name == name);
        //    if (center != null)
        //    {
        //        return center.Id;
        //    }
        //    else
        //    {
        //        var entity = new MapCenter() { Name = name };
        //        repository.Add(entity);
        //        repository.Commit();
        //        return entity.Id;
        //    }
        //}
        public List<MapOverlayPoints> GetMapOverlayPoints(Guid mapOverlayId)
        {
            var query = new Repository<MapOverlayPoints>().TableNoTracking;
            return query.Where(p => p.MapOverlayId == mapOverlayId).ToList();
        }
        public List<MapOverlayPoints> GetAllMapOverlayPoints()
        {
            return new Repository<MapOverlayPoints>().TableNoTracking.ToList();
        }
        public List<MapOverlayPoints> GetChildrenMapOverlayPoints(Guid mapOverlayId)
        {
            var pQuery = new Repository<MapOverlay>().TableNoTracking;
            var guids = pQuery.Where(p => p.ParentId == mapOverlayId).Select(p => p.Id).ToList();

            var query = new Repository<MapOverlayPoints>().TableNoTracking;
            if (guids.Count > 0)
            {
                return query.Where(p => guids.Contains(p.MapOverlayId)).ToList();
            }
            else
            {
                return query.Where(p => p.MapOverlayId == mapOverlayId).ToList();
            }
        }
        public List<MapOverlayPoints> GetSelfChildrenMapOverlayPoints(Guid mapOverlayId)
        {
            var pQuery = new Repository<MapOverlay>().TableNoTracking;
            var guids = pQuery.Where(p => p.ParentId == mapOverlayId).Select(p => p.Id).ToList();
            guids.Add(mapOverlayId);

            var query = new Repository<MapOverlayPoints>().TableNoTracking;
            if (guids.Count > 0)
            {
                return query.Where(p => guids.Contains(p.MapOverlayId)).ToList();
            }
            else
            {
                return query.Where(p => p.MapOverlayId == mapOverlayId).ToList();
            }
        }
        public bool CheckName(string name, Guid parentId)
        {
            var query = new Repository<MapOverlay>().TableNoTracking;
            if (query.Any(p => p.Name == name && p.ParentId == parentId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //public void CenterBatchAdd(List<MapCenter> entitys)
        //{
        //    var repository = new Repository<MapCenter>();
        //    var query = repository.TableNoTracking;
        //    foreach (var item in entitys)
        //    {
        //        if (query.Any(p => p.Name == item.Name && p.Type == item.Type))
        //        {
        //            repository.BatchDelete(p => p.Name == item.Name && p.Type == item.Type);
        //            repository.Commit();
        //        }
        //        repository.Add(item);
        //    }
        //    repository.Commit();
        //}
        public void BatchAdd(List<MapOverlayPoints> entitys)
        {
            var repository = new Repository<MapOverlayPoints>();
            var query = repository.TableNoTracking;
            foreach (var item in entitys)
            {
                if (query.Any(p => p.MapOverlayId == item.MapOverlayId))
                {
                    repository.BatchDelete(p => p.MapOverlayId == item.MapOverlayId);
                    repository.Commit();
                }
                repository.Add(item);
            }
            repository.Commit();
        }
        public List<MapOverlay> GetMapOverlays(Guid? parentId)
        {
            var query = new Repository<MapOverlay>().TableNoTracking;
            if (parentId == null)
            {
                return query.ToList();
            }
            else
            {
                return query.Where(m => m.ParentId == parentId).ToList();
            }
        }

        public IEnumerable GetParentMapOverlays()
        {
            string sql = "WITH temp(Id,ParentId,Name,[Level],haschild)" +
                        " as (" +
                        " select Id,ParentId,Name,[Level],1 as haschild from [MapOverlays]" +
                        " where Name = '中国' and ParentId is null" +
                        " union all" +
                        " select a.Id,a.ParentId,a.Name,a.[Level],haschild = (case when exists(select 1 from [MapOverlays] where [MapOverlays].ParentId=a.id) then 1 else 0 end) from [MapOverlays] a" +
                        " inner join" +
                        " temp b" +
                        " on ( a.ParentId=b.Id) " +
                        " ) select * from temp where haschild=1";

            return new Repository<IEnumerable>().DynamicSqlQuery(sql, null);
        }

        public void DeleteMapOverlayPoints(Guid mapOverlayId)
        {
            using (var repository = new Repository<MapOverlayPoints>())
            {
                string sql = "delete  [MapOverlayPoints]  where [MapOverlayId]=@roleid";

                SqlParameter[] parms = new SqlParameter[]{
                                new SqlParameter("@roleid",mapOverlayId),
                            };
                repository.ExecuteSql(sql, parms);
            }
        }
        public bool HasChildren(Guid id)
        {
            var query = new Repository<MapOverlay>().TableNoTracking;
            return query.Any(p => p.ParentId == id);
        }

        //public MapCenter GetMapCenterDefault(Guid Id)
        //{
        //    var query = new Repository<MapCenter>().TableNoTracking;


        //    var mapcenter = query.Where(m => m.BaseId == Id).FirstOrDefault();

        //    if (mapcenter == null)
        //    {
        //        mapcenter = query.Where(m => m.ParentId == null).FirstOrDefault();
        //    }

        //    return mapcenter;


        //}

        //public List<MapOverlay> GetChildListMap(Guid parentId)
        //{

        //    var query = new Repository<MapOverlay>().TableNoTracking;
        //    var querymapcenter = new Repository<MapCenter>().TableNoTracking.Where(m => m.ParentId == parentId).ToList();
        //    return query.ToList().Join(querymapcenter, ov => ov.MapCenterId, mc => mc.BaseId, (ov, mc) => ov).OrderBy(m => m.Name).ToList();



        //}
    }
}
