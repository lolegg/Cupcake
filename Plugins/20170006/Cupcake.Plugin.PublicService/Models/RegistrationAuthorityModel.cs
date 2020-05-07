using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Models
{
    public class RegistrationAuthorityModel : BaseModel
    {
        /// <summary>
        /// 登记管理机关类别
        /// </summary>
        [Required]
        [Display(Name = "登记管理机关类别")]
        public Guid RegistrationAuthorityType { get; set; }
        /// <summary>
        /// 行政街道
        /// </summary>
        [Display(Name = "行政街道")]
        public Guid? ExecutiveStreet { get; set; }
        /// <summary>
        /// 办事网点地址
        /// </summary>
        [Display(Name = "办事网点地址")]
        public string Address { get; set; }

        public string lng { get; set; }

        public string lat { get; set; }
        /// <summary>
        /// 咨询电话
        /// </summary>
        [Display(Name = "咨询电话")]
        public string Telephone { get; set; }
        /// <summary>
        /// 工作时间
        /// </summary>
        [Display(Name = "工作时间")]
        public string WorkingHours { get; set; }
    }
}
