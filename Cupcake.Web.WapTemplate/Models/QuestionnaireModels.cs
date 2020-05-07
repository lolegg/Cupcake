
using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class QuestionnaireModels
    {
        public Guid id { get; set; } 

        [Required]
        [Display(Name = "问卷调查名称")]
        public string Title { get; set; }
        //[Required]
        [Display(Name = "是否发布")]
        public Guid? IsRelease { get; set; }
       
        [Required]
        [Display(Name = "描述")]
        public string Desc { get; set; }
        public string SyncControlStr { get; set; }

    }
    public class QuestionSurveyListModel: ListModel<QuestionnaireModels>
    {
        public QuestionnaireCondition condition { get; set; }
        public QuestionSurveyListModel(IList<Questionnaire_QuestionSurvey> questionSurveyList)
        {
         List = new List<QuestionnaireModels>();
            foreach (var survey in questionSurveyList)
            {
                QuestionnaireModels model = new QuestionnaireModels();
                model.id = survey.Id;
                model.Title = survey.Title;
                model.Desc = survey.Desc;
                List.Add(model);
            }
        }
    }

    public class QuestionnaireCondition
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? IsRelease { get; set; }
    }
}