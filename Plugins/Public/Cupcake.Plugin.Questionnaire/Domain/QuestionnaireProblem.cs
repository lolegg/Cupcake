using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Domain
{
    /// <summary>
    /// 问题
    /// </summary>
  public   class QuestionnaireProblem : PluginBaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 答案类型
        /// </summary>
        public Guid? QuestionType { get; set; }
        /// <summary>
        /// 问卷调查编号
        /// </summary>
        public Guid QuestionSurveyId { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public Guid? IsRequired { get; set; }

        public QuestionnaireProblemCondition condition { get; set; }
    }

    public class QuestionnaireProblemCondition : Condition
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid QuestionSurveyId { get; set; }
        //public Guid? QuestionType { get; set; }
        //public Guid? IsRequired { get; set; }
        //public IList<QuestionResult> QuestionResultList { get; set; }
    }
}
