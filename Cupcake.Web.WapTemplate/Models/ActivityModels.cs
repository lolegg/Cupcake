
using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class ActivityModels
    {
        public Guid Id { get; set; }
        [Display(Name = "标题")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "发布时间")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "开始时间")]
        [Required]
        public DateTime? BeginDate { get; set; }
        [Display(Name = "结束时间")]
        [Required]
        public DateTime? EndDate { get; set; }
        [Display(Name = "活动地址")]
        [Required]
        public string Address { get; set; }
        [Display(Name = "联系方式")]
        [Required]
        public string Tel { get; set; }
        [Display(Name = "活动类型")]
        [Required]
        public Guid? ActityType { get; set; }
        [Display(Name = "活动状态")]
        [Required]
        public Guid? ActityState { get; set; }
        [Display(Name = "相关图片")]
        public string Imgurl { get; set; }
        [Display(Name = "内容")]
        [Required]
        public string Conent { get; set; }
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Dimension { get; set; }

        [Display(Name = "报名人姓名：")]
        public string RegistrationName { get; set; }
        [Display(Name = "报名人电话：")]
        public string RegistrationTel { get; set; }
        public Guid? SubordinateActivitiesID { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public Guid UserId { get; set; }

        public List<Dictionary> DataDictionary { get; set; }

        public List<Activity_ActivityStyle> ActivityStyle { get; set; }

        public ActivityModels()
        {
            Condition = new ActivityCondition();
        }
        public ActivityModels ToModel(Activity_Activitys info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.ReleaseDate = info.ReleaseDate;
            this.BeginDate = info.BeginDate;
            this.EndDate = info.EndDate;
            this.Address = info.Address;
            this.Tel = info.Tel;
            this.ActityType = info.ActityType;
            this.ActityState = info.ActityState;
            this.Imgurl = info.Imgurl;
            this.Conent = info.Conent;
            return this;
        }

        public ActivityCondition Condition { get; set; }
    }

    public class Dictionary 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Sort { get; set; }
        public string Tips { get; set; }
        public int? CustomType { get; set; }
        public string CustomAttributes { get; set; }
        public Guid ParentId { get; set; }

    }


    public class ActivityModelsListModel : ListModel<ActivityModels>
    {
        public string Index { get; set; }
        public string Total { get; set; }
        public string Size { get; set; }

        public List<Dictionary> DataDictionary { get; set; }
        public ActivityModelsListModel(IList<Activity_Activitys> ActivityModelsListModel)
        {
             List = new List<ActivityModels>();
            foreach (var dp in ActivityModelsListModel)
            {
                ActivityModels ActivityModels = new ActivityModels();
                ActivityModels.Title = dp.Title;
                ActivityModels.ReleaseDate = dp.ReleaseDate;
                ActivityModels.BeginDate =dp.BeginDate;
                ActivityModels.EndDate = dp.EndDate;
                ActivityModels.Address = dp.Address;
                ActivityModels.Tel = dp.Tel;
                ActivityModels.ActityType = dp.ActityType;
                ActivityModels.ActityState = dp.ActityState;
                ActivityModels.Imgurl = dp.Imgurl;
                ActivityModels.Conent = dp.Conent;
                ActivityModels.CreateDate = dp.CreateDate;
                ActivityModels.Id = dp.Id;
                ActivityModels.Longitude = dp.Longitude;
                ActivityModels.Dimension = dp.Dimension;
                List.Add(ActivityModels);
            }
        }

    }

    
    public class ActivityCondition : Condition
    {
        public Guid ActityType { get; set; }

    }
}