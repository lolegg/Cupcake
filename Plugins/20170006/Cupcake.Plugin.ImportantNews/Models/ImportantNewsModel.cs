using Cupcake.Core.Model;
using Cupcake.Plugin.ImportantNews.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.ImportantNews.Models
{
    public class ImportantNewsModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
         [Display(Name = "新闻类型")]
        public Guid NewsType { get; set; }
         [Display(Name = "发布时间")]
         public DateTime? ReleaseTime { get; set; }
         public ImportantNewsModel ToModel(ImportantNewsInfo info)
         {
             this.Id = info.Id;
             this.Title = info.Title;
             this.Content = info.Content;
             this.ReleaseTime = info.ReleaseTime;
             this.NewsType = info.NewsType;
             return this;
         }
    }
     
}
