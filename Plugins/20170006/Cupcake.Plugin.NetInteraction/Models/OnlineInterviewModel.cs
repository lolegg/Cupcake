using Cupcake.Plugin.NetInteraction.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Models
{
    //在线访谈
   public class OnlineInterviewModel : BaseModel
    {
       [Required]
       [Display(Name = "主题")]
        public string theme { get; set; }//主题
          [Display(Name = "嘉宾")]
        public string Guest { get; set; }//嘉宾
          [Display(Name = "时间")]
        public DateTime? shijian { get; set; }//时间
          [Display(Name = "地点")]
        public string Address { get; set; }//地点
          [Display(Name = "是否开启网友提问")]
        //是否开启网友提问
        public bool IsEnabled { get; set; }


          public List<OnlineInterviewQuestionAsk> OnlineInterviewQuestionAskInfo { get; set; }
    }
}
