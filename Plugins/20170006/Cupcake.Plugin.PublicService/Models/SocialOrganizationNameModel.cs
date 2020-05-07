using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PublicService.Models
{
    public class SocialOrganizationNameModel : BaseModel
    {
        /// <summary>
        /// 社会组织名称
        /// </summary>
        [Required]
        [Display(Name = "社会组织名称")]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        [Display(Name = "地址")]
        public string Address { get; set; }

        public string lng { get; set; }

        public string lat { get; set; }
    }
}
