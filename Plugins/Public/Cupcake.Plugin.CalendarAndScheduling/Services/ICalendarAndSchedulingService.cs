using Cupcake.Core;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Cupcake.Plugin.CalendarAndScheduling.Services
{
    public interface ICalendarAndSchedulingService : IpluginBaseService<Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo>
    {
        Cupcake.Core.IPagedList<Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo> SearchNews(Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingCondition condition);
        IList<Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo> SearchAllScheduling(Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingCondition condition);

        //void Add(Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo info);
        //void Update(Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo info);

        //Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo GetById(Guid Id);
        //void Delete(Cupcake.Plugin.CalendarAndScheduling.Domain.CalendarAndSchedulingInfo info); 
    }
}
