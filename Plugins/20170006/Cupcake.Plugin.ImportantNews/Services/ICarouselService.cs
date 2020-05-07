using System;
namespace Cupcake.Plugin.ImportantNews.Services
{
    public interface ICarouselService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.ImportantNews.Domain.CarouselInfo> SearchNews(Cupcake.Plugin.ImportantNews.Domain.CarouselCondition condition);
        void Add(Cupcake.Plugin.ImportantNews.Domain.CarouselInfo info);
        void Update(Cupcake.Plugin.ImportantNews.Domain.CarouselInfo info);
        void Delete(Cupcake.Plugin.ImportantNews.Domain.CarouselInfo info);
        Cupcake.Plugin.ImportantNews.Domain.CarouselInfo GetById(Guid Id);
    }
}
