using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.Announcement.Domain;
using System.Collections.Generic;

namespace Cupcake.Plugin.Announcement.Models
{
    public class AnnouncementModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }

        public int IsPlaced { get; set; }
        [Required]
        [Display(Name = "发布人")]
        public string Name { get; set; }

        [Display(Name = "所属组织")]
        public string Department { get; set; }

        public List<Guid> OrganizationList { get; set; }

        public string DepartmentList { get; set; }
        [Display(Name = "附件下载")]
        public string Imgurl { get; set; }
        public string ImgName { get; set; }
        public string ImgSLT { get; set; }
        public AnnouncementInfo ToInfo()
        {
            AnnouncementInfo info = new AnnouncementInfo();
            info.Id = this.Id;
            info.CreateDate = this.CreateDate;
            info.UpdateDate = this.UpdateDate;
            info.IsDelete = this.IsDelete;
            info.Title = this.Title;
            info.Content = this.Content;
            info.Name = this.Name;
            info.Department = this.Department;
            info.Imgurl = this.Imgurl;
            info.ImgName = this.ImgName;
            info.ImgSLT = this.ImgSLT;
            return info;
        }
        public AnnouncementModel ToModel(AnnouncementInfo info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.Name = info.Name;
            this.Content = info.Content;
            this.Department = info.Department;
            this.Imgurl = info.Imgurl;
            this.ImgName = info.ImgName;
            this.ImgSLT = info.ImgSLT;
            return this;
        }
        public AnnouncementInfo FormData(AnnouncementInfo info)
        {
         info.Id= this.Id;
         info.CreateDate = this.CreateDate;
         info.UpdateDate=this.UpdateDate;
         info.IsDelete = this.IsDelete;
         info.Title = this.Title;
         info.Content = this.Content;
         info.Department=this.Department;
         info.Imgurl = this.Imgurl;
         info.ImgName = this.ImgName;
         info.ImgSLT = this.ImgSLT;
         return info;
        }
    }
}
