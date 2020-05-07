using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using System.Linq;

namespace Cupcake.Services
{
    public class LogService : BaseService<Log>
    {
        public IPagedList<Log> SearchLogs(LogCondition condition)
        {
            var query = new Repository<Log>().Table;

            if (!string.IsNullOrEmpty(condition.Message))
                query = query.Where(t => t.Message.Contains(condition.Message));
            if (condition.LogType != null)
                query = query.Where(t => t.Level == condition.LogType.ToString().ToUpper());
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Log>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
