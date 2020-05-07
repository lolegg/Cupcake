using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    //政策解读
    public interface IMessageOpenPolicyUnscrambleService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenPolicyUnscrambleInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenPolicyUnscrambleInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenPolicyUnscrambleCondition condition);
    }
}
