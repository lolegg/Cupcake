using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    public interface IMessageOpenMechanismService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenMechanismInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenMechanismInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenMechanismCondition condition);
        int GetMessageOpenMechanismCount();
    }
}
