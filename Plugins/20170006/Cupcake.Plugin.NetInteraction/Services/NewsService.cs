using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.Template.Domain;
using System.Linq;

namespace Cupcake.Plugin.Template.Services
{
    public partial class NewsService : INewsService
    {
        private readonly IRepository<News> repository;
        public NewsService(IRepository<News> repository)
        {
            this.repository = repository;
        }

        public IPagedList<News> SearchNews(NewsCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.NewsType != null)
                query = query.Where(t => t.NewsType == condition.NewsType);

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<News>(query, condition.PageIndex, condition.PageSize);
        }
    }
}
