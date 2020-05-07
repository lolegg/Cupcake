using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetWork.Models
{
    class ErrorReportingModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "所属下载")]
        public Guid? TheTitle { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        public ErrorReporting ToInfo()
        {
            ErrorReporting info = new ErrorReporting();
            info.Id = this.Id;
            info.Title = this.Title;
            info.TheTitle = this.TheTitle;
            info.Content = this.Content;
            return info;
        }

        public ErrorReportingModel ToModel(ErrorReporting info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.TheTitle = info.TheTitle;
            this.Content = info.Content;
            return this;
        }

        public ErrorReporting FormData(ErrorReporting info)
        {
            info.Id = this.Id;
            info.Title = this.Title;
            info.TheTitle = this.TheTitle;
            info.Content = this.Content;
            return info;
        }
    }
}
