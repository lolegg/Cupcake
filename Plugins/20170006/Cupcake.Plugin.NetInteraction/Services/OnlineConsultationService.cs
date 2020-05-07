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
    public partial class OnlineConsultationService : IOnlineConsultationService
    {
          private readonly IRepository< OnlineConsultation> repository;
          public OnlineConsultationService(IRepository<OnlineConsultation> repository)
        {
            this.repository = repository;
        }
          public IPagedList<OnlineConsultation> SearchAllOnlineConsultation(OnlineConsultationCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<OnlineConsultation>(query, condition.PageIndex, condition.PageSize);
        }
    

          public void Add(OnlineConsultation info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }

          public List<OnlineConsultation> GetserialNumber()
          {
              return repository.Table.OrderByDescending(t => t.CreateDate).ToList();

          }
          public void Update(OnlineConsultation info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public OnlineConsultation GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(OnlineConsultation info)
          {


              repository.Delete(info);
          }

    }
}
