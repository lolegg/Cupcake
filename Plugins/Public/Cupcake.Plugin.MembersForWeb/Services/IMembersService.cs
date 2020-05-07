using Cupcake.Core;
using System;
using System.Collections.Generic;
namespace Cupcake.Plugin.MembersForWeb.Services
{
    public interface IMembersService : IpluginBaseService<Cupcake.Plugin.MembersForWeb.Domain.Members>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.MembersForWeb.Domain.Members> SearchNews(Cupcake.Plugin.MembersForWeb.Domain.MembersCondition condition);
       
    }
}
