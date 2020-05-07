using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MapMessage.Domain;
using System.Linq;
using Cupcake.Services;
using System.Collections.Generic;

namespace Cupcake.Plugin.MapMessage.Services
{
        public partial class MapMessageService : PluginBaseService<Cupcake.Plugin.MapMessage.Domain.MapMessageInfo>, IMapMessageService
        {
            public MapMessageService(IRepository<Cupcake.Plugin.MapMessage.Domain.MapMessageInfo> repository)
                : base(repository)
            {

            }
            public IPagedList<MapMessageInfo> SearchNews(MapMessageCondition condition)
            {
                var query = repository.Table;

                if (!string.IsNullOrEmpty(condition.Name))
                    query = query.Where(t => t.Name.Contains(condition.Name));
                if (condition.SortModual != null)
            
                    query = query.Where(t => t.SortModual == condition.SortModual);
              

                query = query.Where(t => t.IsDelete == false);
                query = query.OrderByDescending(t => t.CreateDate);

                return new PagedList<MapMessageInfo>(query, condition.PageIndex, condition.PageSize);
            }


        }
   
}
