using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Domain
{
    /// <summary>
    /// 调查统计
    /// </summary>
    public class QuestionStatistic : PluginBaseEntity
    {
        /// <summary>
        /// 问卷编号
        /// </summary>
        public Guid QuestionSurveyId { get; set; }
        /// <summary>
        /// 问题编号
        /// </summary>
        public Guid QuestionInfoId { get; set; }
        /// <summary>
        /// 结果编号
        /// </summary>
        public Guid QuestionResultId { get; set; }
        /// <summary>
        /// 答案类型
        /// </summary>
        public Guid QuestionType { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// Uid
        /// </summary>
        public int Uid { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }
    }
    public class QuestionStatisticResult
    {
        public Guid? QuestionSurveyId { get; set; }
        public string QuestionSurveyName { get; set; }
        public string QuestionInfoName { get; set; }
        public string QuestionResultName { get; set; }
        public int Count { get; set; }
    }
    public class QuestionStatisticList
    {
        public List<QuestionStatisticResult> List { get; set; }
    }

    public class QuestionStatisticCondition : Condition
    {
        public Guid? QuestionSurveyId { get; set; }
        public string Result { get; set; }
    }
}
