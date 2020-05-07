using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class SysSetForm : FrameworkBaseEntity
    {
        public string TableName { get; set; }

        public string FuntionName { get; set; }

        /// <summary>
        /// 保存列名、列显示名称、录入控件等
        /// </summary>
        public string Columns { get; set; }

        /// <summary>
        /// 保存查询条件的集合
        /// </summary>
        public string QueryCriteria { get; set; }

        public PublishStatus Status { get; set; }
    }

    public class SysSetFormCondition : Condition
    {
        public string FuntionName { get; set; }
        public PublishStatus? Status { get; set; }
    }
}
