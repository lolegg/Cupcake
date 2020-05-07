using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.MessageOpen.Domain
{
    //申请表单
  public class MessageOpenApplicationInfo:PluginBaseEntity
  {
      #region 公民
      //姓名
      public string publicName { get; set; }
      //证件名称
      public string publicCardName { get; set; }
      //证件号码
      public string publicCardNumber { get; set; }

      //通信地址
      public string publicAddress { get; set; }
      //邮政编码
      public string publicPostalCode { get; set; }
      //联系电话
      public string publicPhone { get; set; }
      //传真
      public string publicFax { get; set; }
      //电子邮箱
      public string publicMail { get; set; }
      #endregion

      #region 法人\其他组织
      //姓名
      public string OtherName { get; set; }
      //组织机构代码
      public string OtherCode { get; set; }
      //通信地址
      public string OtherAddress { get; set; }

      //法人代表
      public string OtherDeputy { get; set; }
      //联系人姓名
      public string OtherContactName { get; set; }
      //联系人电话
      public string OtherContactPhone { get; set; }
      //传真
      public string OtherFax { get; set; }
      //联系人电子邮件
      public string Othermail { get; set; }
      #endregion


      #region 所需信息情况
      //详情
      public string Details { get; set; }

      //所需信息索取号
      public string ClaimNumber { get; set; }
      //用途
      public string purpose { get; set; }

      //是否申请减免费用
      public string ApplyCost { get; set; }
      //所需信息的指定提供方式
      public string ApplyProviding{ get; set; }
      //获取信息的方式
      public string Applyinformation { get; set; }

      //是否接受其他方式
      public bool OtherWays { get; set; }
      #endregion
      //反馈
      public string Feedback { get; set; }

      //是否公开
      public bool IsPublic { get; set; }
  }
  public class MessageOpenApplicationCondition : Condition
  {
  }
}
