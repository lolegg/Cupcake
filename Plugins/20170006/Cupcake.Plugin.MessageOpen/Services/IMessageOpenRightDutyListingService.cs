using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    //权利清单和责任清单
    public interface IMessageOpenRightDutyListingService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenRightDutyListingInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenRightDutyListingInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenRightDutyListingCondition condition);
    }
}
