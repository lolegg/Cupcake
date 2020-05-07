using Cupcake.Core;
using System;
namespace Cupcake.Plugin.MemberDengJi.Services
{
    public interface IOrganizationalChangeService : IpluginBaseService<Cupcake.Plugin.MemberDengJi.Domain.OrganizationalChange>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MemberDengJi.Domain.OrganizationalChange> SearchOrganizationalChange(Cupcake.Plugin.MemberDengJi.Domain.OrganizationalChangeCondition condition);
    }
}
