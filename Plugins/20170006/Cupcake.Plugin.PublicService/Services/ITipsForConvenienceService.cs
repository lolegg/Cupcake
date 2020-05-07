using Cupcake.Core;
using Cupcake.Plugin.PublicService.Domain;
using System;
namespace Cupcake.Plugin.PublicService.Services
{
    public interface ITipsForConvenienceService:IpluginBaseService<TipsForConvenienceInfo>
    {
        Cupcake.Core.IPagedList<TipsForConvenienceInfo> SearchTipsForConvenience(TipsForConvenienceCondition condition);
    }
}
