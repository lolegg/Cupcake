using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Activity.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.Activity.Services
{
    public partial class ActivityService : IActivityService
    {
        private readonly IRepository<Activitys> repository;
        public ActivityService(IRepository<Activitys> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(Activitys Activitys)
        {
            if (Activitys == null)
                throw new ArgumentNullException("Activitys");

            repository.Delete(Activitys);
        }

        public virtual void InsertGoogleProductRecord(Activitys Activitys)
        {
            if (Activitys == null)
                throw new ArgumentNullException("Activitys");

            repository.Insert(Activitys);
        }

        public virtual void UpdateGoogleProductRecord(Activitys Activitys)
        {
            if (Activitys == null)
                throw new ArgumentNullException("Activitys");

            repository.Update(Activitys);
        }

        public IPagedList<Activitys> SearchActivity(ActivityCondition condition)
        {
            var query = repository.Table;
            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Activitys>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual Activitys GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }
    }
}
