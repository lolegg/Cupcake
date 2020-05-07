using Cupcake.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.Questionnaire.Models
{
   public  class QuestionnaireAnswerModel : BaseModel
    {
        [Required]
        [Display(Name = "答案名称")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "问题编号")]
        public Guid QuestionInfoId { get; set; }
        [Required]
        [Display(Name = "答案类型")]
        public Guid QuestionType { get; set; }
    }
}
