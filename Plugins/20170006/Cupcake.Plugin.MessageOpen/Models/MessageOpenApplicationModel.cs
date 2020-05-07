using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Cupcake.Plugin.MessageOpen.Domain;
using System.Collections.Generic;

namespace Cupcake.Plugin.MessageOpen.Models
{
   public class MessageOpenApplicationModel:BaseModel
    {
        #region 公民
   
        //姓名
         [Display(Name = "姓名")]
        public string publicName { get; set; }
        //证件名称
       [Display(Name = "证件名称")]
        public string publicCardName { get; set; }
        //证件号码
       [Display(Name = "证件号码")]
        public string publicCardNumber { get; set; }

        //通信地址
        [Display(Name = "通信地址")]
        public string publicAddress { get; set; }
        //邮政编码
        [Display(Name = "邮政编码")]
        public string publicPostalCode { get; set; }
        //联系电话
        [Display(Name = "联系电话")]
        public string publicPhone { get; set; }
        //传真
        [Display(Name = "传真")]
        public string publicFax { get; set; }
        //电子邮箱
        [Display(Name = "电子邮箱")]
        public string publicMail { get; set; }
        #endregion

        #region 法人\其他组织
        //姓名
        [Display(Name = "姓名")]
        public string OtherName { get; set; }
        //组织机构代码
        [Display(Name = "组织机构代码")]
        public string OtherCode { get; set; }
        //通信地址
        [Display(Name = "通信地址")]
        public string OtherAddress { get; set; }

        //法人代表
        [Display(Name = "法人代表")]
        public string OtherDeputy { get; set; }
        //联系人姓名
        [Display(Name = "联系人姓名")]
        public string OtherContactName { get; set; }
        //联系人电话
        [Display(Name = "联系人电话")]
        public string OtherContactPhone { get; set; }
        //传真
        [Display(Name = "传真")]
        public string OtherFax { get; set; }
        //联系人电子邮件
        [Display(Name = "联系人电子邮件")]
        public string Othermail { get; set; }
        #endregion


        #region 所需信息情况
        //详情
        [Display(Name = "详情")]
        public string Details { get; set; }

        //所需信息索取号
        [Display(Name = "所需信息索取号")]
        public string ClaimNumber { get; set; }
        //用途
        [Display(Name = "用途")]
        public string purpose { get; set; }

        //是否申请减免费用
        [Display(Name = "是否申请减免费用")]
        public string ApplyCost { get; set; }
        //所需信息的指定提供方式
        [Display(Name = "所需信息的指定提供方式")]
        public string ApplyProviding { get; set; }
        //获取信息的方式
        [Display(Name = "获取信息的方式")]
        public string Applyinformation { get; set; }

        //是否接受其他方式
        [Display(Name = "是否接受其他方式")]
        public bool OtherWays { get; set; }
        #endregion


        //反馈
        public string Feedback { get; set; }

        //是否公开
        public bool IsPublic { get; set; }

        public MessageOpenApplicationModel ToModel(MessageOpenApplicationInfo info) { 
        if(info.publicName!=null){
            this.publicName = info.publicName;
            this.publicCardName = info.publicCardName;
            this.publicCardNumber = info.publicCardNumber;
            this.publicAddress = info.publicAddress;
            this.publicPostalCode = info.publicPostalCode;
            this.publicPhone = info.publicPhone;
            this.publicFax = info.publicFax;
            this.publicMail = info.publicMail;
           
        }
        if(info.OtherName!=null){
            this.OtherName = info.OtherName;
            this.OtherCode = info.OtherCode;
            this.OtherAddress = info.OtherAddress;
            this.OtherDeputy = info.OtherDeputy;
            this.OtherContactName = info.OtherContactName;
            this.OtherContactPhone = info.OtherContactPhone;
            this.OtherFax = info.OtherFax;
            this.Othermail = info.Othermail;
           }
        this.Details = info.Details;
        this.ClaimNumber = info.ClaimNumber;
        this.purpose = info.purpose;
        this.ApplyCost = info.ApplyCost;
        this.ApplyProviding = info.ApplyProviding;
        this.Applyinformation = info.Applyinformation;
        this.OtherWays = info.OtherWays;
        this.Feedback = info.Feedback;
        return this;
        }

        public MessageOpenApplicationInfo FormData(MessageOpenApplicationInfo info) {
            info.Id = info.Id;
  
            info.IsDelete = info.IsDelete; ;
            info.CreateDate = info.CreateDate;
            info.UpdateDate = info.CreateDate;

            info.publicName = info.publicName;
            info.publicCardName = info.publicCardName;
            info.publicCardNumber = info.publicCardNumber;
            info.publicAddress = info.publicAddress;
            info.publicPostalCode = info.publicPostalCode;
            info.publicPhone = info.publicPhone;
            info.publicFax = info.publicFax;
            info.publicMail = info.publicMail;
            info.OtherName = info.OtherName;
            info.OtherCode = info.OtherCode;
            info.OtherAddress = info.OtherAddress;
            info.OtherDeputy = info.OtherDeputy;
            info.OtherContactName = info.OtherContactName;
            info.OtherContactPhone = info.OtherContactPhone;
            info.OtherFax = info.OtherFax;
            info.Othermail = info.Othermail;

            info.Details = info.Details;
            info.ClaimNumber = info.ClaimNumber;
            info.purpose = info.purpose;
            info.ApplyCost = info.ApplyCost;
            info.ApplyProviding = info.ApplyProviding;
            info.Applyinformation = info.Applyinformation;
            info.OtherWays = info.OtherWays;
            info.IsPublic = info.IsPublic;
            info.Feedback = this.Feedback;
            return info;
        }
    }
}
