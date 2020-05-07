using Cupcake.Data.Mappings;
using Cupcake.Plugin.PublicService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Data
{
   public class RegistrationAuthorityMap : PluginBaseEntityMapping<RegistrationAuthorityInfo>
    {
        public RegistrationAuthorityMap()
        {
            this.ToTable("PublicService_RegistrationAuthority");
        }
    }
}
