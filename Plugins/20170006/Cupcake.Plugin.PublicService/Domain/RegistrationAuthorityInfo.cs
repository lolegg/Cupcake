using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Domain
{
    public class RegistrationAuthorityInfo : PluginBaseEntity
    {
        /// <summary>
        /// 登记管理机关类别
        /// </summary>
        public Guid RegistrationAuthorityType { get; set; }
        /// <summary>
        /// 行政街道
        /// </summary>
        public Guid? ExecutiveStreet { get; set; }
        /// <summary>
        /// 办事网点地址
        /// </summary>
        public string Address { get; set; }

        public string lng { get; set; }

        public string lat { get; set; }
        /// <summary>
        /// 咨询电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 工作时间
        /// </summary>
        public string WorkingHours { get; set; }

    }
    public class RegistrationAuthorityCondition : Condition
    {
        public Guid? ExecutiveStreet { get; set; }
    }
}
