using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.NetInteraction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Services
{
    public partial class OnlineCommentsService : IOnlineCommentsService 
    {
          private readonly IRepository< OnlineComments> repository;
          public OnlineCommentsService(IRepository<OnlineComments> repository)
        {
            this.repository = repository;
        }
          public IPagedList<OnlineComments> SearchAllOnlineComments(OnlineCommentsCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineComments>(query, condition.PageIndex, condition.PageSize);
        }
    

          public void Add(OnlineComments info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public void Update(OnlineComments info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public OnlineComments GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(OnlineComments info)
          {


              repository.Delete(info);
          }

    }
}
