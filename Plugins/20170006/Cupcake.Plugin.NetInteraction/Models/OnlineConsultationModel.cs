using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Models
{
    //网上咨询
  public  class OnlineConsultationModel : BaseModel
    {
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "手机号码")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "标题")]

        public string Title { get; set; }
        [Required]
        [Display(Name = "内容")]
        public string Content { get; set; }

        //答复
       [Required]
        [Display(Name = "答复")]
        public string Reply { get; set; }

        //是否显示到前端页面
        [Display(Name = "是否显示到前端页面")]
        public bool IsDisplay { get; set; }

        //询问时间
         [Display(Name = "询问时间")]
        public DateTime? AskTime { get; set; }
        //答复时间
       [Required]
         [Display(Name = "答复时间")]
        public DateTime? ReplyTime { get; set; }

         [Display(Name = "序列号")]
         public int serialNumber { get; set; }
    }
}
