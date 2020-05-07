using System;
using Cupcake.Core;
namespace Cupcake.Plugin.MessageOpen.Services
{
    //政策法规
    public interface IMessageOpenPolicyService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenPolicyInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenPolicyInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenPolicyCondition condition);
    }
}
