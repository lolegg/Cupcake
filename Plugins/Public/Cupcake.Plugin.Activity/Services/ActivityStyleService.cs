using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Activity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Services
{
    public partial class ActivityStyleService : IActivityStyleService
    {
          private readonly IRepository<ActivityStyle> repository;
          public ActivityStyleService(IRepository<ActivityStyle> repository)
        {
            this.repository = repository;
        }

          public virtual void DeleteGoogleProduct(ActivityStyle ActivityStyle)
        {
            if (ActivityStyle == null)
                throw new ArgumentNullException("ActivityStyle");

            repository.Delete(ActivityStyle);
        }

          public virtual void InsertGoogleProductRecord(ActivityStyle ActivityStyle)
        {
            if (ActivityStyle == null)
                throw new ArgumentNullException("ActivityStyle");

            repository.Insert(ActivityStyle);
        }

          public virtual void UpdateGoogleProductRecord(ActivityStyle ActivityStyle)
        {
            if (ActivityStyle == null)
                throw new ArgumentNullException("ActivityStyle");

            repository.Update(ActivityStyle);
        }

          public IPagedList<ActivityStyle> SearchActivityStyle(ActivityStyleCondition condition)
          {
              var query = repository.Table;
              if (!string.IsNullOrEmpty(condition.Title)) 
                  query = query.Where(t => t.Title.Contains(condition.Title));
              query = query.Where(t => t.IsDelete == false);
              query = query.OrderByDescending(t => t.CreateDate);

              return new PagedList<ActivityStyle>(query, condition.PageIndex, condition.PageSize);
          }
          public virtual ActivityStyle GetById(Guid id)
          {
              var query = repository.GetById(id);
              return query;
          }
    }
}
