using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class QuestionnaireProblemModels
    {
        public class QuestionnaireProblemModel
        {
            [Required]
            [Display(Name = "问题名称")]
            public string Title { get; set; }
            [Required]
            [Display(Name = "答案类型")]
            public Guid? QuestionType { get; set; }
            [Required]
            [Display(Name = "问卷调查编号")]
            public Guid QuestionSurveyId { get; set; }
            [Required]
            [Display(Name = "是否必填")]
            public Guid? IsRequired { get; set; }
        }
    }
    public class QuestionnaireProblemCondition
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid QuestionSurveyId { get; set; }
        public Guid? QuestionType { get; set; }
        public Guid? IsRequired { get; set; }
        public IList<Questionnaire_QuestionResult> QuestionResultList { get; set; }
    }
}