using System;
namespace Cupcake.Plugin.Template.Services
{
    public interface INewsService
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.Template.Domain.News> SearchNews(Cupcake.Plugin.Template.Domain.NewsCondition condition);
    }
}
