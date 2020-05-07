using Cupcake.Core.Model;
using Cupcake.Plugin.Activity.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.Activity.Models
{
    public class ActivitysModel : BaseModel
    {
        [Display(Name = "标题")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "发布时间")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "开始时间")]
        [Required]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "结束时间")]
        [Required]
        public DateTime? EndDate { get; set; }
        [Display(Name = "活动地址")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "联系方式")]
        [Required]
        public string Tel { get; set; }
        [Display(Name = "活动类型")]
        [Required]
        public Guid? ActityType { get; set; }
        [Display(Name = "活动状态")]
        [Required]
        public Guid? ActityState { get; set; }
        [Display(Name = "相关图片")]
        public string Imgurl { get; set; }
        [Display(Name = "内容")]
        [Required]
        public string Conent { get; set; }
        public bool IsTop { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Dimension { get; set; }

        public Activitys ToInfo()
        {
            Activitys info = new Activitys();
            info.Title = this.Title;
            info.ReleaseDate = this.ReleaseDate;
            info.BeginDate = this.BeginDate;
            info.EndDate = this.EndDate;
            info.Address = this.Address;
            info.Tel = this.Tel;
            info.ActityType = this.ActityType;
            info.ActityState = this.ActityState;
            info.Imgurl = this.Imgurl;
            info.Conent = this.Conent;
            info.IsTop = this.IsTop;
            info.Longitude = this.Longitude;
            info.Dimension = this.Dimension;
            return info;
        }

        public ActivitysModel ToModel(Activitys info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.ReleaseDate = info.ReleaseDate;
            this.BeginDate = info.BeginDate;
            this.EndDate = info.EndDate;
            this.Address = info.Address;
            this.Tel = info.Tel;
            this.ActityType = info.ActityType;
            this.ActityState = info.ActityState;
            this.Imgurl = info.Imgurl;
            this.Conent = info.Conent;
            this.IsTop = info.IsTop;
            this.Longitude = info.Longitude;
            this.Dimension = info.Dimension;
            return this;
        }

        public Activitys FormData(Activitys info)
        {
            info.Id=this.Id ;
            info.Title = this.Title;
            info.ReleaseDate=this.ReleaseDate ;
            info.BeginDate = this.BeginDate;
            info.EndDate = this.EndDate;
            info.Address = this.Address;
            info.Tel = this.Tel;
            info.ActityType = this.ActityType;
            info.ActityState = this.ActityState;
            info.Imgurl = this.Imgurl;
            info.Conent = this.Conent;
            info.IsTop = this.IsTop;
            info.Longitude = this.Longitude;
            info.Dimension = this.Dimension;
            return info;
        }
    }


}
