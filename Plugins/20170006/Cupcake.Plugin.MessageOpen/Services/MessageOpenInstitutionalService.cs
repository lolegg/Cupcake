using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Linq;
using Cupcake.Services;
using System;

namespace Cupcake.Plugin.MessageOpen.Services
{
    public partial class MessageOpenInstitutionalService : PluginBaseService<MessageOpenInstitutionalInfo>, IMessageOpenInstitutionalService
    {
        public MessageOpenInstitutionalService(IRepository<MessageOpenInstitutionalInfo> repository)
            : base(repository)
        {
         
        }

        public IPagedList<MessageOpenInstitutionalInfo> SearchNews(MessageOpenInstitutionalCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.Institutional != null)
                query = query.Where(t => t.Institutional == condition.Institutional);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<MessageOpenInstitutionalInfo>(query, condition.PageIndex, condition.PageSize);
        }
        public int GetAddMessageOpenInstitutionalCount(Guid? id)
        {
            var query = repository.Table;
            int count = 0;
            if (id != null)
            {
                count = query.Where(m => m.Institutional == id && m.IsDelete == false).Count();
            }
            return count;
        }

        public int GetEditMessageOpenInstitutionalCount(Guid id, Guid? InstitutionalId)
        {
            var query = repository.Table;
            int count = 0;
            if (id != null)
            {
                count = query.Where(m => m.Id != id && m.IsDelete == false && m.Institutional == InstitutionalId).Count();
            }
            return count;
        }

    }
}
