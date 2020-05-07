using Cupcake.Core.Domain;
using Cupcake.Core.Model;
using Cupcake.Plugin.WorkNotice.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cupcake.Plugin.WorkNotice.Models
{
    public class WorkNoticeModel : BaseModel
    {
        [Required]
        [Display(Name = "提醒名称")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "提醒内容")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "发布人")]
        public string Publisher { get; set; }
        [Required]
        [Display(Name = "发布时间")]
        public DateTime? PublishDate { get; set; }
        [Required]
        [Display(Name = "工作截止日期")]
        public DateTime? WorkAbortDate { get; set; }
        [Required]
        [Display(Name = "所属组织")]
        public string Department { get; set; }
        [Required]
        [Display(Name = "相关附件")]
        public string Imgurl { get; set; }
        public string ImgName { get; set; }
        public string ImgSLT { get; set; }
        public List<Guid> OrganizationList { get; set; }

        public WorkNotices ToInfo()
        {
            WorkNotices info = new WorkNotices();
            info.Title = this.Title;
            info.Content = this.Content;
            info.Publisher = this.Publisher;
            info.PublishDate = this.PublishDate;
            info.WorkAbortDate = this.WorkAbortDate;
            info.Department = this.Department;
            info.Imgurl = this.Imgurl;
            info.ImgName = this.ImgName;
            info.ImgSLT = this.ImgSLT;
            return info;
        }

        public WorkNoticeModel ToModel(WorkNotices info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.Content = info.Content;
            this.Publisher = info.Publisher;
            this.PublishDate = info.PublishDate;
            this.WorkAbortDate = info.WorkAbortDate;
            this.Department = info.Department;
            this.Imgurl = info.Imgurl;
            this.ImgName = info.ImgName;
            this.ImgSLT = info.ImgSLT;
            return this;
        }
        public WorkNotices FormData(WorkNotices info)
        {
            info.Id = this.Id;
            info.Title = this.Title;
            info.Content = this.Content;
            info.Publisher = this.Publisher;
            info.PublishDate = this.PublishDate;
            info.WorkAbortDate = this.WorkAbortDate;
            info.Department = this.Department;
            info.Imgurl = this.Imgurl;
            info.ImgName = this.ImgName;
            info.ImgSLT = this.ImgSLT;
            return info;
        }
    }
}
