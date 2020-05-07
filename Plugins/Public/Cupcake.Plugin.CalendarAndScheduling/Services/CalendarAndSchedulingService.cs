using Cupcake.Core;
using Cupcake.Data;
using Cupcake.Plugin.CalendarAndScheduling.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cupcake.Plugin.CalendarAndScheduling.Services
{
    public partial class CalendarAndSchedulingService :PluginBaseService<CalendarAndSchedulingInfo>,ICalendarAndSchedulingService
    {
        public CalendarAndSchedulingService(IRepository<CalendarAndSchedulingInfo> repository):base(repository)
        {
         
        }
        public IPagedList<CalendarAndSchedulingInfo> SearchNews(CalendarAndSchedulingCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.ImportantLevel != null)
                query = query.Where(t => t.ImportantLevel == condition.ImportantLevel);
            if (condition.DateT.HasValue)
            {
                int month = Convert.ToDateTime(condition.DateT).Month;
                query = query.Where(t => t.month == month);
            }


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<CalendarAndSchedulingInfo>(query, condition.PageIndex, condition.PageSize);
        }
        public IList<CalendarAndSchedulingInfo> SearchAllScheduling(CalendarAndSchedulingCondition condition)
        {
            var query = repository.Table;

            if (!string.IsNullOrEmpty(condition.Title))
                query = query.Where(t => t.Title.Contains(condition.Title));
            if (condition.ImportantLevel != null)
                query = query.Where(t => t.ImportantLevel == condition.ImportantLevel);
            if (condition.DateT.HasValue)
            {
                int month = Convert.ToDateTime(condition.DateT).Month;
                query = query.Where(t => t.month == month);
            }


            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<CalendarAndSchedulingInfo>(query, condition.PageIndex, condition.PageSize);
        }

        //public void Add(CalendarAndSchedulingInfo info)
        //{
        //    var query = repository.Table;
        //    repository.Insert(info);
      
        //}
        //public void Update(CalendarAndSchedulingInfo info)
        //{
        //    var query = repository.Table;
        //    repository.Update(info);
          
        //}

        //public CalendarAndSchedulingInfo GetById(Guid Id)
        //{
        //    var query = repository.GetById(Id);

        //    return query;

        
        //}

        //public void Delete(CalendarAndSchedulingInfo info)
        //{


        //    repository.Delete(info);
        //}
       

    }
}
