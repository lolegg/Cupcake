using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.News.Domain;

namespace Cupcake.Plugin.News.Models
{
    public class NewsModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        public Guid NewsType { get; set; }

        public string Name { get; set; }
        public DateTime publicTime { get; set; }
        public NewsInfo ToInfo()
        {
            NewsInfo info = new NewsInfo();
            info.Id = this.Id;
            info.CreateDate = this.CreateDate;
            info.UpdateDate = this.UpdateDate;
            info.IsDelete = this.IsDelete;
            info.Title = this.Title;
            info.Content = this.Content;
            info.NewsType = this.NewsType;
            info.Name = this.Name;
            info.publicTime = this.publicTime;
            return info;
        }
        public NewsModel ToModel(NewsInfo info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.Content = info.Content;
            this.NewsType = info.NewsType;
            this.publicTime = info.publicTime;
            this.Name = info.Name;
            return this;
        }
        public NewsInfo FormData(NewsInfo info)
        {
            this.Id = info.Id;
            info.Title = this.Title;
            info.Content = this.Content;
            info.NewsType = this.NewsType;
            info.publicTime = this.publicTime;
            info.Name = this.Name;
            return info;
        }
    }
}
