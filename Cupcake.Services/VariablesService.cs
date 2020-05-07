using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Linq;

namespace Cupcake.Services
{
    public class VariablesService : BaseService<Variables>
    {
        public IPagedList<Variables> SearchVariables(VariablesCondition condition)
        {
            var query = new Repository<Variables>().Table;

            if (!string.IsNullOrEmpty(condition.Name))
                query = query.Where(t => t.Name.Contains(condition.Name));
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Variables>(query, condition.PageIndex, condition.PageSize);
        }

        public bool AlreadyExists(string name, Guid id)
        {
            return base.AlreadyExists(m => m.Name == name.Trim() && m.Id != id && m.IsDelete == false);
        }
    }
}
