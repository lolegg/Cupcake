using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Cupcake.Plugin.MessageOpen.Domain;

namespace Cupcake.Plugin.MessageOpen.Models
{
    //机构职能
   public class MessageOpenInstitutionalModel:BaseModel
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
        
        [Display(Name="机构职能类别")]
        public Guid? Institutional { get; set; }

        
        [Display(Name = "所属组织")]
        public string Department { get; set; }

        public List<Guid> OrganizationList { get; set; }

        public string DepartmentList { get; set; }
        public MessageOpenInstitutionalInfo ToInfo()
        {
            MessageOpenInstitutionalInfo info = new MessageOpenInstitutionalInfo();
            info.Title = this.Title;
            info.publicName = this.publicName;
            info.Content = this.Content;
            info.publicTime = this.publicTime;
            info.Institutional = this.Institutional;
            info.Department = this.Department;

            return info;
        }
        public MessageOpenInstitutionalModel ToModel(MessageOpenInstitutionalInfo info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.publicName = info.publicName;
            this.Content = info.Content;
            this.publicTime = info.publicTime;
            this.Institutional = info.Institutional;
            this.Department = info.Department;

            return this;
        }
        public MessageOpenInstitutionalInfo FormData(MessageOpenInstitutionalInfo info)
        {
            info.Title = this.Title;
            info.publicName = this.publicName;
            info.Content = this.Content;
            info.publicTime = this.publicTime;
            info.Institutional = this.Institutional;
            info.Department = this.Department;

            return info;
        }
    }
  
}
