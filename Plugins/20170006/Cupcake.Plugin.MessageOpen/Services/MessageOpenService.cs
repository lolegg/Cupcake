using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Linq;
using Cupcake.Services;
using System;
using Cupcake.Plugin.MessageOpen.Helper;

namespace Cupcake.Plugin.MessageOpen.Services
{
    public partial class MessageOpenService : PluginBaseService<MessageOpenInfo>,IMessageOpenService
    {
        public MessageOpenService(IRepository<MessageOpenInfo> repository)
            : base(repository)
        {
         
        }

         public IPagedList<MessageOpenInfo> SearchNews(MessageOpenCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.Title != null)
                query = query.Where(t => t.Title == condition.Title);
            if (condition.MessageOpenChoose != null)
                query = query.Where(t => t.MessageOpenChoose == condition.MessageOpenChoose);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<MessageOpenInfo>(query, condition.PageIndex, condition.PageSize);
        }
         public int GetAddMessageOpenChooseCount(Guid? id) {
             var query = repository.Table;
             int count = 0;
             if(id!=null){
                count = query.Where(m => m.MessageOpenChoose == id && m.IsDelete==false).Count();
             }
             return count;
         }

         public int GetEditMessageOpenChooseCount(Guid id, Guid? MessageOpenChooseId)
         {
             var query = repository.Table;
             int count = 0;
             if (id != null)
             {
                 count = query.Where(m => m.Id != id && m.IsDelete == false && m.MessageOpenChoose == MessageOpenChooseId).Count();
             }
             return count;
         }

      
    }
}
