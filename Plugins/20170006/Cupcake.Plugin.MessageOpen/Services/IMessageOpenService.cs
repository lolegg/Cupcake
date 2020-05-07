using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    public interface IMessageOpenService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenCondition condition);

       int GetAddMessageOpenChooseCount(Guid? id);
       int GetEditMessageOpenChooseCount(Guid id, Guid? MessageOpenChooseId);
    }
}
