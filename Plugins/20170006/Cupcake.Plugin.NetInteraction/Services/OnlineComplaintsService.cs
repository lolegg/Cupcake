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
    public partial class OnlineComplaintsService : IOnlineComplaintsService
    {
          private readonly IRepository< OnlineComplaints> repository;
          public OnlineComplaintsService(IRepository<OnlineComplaints> repository)
        {
            this.repository = repository;
        }
          public IPagedList<OnlineComplaints> SearchAllOnlineComplaints(OnlineComplaintsCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineComplaints>(query, condition.PageIndex, condition.PageSize);
        }

          public List<OnlineComplaints> GetserialNumber()
          {
              return repository.Table.OrderByDescending(t => t.CreateDate).ToList();

          }
          public void Add(OnlineComplaints info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public void Update(OnlineComplaints info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public OnlineComplaints GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(OnlineComplaints info)
          {


              repository.Delete(info);
          }

    }
}
