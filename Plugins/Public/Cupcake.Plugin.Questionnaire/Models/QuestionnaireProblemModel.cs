using Cupcake.Core.Model;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Models
{
  public   class QuestionnaireProblemModel : BaseModel
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
    //public class QuestionnaireProblemListModel : ListModel<QuestionnaireProblemModel>
    //{
    //    public string ParentName { get; set; }
    //    public QuestionnaireProblemCondition condition { get; set; }
    //    public QuestionnaireProblemListModel(IList<QuestionnaireProblem> questionInfoList)
    //    {
    //        List = new List<QuestionnaireProblemModel>();
    //        foreach (var item in questionInfoList)
    //        {
    //            QuestionnaireProblemModel Questmodel = new Models.QuestionnaireProblemModel();
    //            Questmodel.Title = item.Title;
    //            Questmodel.QuestionType = item.QuestionType;
    //            Questmodel.QuestionSurveyId = item.QuestionSurveyId;
    //            Questmodel.IsRequired = item.IsRequired;
    //            Questmodel.Id = item.Id;
               
    //            List.Add(Questmodel);
    //        }
    //    }
    //}
}
