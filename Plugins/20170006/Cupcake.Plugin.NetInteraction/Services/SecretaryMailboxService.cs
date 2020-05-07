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
    public partial  class SecretaryMailboxService : ISecretaryMailboxService
    {
          private readonly IRepository< SecretaryMailbox> repository;
          public SecretaryMailboxService(IRepository<SecretaryMailbox> repository)
        {
            this.repository = repository;
        }
          public IPagedList<SecretaryMailbox> SearchAllSecretaryMailbox(SecretaryMailboxCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<SecretaryMailbox>(query, condition.PageIndex, condition.PageSize);
        }
    

          public void Add(SecretaryMailbox info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public List<SecretaryMailbox> GetserialNumber()
          {
              return repository.Table.OrderByDescending(t => t.CreateDate).ToList();
            
          }


          public void Update(SecretaryMailbox info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public SecretaryMailbox GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(SecretaryMailbox info)
          {


              repository.Delete(info);
          }

    }
}
