using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.ImportantNews.Domain;
using System;
using System.Linq;

namespace Cupcake.Plugin.ImportantNews.Services
{
    public partial class CarouselService : ICarouselService
    {
        private readonly IRepository<CarouselInfo> repository;
        public CarouselService(IRepository<CarouselInfo> repository)
        {
            this.repository = repository;
        }

        public IPagedList<CarouselInfo> SearchNews(CarouselCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
           
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<CarouselInfo>(query, condition.PageIndex, condition.PageSize);
        }
        public void Add(CarouselInfo info)
        {
            var query = repository.Table;
            repository.Insert(info);
        }
        public void Update(CarouselInfo info)
        {
            var query = repository.Table;
            repository.Update(info);
        }
        public void Delete(CarouselInfo info)
        {
            repository.Delete(info);
        }
        public CarouselInfo GetById(Guid Id)
        {
            var query = repository.GetById(Id);
            return query;
        }

    }
}
