using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.ImportantNews.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.ImportantNews.Services
{
    public partial class ImportantNewsService : IImportantNewsService
    {
        private readonly IRepository<ImportantNewsInfo> repository;
        public ImportantNewsService(IRepository<ImportantNewsInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<ImportantNewsInfo> SearchNews(NewsCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.NewsType != null)
                query = query.Where(t => t.NewsType == condition.NewsType);
            if (condition.BeginDate != null)
                query = query.Where(t => t.ReleaseTime > condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(t => t.ReleaseTime < condition.EndDate);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.ReleaseTime);

            return new PagedList<ImportantNewsInfo>(query, condition.PageIndex, condition.PageSize);
        }
        public void Add(ImportantNewsInfo info)
        {
            var query = repository.Table;
            repository.Insert(info);
        }
        public void Update(ImportantNewsInfo info)
        {
            var query = repository.Table;
            repository.Update(info);
        }
        public void Delete(ImportantNewsInfo info)
        {
            repository.Delete(info);
        }
        public ImportantNewsInfo GetById(Guid Id)
        {
            var query = repository.GetById(Id);
            return query;
        }

    }
}
