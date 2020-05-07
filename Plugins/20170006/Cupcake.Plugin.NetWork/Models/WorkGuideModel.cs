using Cupcake.Core.Model;
using Cupcake.Plugin.NetWork.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.NetWork.Models
{
    public class WorkGuideModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name="组织类型")]
        public Guid? WorkType { get; set; }
        [Display(Name = "适用范围")]
        public string ScopeOfApplication { get; set; }
        [Display(Name = "承诺时限")]
        public string CommitmentTimeLimit { get; set; }
        [Display(Name = "法定时限")]
        public string LegalTimeLimit { get; set; }
        [Display(Name = "办理地点及时间")]
        public string HandlePlaceTime { get; set; }
        [Display(Name = "申请条件")]
        public string ApplicationConditions { get; set; }
        [Display(Name = "申请材料")]
        public string ApplicationMaterials { get; set; }
        [Display(Name = "设定依据")]
        public string SetBasis { get; set; }
        [Display(Name = "办理流程")]
        public string ManagementProcess { get; set; }
        [Display(Name = "其他")]
        public string Other { get; set; }

        public WorkGuide ToInfo()
        {
            WorkGuide info = new WorkGuide();
            info.Id = this.Id;
            info.Title = this.Title;
            info.WorkType = this.WorkType;
            info.ScopeOfApplication = this.ScopeOfApplication;
            info.CommitmentTimeLimit = this.CommitmentTimeLimit;
            info.LegalTimeLimit = this.LegalTimeLimit;
            info.HandlePlaceTime = this.HandlePlaceTime;
            info.ApplicationConditions = this.ApplicationConditions;
            info.ApplicationMaterials = this.ApplicationMaterials;
            info.SetBasis = this.SetBasis;
            info.ManagementProcess = this.ManagementProcess;
            info.Other = this.Other;
            return info;
        }

        public WorkGuideModel ToModel(WorkGuide info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.WorkType = info.WorkType;
            this.ScopeOfApplication = info.ScopeOfApplication;
            this.CommitmentTimeLimit = info.CommitmentTimeLimit;
            this.LegalTimeLimit = info.LegalTimeLimit;
            this.HandlePlaceTime = info.HandlePlaceTime;
            this.ApplicationConditions = info.ApplicationConditions;
            this.ApplicationMaterials = info.ApplicationMaterials;
            this.SetBasis = info.SetBasis;
            this.ManagementProcess = info.ManagementProcess;
            this.Other = info.Other;
            return this;
        }

        public WorkGuide FormData(WorkGuide info)
        {
            info.Id = this.Id;
            info.Title = this.Title;
            info.WorkType = this.WorkType;
            info.ScopeOfApplication = this.ScopeOfApplication;
            info.CommitmentTimeLimit = this.CommitmentTimeLimit;
            info.LegalTimeLimit = this.LegalTimeLimit;
            info.HandlePlaceTime = this.HandlePlaceTime;
            info.ApplicationConditions = this.ApplicationConditions;
            info.ApplicationMaterials = this.ApplicationMaterials;
            info.SetBasis = this.SetBasis;
            info.ManagementProcess = this.ManagementProcess;
            info.Other = this.Other;
            return info;
        }
    }
}
