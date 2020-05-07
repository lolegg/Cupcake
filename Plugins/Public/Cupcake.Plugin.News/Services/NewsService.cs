using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.News.Domain;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Cupcake.Core.Domain;
using System.Linq.Expressions;
using Cupcake.Plugin.News.Models;

namespace Cupcake.Plugin.News.Services
{
    public partial class NewsService : INewsService
    {
        private readonly IRepository<NewsInfo> repository;
        public NewsService(IRepository<NewsInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<NewsInfo> SearchNews(NewsCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.NewsType != null)
                query = query.Where(t => t.NewsType == condition.NewsType);
            if (condition.BeginDate != null)
                query = query.Where(t => t.CreateDate > condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(t => t.CreateDate < condition.EndDate);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<NewsInfo>(query, condition.PageIndex, condition.PageSize);
        }

        public virtual void DeleteGoogleProduct(NewsInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");
            repository.Delete(googleProductRecord);
        }

        public virtual IList<NewsInfo> GetAll()
        {
            var query = from gp in repository.Table
                        orderby gp.Id
                        select gp;
            var records = query.ToList();
            return records;
        }

        public virtual NewsInfo GetById(Guid googleProductRecordId)
        {
            if (googleProductRecordId == null)
                return null;

            return repository.GetById(googleProductRecordId);
        }
        public virtual void InsertGoogleProductRecord(NewsInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Insert(googleProductRecord);
        }

        public virtual void UpdateGoogleProductRecord(NewsInfo googleProductRecord)
        {
            if (googleProductRecord == null)
                throw new ArgumentNullException("googleProductRecord");

            repository.Update(googleProductRecord);
        }
    }
}
