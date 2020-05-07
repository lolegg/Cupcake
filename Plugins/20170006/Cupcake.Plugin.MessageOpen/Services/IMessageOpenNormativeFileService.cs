using System;
using Cupcake.Core;

namespace Cupcake.Plugin.MessageOpen.Services
{
    //规范性文件
    public interface IMessageOpenNormativeFileService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenNormativeFileInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenNormativeFileInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenNormativeFileCondition condition);
    }
}
