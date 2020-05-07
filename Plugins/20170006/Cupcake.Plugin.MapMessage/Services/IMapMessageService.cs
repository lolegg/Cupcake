using Cupcake.Core;
using System;
namespace Cupcake.Plugin.MapMessage.Services
{
    public interface IMapMessageService : IpluginBaseService<Cupcake.Plugin.MapMessage.Domain.MapMessageInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MapMessage.Domain.MapMessageInfo> SearchNews(Cupcake.Plugin.MapMessage.Domain.MapMessageCondition condition);
    }
}
