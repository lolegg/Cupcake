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
    public partial class OnlineInterviewQuestionAskService : IOnlineInterviewQuestionAskService
    {
          private readonly IRepository< OnlineInterviewQuestionAsk> repository;
          public OnlineInterviewQuestionAskService(IRepository<OnlineInterviewQuestionAsk> repository)
        {
            this.repository = repository;
        }
          public IPagedList<OnlineInterviewQuestionAsk> SearchAllOnlineInterviewQuestionAsk(OnlineInterviewQuestionAskCondition condition)
        {
            var query = repository.Table;



            if (condition.OnlineInterviewId!=null)
                query = query.Where(t => t.OnlineInterviewId==condition.OnlineInterviewId);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineInterviewQuestionAsk>(query, condition.PageIndex, condition.PageSize);
        }
    

          public void Add(OnlineInterviewQuestionAsk info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public void Update(OnlineInterviewQuestionAsk info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public OnlineInterviewQuestionAsk GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(OnlineInterviewQuestionAsk info)
          {


              repository.Delete(info);
          }

    }
}
