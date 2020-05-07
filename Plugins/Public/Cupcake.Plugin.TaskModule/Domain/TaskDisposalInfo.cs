using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.TaskModule.Domain
{
    public class TaskDisposalInfo : PluginBaseEntity
    {

        /// <summary>
        /// 任务下发Id
        /// </summary>
        public Guid TaskIssuedId { get; set; }

        /// <summary>
        /// 处置完成日期
        /// </summary>
        public DateTime DisposalCompletedDate { get; set; }
        /// <summary>
        /// 处置结果
        /// </summary>
        public string DisposalResult { get; set; }

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
        /// 处置人
        /// </summary>
        public string DisposalPerson { get; set; }
        /// <summary>
        /// 事件处置日期
        /// </summary>
        public DateTime EventDisposalDate { get; set; }
    }
    public class TaskDisposalCondition : Condition
    {
        public Guid? TaskIssuedId { get; set; }

    }
}
