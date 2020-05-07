using Cupcake.Core.Model;
using Cupcake.Plugin.MemberDengJi.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.MemberDengJi.Models
{
    public class OrganizationalChangeModel : BaseModel
    {
        [Display(Name = "用户Id")]
        public Guid? UserId { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "手机号")]
        public string Tel { get; set; }
        [Display(Name = "邮箱")]
        public string MailBox { get; set; }
        [Display(Name = "组织介绍")]
        public string OrganizationalIntroduce { get; set; }
        public OrganizationalChange ToInfo()
        {
            OrganizationalChange info = new OrganizationalChange();
            info.Id = this.Id;
            info.UserId = this.UserId;
            info.UserName = this.UserName;
            info.Tel = this.Tel;
            info.MailBox = this.MailBox;
            info.OrganizationalIntroduce = this.OrganizationalIntroduce;
            return info;
        }

        public OrganizationalChangeModel ToModel(OrganizationalChange info)
        {
            this.Id = info.Id;
            this.UserId = info.UserId;
            this.UserName = info.UserName;
            this.Tel = info.Tel;
            this.MailBox = info.MailBox;
            this.OrganizationalIntroduce = info.OrganizationalIntroduce;
            return this;
        }

        public OrganizationalChange FormData(OrganizationalChange info)
        {
            info.Id = this.Id;
            info.UserId = this.UserId;
            info.UserName = this.UserName;
            info.Tel = this.Tel;
            info.MailBox = this.MailBox;
            info.OrganizationalIntroduce = this.OrganizationalIntroduce;
            return info;
        }
    }
}
