using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
   //信息公开年报
    public interface IMessageOpenAnnualService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAnnualInfo>
    {
       Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAnnualInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenAnnualCondition condition);
    }
}
