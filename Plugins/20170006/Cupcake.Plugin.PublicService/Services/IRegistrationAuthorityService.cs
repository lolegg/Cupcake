using Cupcake.Core;
using Cupcake.Plugin.PublicService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Services
{
    public interface IRegistrationAuthorityService : IpluginBaseService<RegistrationAuthorityInfo>
    {
        Cupcake.Core.IPagedList<RegistrationAuthorityInfo> SearchRegistrationAuthority(RegistrationAuthorityCondition condition);
    }
}
