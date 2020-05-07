using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.Template.Models
{
    public class NewsModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        public Guid NewsType { get; set; }
    }
}
