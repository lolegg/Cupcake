using Cupcake.Core.Model;
using Cupcake.Plugin.NetWork.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.NetWork.Models
{
    public class FormDownloadModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name="组织类型")]
        public Guid? WorkType { get; set; }
        [Display(Name = "文件下载")]
        public string Imgurl { get; set; }
        public string ImgName { get; set; }
        public string ImgSLT { get; set; }
        public FormDownload ToInfo()
        {
            FormDownload info = new FormDownload();
            info.Id = this.Id;
            info.Title = this.Title;
            info.WorkType = this.WorkType;
            info.Imgurl = this.Imgurl;
            info.ImgName = this.ImgName;
            info.ImgSLT = this.ImgSLT;
            return info;
        }

        public FormDownloadModel ToModel(FormDownload info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.WorkType = info.WorkType;
            this.Imgurl = info.Imgurl;
            this.ImgName = info.ImgName;
            this.ImgSLT = info.ImgSLT;
            return this;
        }

        public FormDownload FormData(FormDownload info)
        {
            info.Id = this.Id;
            info.Title = this.Title;
            info.WorkType = this.WorkType;
            info.Imgurl = this.Imgurl;
            info.ImgName = this.ImgName;
            info.ImgSLT = this.ImgSLT;
            return info;
        }
    }
}
