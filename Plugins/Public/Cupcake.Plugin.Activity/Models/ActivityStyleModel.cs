using Cupcake.Plugin.Activity.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Activity.Models
{
    public class ActivityStyleModel : BaseModel
    {
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
        public ActivityStyle ToInfo()
        {
            ActivityStyle info = new ActivityStyle();
            info.Title = this.Title;
            info.DeliveryTime = this.DeliveryTime;
            info.Address = this.Address;
            info.Imgurl = this.Imgurl;
            info.Content = this.Content;
            info.IsTop = this.IsTop;
            return info;
        }

        public ActivityStyleModel ToModel(ActivityStyle info)
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

        public ActivityStyle FormData(ActivityStyle info)
        {
            info.Title = this.Title;
            info.DeliveryTime = this.DeliveryTime;
            info.Address = this.Address;
            info.Imgurl = this.Imgurl;
            info.Content = this.Content;
            info.IsTop = this.IsTop;
            return info;
        }
    }
}
