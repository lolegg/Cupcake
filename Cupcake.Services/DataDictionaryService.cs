using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections;
using System.Linq;

namespace Cupcake.Services
{
    public class DataDictionaryService : BaseService<DataDictionary>
    {
        public IEnumerable GetAllDataDictionarysForTree()
        {
            string sql = "SELECT d.ChildrenNumber,c.* FROM DataDictionary c LEFT JOIN " +
                         "(SELECT a.Id, COUNT(b.ParentId) ChildrenNumber FROM DataDictionary a LEFT JOIN DataDictionary b on a.Id = b.ParentId GROUP BY a.Id) as d " +
                         "ON c.Id = d.Id WHERE c.IsDelete = 0";

            return new Repository<IEnumerable>().DynamicSqlQuery(sql, null);
        }

        public bool AlreadyExists(string name, Guid parentId, Guid id)
        {
            return base.AlreadyExists(m => m.Name == name.Trim() && m.ParentId == parentId && m.Id != id && m.IsDelete == false);
        }

        public IPagedList<DataDictionary> SearchDataDictionarys(DataDictionaryCondition condition)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;

            if (!string.IsNullOrEmpty(condition.Name))
                query = query.Where(t => t.Name.Contains(condition.Name));
            query = query.Where(t => t.ParentId == condition.NodeId);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<DataDictionary>(query, condition.PageIndex, condition.PageSize);
        }

        public bool HasChildren(Guid id)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            return query.Any(p => p.ParentId == id);
        }
        public DataDictionary GetRoot()
        {
            var query = new Repository<DataDictionary>().TableNoTracking;

            query = query.Where(t => t.Name == "Root");
            query = query.Where(t => t.ParentId == null);
            query = query.Where(t => t.IsDelete == false);
            return query.FirstOrDefault();
        }
    }
}
