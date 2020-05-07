using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Linq;
using Cupcake.Services;
using System;

namespace Cupcake.Plugin.MessageOpen.Services
{
   public partial class MessageOpenMechanismService: PluginBaseService<MessageOpenMechanismInfo>, IMessageOpenMechanismService
    {
       public MessageOpenMechanismService(IRepository<MessageOpenMechanismInfo> repository)
            : base(repository)
        {
         
        }

        public IPagedList<MessageOpenMechanismInfo> SearchNews(MessageOpenMechanismCondition condition)
        {
            var query = repository.Table;

            if (condition.Id!=null)
                query = query.Where(t => t.Id==condition.Id);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<MessageOpenMechanismInfo>(query, condition.PageIndex, condition.PageSize);
        }
        public int GetMessageOpenMechanismCount()
        {
            var query = repository.Table;
            return query.Where(m => m.IsDelete == false).Count();
        }
    }
}
