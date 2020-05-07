using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Cupcake.Plugin.MessageOpen.Domain;


namespace Cupcake.Plugin.MessageOpen.Models
{
   public class MessageOpenMechanismModel:BaseModel
    {
        //名称
       [Required]
       [Display(Name = "名称")]
        public string Name { get; set; }
        //电话
       [Required]
       [Display(Name = "电话")]
        public string Phone { get; set; }
        //邮箱
       [Required]
       [Display(Name = "邮箱")]
        public string Mailbox { get; set; }
        //地址
       [Required]
       [Display(Name = "地址")]
        public string Address { get; set; }
        //接待时间
       [Required]
       [Display(Name = "接待时间")]
        public string MeetTime { get; set; }

       //发布人

       [Display(Name = "发布人")]
       public string publicName { get; set; }

       public MessageOpenMechanismInfo ToInfo()
       {
           MessageOpenMechanismInfo info = new MessageOpenMechanismInfo();
           info.Name = this.Name;
           info.Phone = this.Phone;
           info.Mailbox = this.Mailbox;
           info.Address = this.Address;
           info.MeetTime = this.MeetTime;
           return info;
       }
       public MessageOpenMechanismModel ToModel(MessageOpenMechanismInfo info)
       {
           this.Id = info.Id;
           this.Name = info.Name;
           this.Phone = info.Phone;
           this.Mailbox = info.Mailbox;
           this.Address = info.Address;
           this.MeetTime = info.MeetTime;
           return this;
       }
       public MessageOpenMechanismInfo FormData(MessageOpenMechanismInfo info)
       {
           info.Name = this.Name;
           info.Phone = this.Phone;
           info.Mailbox = this.Mailbox;
           info.Address = this.Address;
           info.MeetTime = this.MeetTime;
           return info;
       }
    }
}
