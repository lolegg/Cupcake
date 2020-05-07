using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.SystemMessage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.SystemMessage.Services
{
    public partial class SystemMessageInfoService : ISystemMessageInfoService
    {
          private readonly IRepository< SystemMessageInfo> repository;
          public SystemMessageInfoService(IRepository<SystemMessageInfo> repository)
        {
            this.repository = repository;
        }
          public IPagedList<SystemMessageInfo> SearchAllSystemMessageInfo(SystemMessageInfoCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
          


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<SystemMessageInfo>(query, condition.PageIndex, condition.PageSize);
        }
    

          public void Add(SystemMessageInfo info)
          {
              var query = repository.Table;
              repository.Insert(info);

          }


          public void Update(SystemMessageInfo info)
          {
              var query = repository.Table;
              repository.Update(info);

          }

          public SystemMessageInfo GetById(Guid Id)
          {
              var query = repository.GetById(Id);

              return query;


          }

          public void Delete(SystemMessageInfo info)
          {


              repository.Delete(info);
          }

    }
}
