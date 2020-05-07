using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.NetWork.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.NetWork.Services
{
    public partial class WorkGuideService : IWorkGuideService
    {
        private readonly IRepository<WorkGuide> repository;
        public WorkGuideService(IRepository<WorkGuide> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(WorkGuide WorkGuide)
        {
            if (WorkGuide == null)
                throw new ArgumentNullException("WorkGuide");

            repository.Delete(WorkGuide);
        }

        public virtual void InsertGoogleProductRecord(WorkGuide WorkGuide)
        {
            if (WorkGuide == null)
                throw new ArgumentNullException("WorkGuide");

            repository.Insert(WorkGuide);
        }

        public virtual void UpdateGoogleProductRecord(WorkGuide WorkGuide)
        {
            if (WorkGuide == null)
                throw new ArgumentNullException("WorkGuide");

            repository.Update(WorkGuide);
        }

        public IPagedList<WorkGuide> SearchWorkGuide(WorkGuideCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.WorkType != null)
                query = query.Where(t => t.WorkType == condition.WorkType);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<WorkGuide>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual WorkGuide GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }
    }
}
