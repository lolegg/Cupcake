using System;
namespace Cupcake.Plugin.News.Services
{
    public interface INewsService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.News.Domain.NewsInfo> SearchNews(Cupcake.Plugin.News.Domain.NewsCondition condition);
        void DeleteGoogleProduct(Cupcake.Plugin.News.Domain.NewsInfo googleProductRecord);
        System.Collections.Generic.IList<Cupcake.Plugin.News.Domain.NewsInfo> GetAll();
        Cupcake.Plugin.News.Domain.NewsInfo GetById(Guid googleProductRecordId);
        void InsertGoogleProductRecord(Cupcake.Plugin.News.Domain.NewsInfo googleProductRecord);
        void UpdateGoogleProductRecord(Cupcake.Plugin.News.Domain.NewsInfo googleProductRecord);
    }
}
