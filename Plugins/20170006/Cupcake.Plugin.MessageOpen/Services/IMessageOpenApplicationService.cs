using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    public interface IMessageOpenApplicationService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenApplicationInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenApplicationInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenApplicationCondition condition);
    }
}
