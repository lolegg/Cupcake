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
  public partial  class OnlineInterviewService:IOnlineInterviewService
    {
          private readonly IRepository< OnlineInterview> repository;
          public OnlineInterviewService(IRepository<OnlineInterview> repository)
        {
            this.repository = repository;
        }
          public IPagedList<OnlineInterview> SearchAllOnlineInterview(OnlineInterviewCondition condition)
        {
            var query = repository.Table;

         
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineInterview>(query, condition.PageIndex, condition.PageSize);
        }
    

          public void Add(OnlineInterview info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public void Update(OnlineInterview info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public OnlineInterview GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(OnlineInterview info)
          {


              repository.Delete(info);
          }

    }
}
