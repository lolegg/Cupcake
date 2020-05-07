using Cupcake.Core.Model;
using Cupcake.Plugin.MemberDengJi.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.MemberDengJi.Models
{
    public class GovernmentPurchasingModel : BaseModel
    {
        [Display(Name = "用户Id")]
        public Guid? UserId { get; set; }
        [Display(Name = "购买部门")]
        public string PurchasingDepartment { get; set; }
        [Display(Name = "服务名称")]
        public string ServiceName { get; set; }
        [Display(Name = "购买日期")]
        public DateTime? PurchasingTime { get; set; }
        [Display(Name = "服务费用")]
        public string ServiceCharge { get; set; }
        [Display(Name = "详细情况")]
        public string DetailsSituation { get; set; }
        public GovernmentPurchasing ToInfo()
        {
            GovernmentPurchasing info = new GovernmentPurchasing();
            info.UserId = this.UserId;
            info.PurchasingDepartment = this.PurchasingDepartment;
            info.ServiceName = this.ServiceName;
            info.PurchasingTime = this.PurchasingTime;
            info.ServiceCharge = this.ServiceCharge;
            info.DetailsSituation = this.DetailsSituation;
            return info;
        }

        public GovernmentPurchasingModel ToModel(GovernmentPurchasing info)
        {
            this.UserId = info.UserId;
            this.PurchasingDepartment = info.PurchasingDepartment;
            this.ServiceName = info.ServiceName;
            this.PurchasingTime = info.PurchasingTime;
            this.ServiceCharge = info.ServiceCharge;
            this.DetailsSituation = info.DetailsSituation;
            return this;
        }

        public GovernmentPurchasing FormData(GovernmentPurchasing info)
        {
            info.UserId = this.UserId;
            info.PurchasingDepartment = this.PurchasingDepartment;
            info.ServiceName = this.ServiceName;
            info.PurchasingTime = this.PurchasingTime;
            info.ServiceCharge = this.ServiceCharge;
            info.DetailsSituation = this.DetailsSituation;
            return info;
        }
    }
}
