using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Domain
{
    /// <summary>
    /// 答案
    /// </summary>
    public class QuestionnaireAnswer : PluginBaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 问题编号
        /// </summary>
        public Guid QuestionInfoId { get; set; }
        /// <summary>
        /// 答案类型
        /// </summary>
        public Guid QuestionType { get; set; }
    }
    public class QuestionnaireAnswerCondition : Condition
    {
        public string Title { get; set; }
        public Guid QuestionInfoId { get; set; }
        public Guid QuestionType { get; set; }
    }
}
