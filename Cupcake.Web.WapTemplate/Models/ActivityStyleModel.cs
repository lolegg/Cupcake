
using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Web.WapTemplate.Models
{
    public class ActivityStyleModel
    {
        public Guid Id { get; set; }
        [Display(Name = "活动名称")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "活动介绍")]
        public string Content { get; set; }

        [Display(Name = "发布时间")]
        public DateTime? DeliveryTime { get; set; }

        [Display(Name = "活动地址")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "是否置顶")]
        public bool IsTop { get; set; }

        [Display(Name = "相关图片")]
        public string Imgurl { get; set; }


        public ActivityStyleModel ToModel(Activity_ActivityStyle info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.Address = info.Address;
            this.Imgurl = info.Imgurl;
            this.IsTop = info.IsTop;
            this.Content = info.Content;
            this.DeliveryTime = info.DeliveryTime;
            return this;
        }

      
    }
}
