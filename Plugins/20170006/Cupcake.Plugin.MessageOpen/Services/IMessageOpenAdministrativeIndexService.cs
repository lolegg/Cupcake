using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    //行政许可事项目录
    public interface IMessageOpenAdministrativeIndexService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAdministrativeIndexInfo>
    {
      Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenAdministrativeIndexInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenAdministrativeIndexCondition condition);
    }
}
