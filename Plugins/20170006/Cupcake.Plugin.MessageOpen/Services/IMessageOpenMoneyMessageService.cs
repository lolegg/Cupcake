using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    //财政资金信息
    public interface IMessageOpenMoneyMessageService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenMoneyMessageInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenMoneyMessageInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenMoneyMessageCondition condition);
    }
}
