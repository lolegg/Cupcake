using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Collections.Generic;

namespace Cupcake.Plugin.MessageOpen.Models
{

    //权利清单和责任清单
   public class MessageOpenRightDutyListingModel:BaseModel
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
        [Display(Name = "所属组织")]
        public string Department { get; set; }

        public List<Guid> OrganizationList { get; set; }

        public string DepartmentList { get; set; }
        public bool IsTop { get; set; }

        public MessageOpenRightDutyListingInfo ToInfo()
        {
            MessageOpenRightDutyListingInfo info = new MessageOpenRightDutyListingInfo();
            info.Title = this.Title;
            info.publicName = this.publicName;
            info.Content = this.Content;
            info.publicTime = this.publicTime;
            info.Department = this.Department;
            info.IsTop = this.IsTop;
            return info;
        }
        public MessageOpenRightDutyListingModel ToModel(MessageOpenRightDutyListingInfo info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.publicName = info.publicName;
            this.Content = info.Content;
            this.publicTime = info.publicTime;
            this.Department = info.Department;
            this.IsTop = info.IsTop;
            return this;
        }
        public MessageOpenRightDutyListingInfo FormData(MessageOpenRightDutyListingInfo info)
        {
            info.Title = this.Title;
            info.publicName = this.publicName;
            info.Content = this.Content;
            info.publicTime = this.publicTime;
            info.Department = this.Department;
            info.IsTop = this.IsTop;
            return info;
        }
    }
}
