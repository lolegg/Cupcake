using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Linq;
using Cupcake.Services;
using Cupcake.Plugin.MessageOpen.Helper;
using System;


namespace Cupcake.Plugin.MessageOpen.Services
{
    public partial class MessageOpenApplicationService : PluginBaseService<MessageOpenApplicationInfo>, IMessageOpenApplicationService
    {
        public MessageOpenApplicationService(IRepository<MessageOpenApplicationInfo> repository)
            : base(repository)
        {
         
        }

        public IPagedList<MessageOpenApplicationInfo> SearchNews(MessageOpenApplicationCondition condition)
        {
            var query = repository.Table;
            query = query.Where(t => t.IsDelete == false).OrderByDescending(t=>t.CreateDate);
            return new PagedList<MessageOpenApplicationInfo>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
