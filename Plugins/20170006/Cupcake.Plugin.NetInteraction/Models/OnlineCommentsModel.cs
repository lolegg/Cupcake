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
    //网上征询
    public class OnlineCommentsModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }


        ////背景介绍
        //[Display(Name = "背景介绍")]
        //public string BackgroundIntroduction { get; set; }
        ////草案全文
        //[Display(Name = "草案全文")]
        //public string DraftFullText { get; set; }
        ////公众意见与建议
        //[Display(Name = "公众意见与建议")]
        //public string Opinion { get; set; }
        ////公众意见采纳情况反馈
        //[Display(Name = "公众意见采纳情况反馈")]
        //public string Feedback { get; set; }
        //是否结束
        [Display(Name = "是否结束")]
        public bool IsEnd { get; set; }
         [Display(Name = "开始时间")]
        public DateTime? StartTime { get; set; }
         [Display(Name = "结束时间")]
        public DateTime? EndTime { get; set; }

         public List<OnlineCommentsOpinion> OnlineCommentsOpinionInfo { get; set; }
    }
}
