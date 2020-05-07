using Cupcake.Plugin.NetInteraction.Domain;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.NetInteraction.Models
{
   public class OnlineInterviewQuestionAskModel : BaseModel
    {
        public Guid OnlineInterviewId { get; set; }
        [Required]
        [Display(Name = "问题")]
        public string Question { get; set; }
          [Display(Name = "答复")]
        public string Ask { get; set; }
          [Display(Name = "姓名")]
        public string Name { get; set; }
          [Display(Name = "电话")]
        public string Phone { get; set; }
          [Display(Name = "地址")]
        public string Address { get; set; }


          public OnlineInterview OnlineInterview { get; set; }
    }
}
