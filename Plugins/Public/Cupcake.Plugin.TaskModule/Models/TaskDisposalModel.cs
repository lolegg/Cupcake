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
    public class TaskDisposalModel : BaseModel
    {
        /// <summary>
        /// 任务下发Id
        /// </summary>
        [Display(Name = "任务下发Id")]
        public Guid TaskIssuedId { get; set; }

        /// <summary>
        /// 处置完成日期
        /// </summary>
        [Display(Name = "处置完成日期")]
        [Required]
        public DateTime DisposalCompletedDate { get; set; }
        /// <summary>
        /// 处置结果
        /// </summary>
        [Display(Name = "处置结果")]
        public string DisposalResult { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
       [Display(Name = "附件名称")]
        public string EnclosureName { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        [Display(Name = "附件地址")]
        public string EnclosureUrl { get; set; }

        [Display(Name = "附件缩略图")]
        public string Thumbnail { get; set; }
        /// <summary>
        /// 处置人
        /// </summary>
        [Display(Name = "处置人")]
        [Required]
        public string DisposalPerson { get; set; }
        /// <summary>
        /// 事件处置日期
        /// </summary>
       [Display(Name = "事件处置日期")]
       [Required]
        public DateTime EventDisposalDate { get; set; }

       public TaskDisposalInfo ToInfo()
       {
           TaskDisposalInfo info = new TaskDisposalInfo();
           info.TaskIssuedId = this.TaskIssuedId;
           info.DisposalCompletedDate = this.DisposalCompletedDate;
           info.DisposalResult = this.DisposalResult;
           info.EnclosureName = this.EnclosureName;
           info.EnclosureUrl = this.EnclosureUrl;
           info.DisposalPerson = this.DisposalPerson;
           info.EventDisposalDate = this.EventDisposalDate;
           info.Thumbnail = this.Thumbnail;
           return info;
       }
       public TaskDisposalModel ToModel(TaskDisposalInfo info)
       {
           this.Id = info.Id;
           this.TaskIssuedId = info.TaskIssuedId;
           this.DisposalCompletedDate = info.DisposalCompletedDate;
           this.DisposalResult = info.DisposalResult;
           this.EnclosureName = info.EnclosureName;
           this.EnclosureUrl = info.EnclosureUrl;
           this.DisposalPerson = info.DisposalPerson;
           this.EventDisposalDate = info.EventDisposalDate;
           this.Thumbnail = info.Thumbnail;
           return this;
       }
       public TaskDisposalInfo FormData(TaskDisposalInfo info)
       {
           info.TaskIssuedId = this.TaskIssuedId;
           info.DisposalCompletedDate = this.DisposalCompletedDate;
           info.DisposalResult = this.DisposalResult;
           info.EnclosureName = this.EnclosureName;
           info.EnclosureUrl = this.EnclosureUrl;
           info.DisposalPerson = this.DisposalPerson;
           info.EventDisposalDate = this.EventDisposalDate;
           info.Thumbnail = this.Thumbnail;
           return info;
       }
    }
}
