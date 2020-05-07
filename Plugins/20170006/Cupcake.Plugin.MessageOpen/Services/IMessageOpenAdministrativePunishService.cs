using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    //行政处罚事项目录
    public interface IMessageOpenAdministrativePunishService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAdministrativePunishInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAdministrativePunishInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenAdministrativePunishCondition condition);
    }
}
