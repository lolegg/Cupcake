using Cupcake.Core.Model;
using Cupcake.Plugin.Questionnaire.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.Questionnaire.Models
{
    public class QuestionnaireModel : BaseModel
    {
        [Required]
        [Display(Name = "问卷调查名称")]
        public string Title { get; set; }
        //[Required]
        [Display(Name = "是否发布")]
        public Guid? IsRelease { get; set; }
        //[Required]
        //[Display(Name = "信息来源")]
        //public SourceType SourceType { get; set; }
        [Required]
        [Display(Name = "描述")]
        public string Desc { get; set; }
        public string SyncControlStr { get; set; }

        public QuestionnaireInfo FormData(QuestionnaireInfo info)
        {
            info.Title = this.Title;
           // info.IsRelease = IsRelease.no;
            info.Desc = this.Desc;
            //info.SourceType = this.SourceType;
            return info;
        }
    }


    public class QuestionnaireListModel : ListModel<QuestionnaireModel>
    {
        public QuestionnaireCondition condition { get; set; }
        public QuestionnaireListModel(IList<QuestionnaireInfo> QuestionnaireItemList)
        {
            List = new List<QuestionnaireModel>();
            foreach (var item in QuestionnaireItemList)
            {
                QuestionnaireModel Questmodel = new Models.QuestionnaireModel();
                Questmodel.Title = item.Title;
                Questmodel.Desc = item.Desc;
                Questmodel.Id = item.Id;
                List.Add(Questmodel);
            }
        }
    }
}
