using Cupcake.Core;
using Cupcake.Plugin.PublicService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Services
{
    public interface ISocialOrganizationNameService : IpluginBaseService<SocialOrganizationNameInfo>
    {
        Cupcake.Core.IPagedList<SocialOrganizationNameInfo> SearchSocialOrganizationName(SocialOrganizationNameCondition condition);
    }
}
