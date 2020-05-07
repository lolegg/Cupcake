using Cupcake.Core.Model;
using Cupcake.Plugin.CalendarAndScheduling.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.CalendarAndScheduling.Models
{
    public class CalendarAndSchedulingModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "日期时间")]
        public DateTime? DateT { get; set; }
        [Display(Name = "重要程度")]
        public Guid ImportantLevel { get; set; }

        public int dayNum { get; set; }

        public int month { get; set; }

        public Guid UserId { get; set; }

    }

    public class CalendarAndSchedulingListModel : ListModel<CalendarAndSchedulingModel>
    {
        public string Title { get; set; }

        public DateTime? DateT { get; set; }

        public Guid? ImportantLevel { get; set; }

        public CalendarAndSchedulingListModel(IList<CalendarAndSchedulingInfo> CalendarAndSchedulingItemList)
        {
            List = new List<CalendarAndSchedulingModel>();
          
            foreach (var item in CalendarAndSchedulingItemList)
            {
                CalendarAndSchedulingModel SchedulingModel = new CalendarAndSchedulingModel();
                SchedulingModel.Content = item.Content;
                SchedulingModel.DateT = item.DateT;
                SchedulingModel.dayNum = item.dayNum;
                SchedulingModel.ImportantLevel = item.ImportantLevel;
                SchedulingModel.month = item.month;
                SchedulingModel.Title = item.Title;
                SchedulingModel.Id = item.Id;


                List.Add(SchedulingModel);
            }
     
        }
    
    }
}
