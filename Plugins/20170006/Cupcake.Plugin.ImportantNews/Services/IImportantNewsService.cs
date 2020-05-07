using System;
namespace Cupcake.Plugin.ImportantNews.Services
{
    public interface IImportantNewsService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.ImportantNews.Domain.ImportantNewsInfo> SearchNews(Cupcake.Plugin.ImportantNews.Domain.NewsCondition condition);
        void Add(Cupcake.Plugin.ImportantNews.Domain.ImportantNewsInfo info);
        void Update(Cupcake.Plugin.ImportantNews.Domain.ImportantNewsInfo info);
        void Delete(Cupcake.Plugin.ImportantNews.Domain.ImportantNewsInfo info);
        Cupcake.Plugin.ImportantNews.Domain.ImportantNewsInfo GetById(Guid Id);
    }
}
