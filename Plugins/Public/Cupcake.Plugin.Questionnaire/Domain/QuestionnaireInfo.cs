using Cupcake.Core.Domain;
using System;

namespace Cupcake.Plugin.Questionnaire.Domain
{
    public class QuestionnaireInfo : PluginBaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsRelease { get; set; }
       
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }

    public class QuestionnaireCondition : Condition
    {
        public string Title { get; set; }
        public bool IsRelease { get; set; }
    }
}
