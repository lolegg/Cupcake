using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Domain
{
    public class SocialOrganizationNameInfo : PluginBaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        public string lng { get; set; }

        public string lat { get; set; }
    }
    public class SocialOrganizationNameCondition : Condition
    {
        public string Name { get; set; }
    }
}
