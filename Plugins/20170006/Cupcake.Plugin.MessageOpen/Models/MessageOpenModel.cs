using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.MessageOpen.Helper;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Collections.Generic;

namespace Cupcake.Plugin.MessageOpen.Models
{
  public class MessageOpenModel:BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Display(Name = "发布人")]
        public string publicName { get; set; }
        [Required]
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Required]
        [Display(Name = "发布时间")]
        public DateTime? publicTime { get; set; }
        [Required]
        [Display(Name = "政府信息公开/规定/目录/指南/申请/")]
        public Guid? MessageOpenChoose{get;set;}
        
        [Display(Name = "所属组织")]
        public string Department { get; set; }

        [Display(Name = "申请目录")]
        public string Catalog { get; set; }

        public List<Guid> OrganizationList { get; set; }

        public string DepartmentList { get; set; }


        public MessageOpenInfo ToInfo()
        {
            MessageOpenInfo info = new MessageOpenInfo();
            info.Title = this.Title;
            info.publicName = this.publicName;
            info.Content = this.Content;
            info.publicTime = this.publicTime;
            info.MessageOpenChoose = this.MessageOpenChoose;
            info.Department = this.Department;
            info.Catalog = this.Catalog;
            return info;
        }
        public MessageOpenModel ToModel(MessageOpenInfo info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.publicName = info.publicName;
            this.Content = info.Content;
            this.publicTime = info.publicTime;
            this.MessageOpenChoose = info.MessageOpenChoose;
            this.Department = info.Department;
            this.Catalog = info.Catalog;
            return this;
        }
        public MessageOpenInfo FormData(MessageOpenInfo info)
        {
            info.Title = this.Title;
            info.publicName = this.publicName;
            info.Content = this.Content;
            info.publicTime = this.publicTime;
            info.MessageOpenChoose = this.MessageOpenChoose;
            info.Department = this.Department;
            info.Catalog = this.Catalog;
            return info;
        }
    }
}
