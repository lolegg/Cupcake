using Cupcake.Core.Model;
using Cupcake.Plugin.ImportantNews.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.ImportantNews.Models
{
    public class CarouselModel : BaseModel
    {

        public string CreatorId { get; set; }
         /// <summary>
         /// 图片路径
         /// </summary>
         /// [Required]
         [Display(Name = "图片路径")]
         public string PictureUrl { get; set; }

         /// <summary>
         /// 图片跳转路径
         /// </summary>
            [Display(Name = "路径ID")]
         public string PictureJumpPath { get; set; }

         /// <summary>
         /// 图片显示文字标题
         /// </summary>
           [Display(Name = "标题")]
         public string Title { get; set; }

           /// <summary>
           /// 轮播图所属模块
           /// </summary>
         [Display(Name = "轮播图所属模块")]  
        public Guid CarouselType { get; set; }

           /// <summary>
           /// 详情所属模块
           /// </summary>
         [Display(Name = "详情所属模块")] 
        public Guid? DetailsType { get; set; }

         public string LuJing { get; set; }

         public CarouselModel ToModel(CarouselInfo info)
         {
             this.Id = info.Id;
             this.Title = info.Title;
             this.CarouselType = info.CarouselType;
             this.DetailsType = info.DetailsType;
             this.PictureUrl = info.PictureUrl;
             this.PictureJumpPath = info.PictureJumpPath;
             return this;
         }
    }
     
}
