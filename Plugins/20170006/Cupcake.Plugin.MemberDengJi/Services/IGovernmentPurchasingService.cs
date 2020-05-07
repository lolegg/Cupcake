using Cupcake.Core;
using System;
namespace Cupcake.Plugin.MemberDengJi.Services
{
    public interface IGovernmentPurchasingService : IpluginBaseService<Cupcake.Plugin.MemberDengJi.Domain.GovernmentPurchasing>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MemberDengJi.Domain.GovernmentPurchasing> SearchGovernmentPurchasing(Cupcake.Plugin.MemberDengJi.Domain.GovernmentPurchasingCondition condition);
    }
}
