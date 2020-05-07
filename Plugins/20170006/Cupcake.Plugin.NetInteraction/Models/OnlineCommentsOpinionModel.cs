using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Models
{
    public class OnlineCommentsOpinionModel : BaseModel
    {

        public Guid OnlineCommentsId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "内容")]
        public string Content { get; set; }
    }
}
