using System;
using Cupcake.Core;


namespace Cupcake.Plugin.MessageOpen.Services
{
    //机构职能
    public interface IMessageOpenInstitutionalService : IpluginBaseService<Cupcake.Plugin.MessageOpen.Domain.MessageOpenInstitutionalInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MessageOpen.Domain.MessageOpenInstitutionalInfo> SearchNews(Cupcake.Plugin.MessageOpen.Domain.MessageOpenInstitutionalCondition condition);

        int GetAddMessageOpenInstitutionalCount(Guid? id);

        int GetEditMessageOpenInstitutionalCount(Guid id, Guid? InstitutionalId);
    }
}
