using System;
using Cupcake.Core;
namespace Cupcake.Plugin.MessageOpen.Services
{
    //通知公告
    public interface IMessageOpenAnnouncementsService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAnnouncementsInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAnnouncementsInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenAnnouncementsCondition condition);
    }
}
