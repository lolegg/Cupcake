using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.PublicService.Models
{
    public class TipsForConvenienceModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
    }
}
