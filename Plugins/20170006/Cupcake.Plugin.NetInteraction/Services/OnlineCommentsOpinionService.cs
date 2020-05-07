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
   public partial class OnlineCommentsOpinionService : IOnlineCommentsOpinionService 
    {
          private readonly IRepository< OnlineCommentsOpinion> repository;
          public OnlineCommentsOpinionService(IRepository<OnlineCommentsOpinion> repository)
        {
            this.repository = repository;
        }
          public IPagedList<OnlineCommentsOpinion> SearchAllOnlineCommentsOpinion(OnlineCommentsOpinionCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Content))
                query = query.Where(t => t.Content.Contains(condition.Content));
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineCommentsOpinion>(query, condition.PageIndex, condition.PageSize);
        }


          public void Add(OnlineCommentsOpinion info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public void Update(OnlineCommentsOpinion info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public OnlineCommentsOpinion GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(OnlineCommentsOpinion info)
          {


              repository.Delete(info);
          }

    }
}
