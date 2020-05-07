using Cupcake.Core.Model;
using Cupcake.Plugin.TaskModule.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.TaskModule.Models
{
    public class TaskIssuedModel : BaseModel
    {
        [Required]
        [Display(Name = "任务概述")]
        public string TaskOverview { get; set; }
        [Required]
        [Display(Name = "详细描述")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "下发日期")]
        public DateTime IssuedDate { get; set; }
         [Required]
        [Display(Name = "任务类型")]
        public string TaskType { get; set; }
         [Required]
        [Display(Name = "完成日期")]
        public DateTime CompleteDate { get; set; }

        [Display(Name = "附件名称")]
        public string EnclosureName { get; set; }

        [Display(Name = "附件地址")]
        public string EnclosureUrl { get; set; }

        [Display(Name = "附件缩略图")]
        public string Thumbnail { get; set; }

        [Display(Name = "部门")]
        public string Department { get; set; }

        [Display(Name = "任务处置")]
        public bool TaskManagement { get; set; }
        [Display(Name = "发布人")]
        public string Publisher { get; set; }

        public List<Guid> OrganizationList { get; set; }
        public TaskDisposalModel taskDisposalModel { get; set; }

        public TaskIssuedInfo ToInfo()
        {
            TaskIssuedInfo info = new TaskIssuedInfo();
            info.TaskOverview = this.TaskOverview;
            info.Description = this.Description;
            info.IssuedDate = this.IssuedDate;
            info.TaskType = this.TaskType;
            info.CompleteDate = this.CompleteDate;
            info.EnclosureName = this.EnclosureName;
            info.EnclosureUrl = this.EnclosureUrl;
            info.Department = this.Department;
            info.TaskManagement = this.TaskManagement;
            info.Publisher = this.Publisher;
            info.Thumbnail = this.Thumbnail;
            return info;
        }
        public TaskIssuedModel ToModel(TaskIssuedInfo info)
        {
            this.Id = info.Id;
            this.TaskOverview = info.TaskOverview;
            this.Description = info.Description;
            this.IssuedDate = info.IssuedDate;
            this.TaskType = info.TaskType;
            this.CompleteDate = info.CompleteDate;
            this.EnclosureName = info.EnclosureName;
            this.EnclosureUrl = info.EnclosureUrl;
            this.Department = info.Department;
            this.TaskManagement = info.TaskManagement;
            this.Publisher = info.Publisher;
            this.Thumbnail = info.Thumbnail;
            return this;
        }
        public TaskIssuedInfo FormData(TaskIssuedInfo info)
        {
            info.TaskOverview = this.TaskOverview;
            info.Description = this.Description;
            info.IssuedDate = this.IssuedDate;
            info.TaskType = this.TaskType;
            info.CompleteDate = this.CompleteDate;
            info.EnclosureName = this.EnclosureName;
            info.EnclosureUrl = this.EnclosureUrl;
            info.Department = this.Department;
            info.TaskManagement = this.TaskManagement;
            info.Publisher = this.Publisher;
            info.Thumbnail = this.Thumbnail;
            return info;
        }
    }
    public class TaskIssuedListModel : ListModel<TaskIssuedModel>
    {
        public TaskIssuedCondition condition { get; set; }
        public TaskIssuedListModel(IList<TaskIssuedInfo> TaskIssuedList)
        {
            List = new List<TaskIssuedModel>();
            foreach (var info in TaskIssuedList)
            {
                TaskIssuedModel userModel = new TaskIssuedModel().ToModel(info);
                List.Add(userModel);
            }
        }
    }
}
