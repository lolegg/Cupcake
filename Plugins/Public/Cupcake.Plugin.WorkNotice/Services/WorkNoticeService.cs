using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using Cupcake.Plugin.WorkNotice.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cupcake.Plugin.WorkNotice.Services
{
    public partial class WorkNoticeService : IWorkNoticeService
    {
        private readonly IRepository<WorkNotices> repository;
        public WorkNoticeService(IRepository<WorkNotices> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(WorkNotices WorkNotices)
        {
            if (WorkNotices == null)
                throw new ArgumentNullException("WorkNotices");

            repository.Delete(WorkNotices);
        }

        public virtual void InsertGoogleProductRecord(WorkNotices WorkNotices)
        {
            if (WorkNotices == null)
                throw new ArgumentNullException("WorkNotices");

            repository.Insert(WorkNotices);
        }

        public virtual void UpdateGoogleProductRecord(WorkNotices WorkNotices)
        {
            if (WorkNotices == null)
                throw new ArgumentNullException("WorkNotices");

            repository.Update(WorkNotices);
        }


        public IPagedList<WorkNotices> SearchWorkNotices(WorkNoticeCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<WorkNotices>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual WorkNotices GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }

      
       
    }
}
