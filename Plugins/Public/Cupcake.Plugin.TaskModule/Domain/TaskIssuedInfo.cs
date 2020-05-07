using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.TaskModule.Domain
{
    public class TaskIssuedInfo : PluginBaseEntity
    {
        /// <summary>
        /// 任务概述
        /// </summary>
        public string TaskOverview { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 下发日期
        /// </summary>
        public DateTime IssuedDate { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime CompleteDate { get; set; }
          /// <summary>
        /// 附件名称
        /// </summary>
        public string EnclosureName { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string EnclosureUrl { get; set; }

        /// <summary>
        /// 附件缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 任务处置
        /// </summary>
        public bool TaskManagement { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string Publisher { get; set; }


    }
    public class TaskIssuedCondition : Condition
    {
        public string TaskOverview { get; set; }

        public DateTime? IssuedDateStart { get; set; }

        public DateTime? IssuedDateEnd { get; set; }

        public string TaskType { get; set; }

        public bool? TaskManagement { get; set; }

    }
}
