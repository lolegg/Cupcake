using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.NetWork.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.NetWork.Services
{
    public partial class FormDownloadService : IFormDownloadService
    {
        private readonly IRepository<FormDownload> repository;
        public FormDownloadService(IRepository<FormDownload> repository)
        {
            this.repository = repository;
        }

        public virtual void DeleteGoogleProduct(FormDownload FormDownload)
        {
            if (FormDownload == null)
                throw new ArgumentNullException("FormDownload");

            repository.Delete(FormDownload);
        }

        public virtual void InsertGoogleProductRecord(FormDownload FormDownload)
        {
            if (FormDownload == null)
                throw new ArgumentNullException("FormDownload");

            repository.Insert(FormDownload);
        }

        public virtual void UpdateGoogleProductRecord(FormDownload FormDownload)
        {
            if (FormDownload == null)
                throw new ArgumentNullException("FormDownload");

            repository.Update(FormDownload);
        }

        public IPagedList<FormDownload> SearchFormDownload(FormDownloadCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.WorkType != null)
                query = query.Where(t => t.WorkType == condition.WorkType);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<FormDownload>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual FormDownload GetById(Guid id)
        {
            var query = repository.GetById(id);
            return query;
        }

    }
}
