using System;
using Cupcake.Core;
namespace Cupcake.Plugin.MessageOpen.Services
{
    //近期信息公开
    public interface IMessageOpenRecentlyService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenRecentlyInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenRecentlyInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenRecentlyCondition condition);
    }
}
