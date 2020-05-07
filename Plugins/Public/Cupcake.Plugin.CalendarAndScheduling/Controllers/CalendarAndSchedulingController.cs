using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.CalendarAndScheduling.Domain;
using Cupcake.Plugin.CalendarAndScheduling.Models;
using Cupcake.Plugin.CalendarAndScheduling.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.CalendarAndScheduling.Controllers
{
    public class CalendarAndSchedulingController : Controller
    {
        private readonly ICalendarAndSchedulingService service;

        public CalendarAndSchedulingController(ICalendarAndSchedulingService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(CalendarAndSchedulingCondition condition)
        {
            if (!condition.DateT.HasValue)
            {
                condition.DateT = DateTime.Now;
            }
            var news = service.SearchAllScheduling(condition);
            var models =new CalendarAndSchedulingListModel(news);

            models.DateT = condition.DateT;
            models.Title = condition.Title;
            models.ImportantLevel = condition.ImportantLevel;
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        [HttpGet]
        public ActionResult Create(DateTime DateT,int day=1) {

            CalendarAndSchedulingModel model = new CalendarAndSchedulingModel();
            model.DateT =new DateTime(DateT.Year,DateT.Month,day);
            model.dayNum = day;

            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
            
        
        }
        [HttpPost]
        public ActionResult Create(CalendarAndSchedulingModel model)
        {
              if (ModelState.IsValid)
            {
        var user= Session["User"] as User;
        CalendarAndSchedulingInfo info = new CalendarAndSchedulingInfo();
        info.Id = Guid.NewGuid();
        info.dayNum = model.dayNum;
        info.month = Convert.ToDateTime(model.DateT).Month;
        info.Title = model.Title;
        info.DateT = model.DateT;
        info.Content = model.Content;
        info.ImportantLevel = model.ImportantLevel;
        info.UserId = user.Id;
        service.Add(info);
            }
              return Json(new AjaxResult() { Result = Result.Success });
              
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            CalendarAndSchedulingModel model = new CalendarAndSchedulingModel();
           var info= service.GetById(id);


           model.Title = info.Title;
           model.ImportantLevel = info.ImportantLevel;
           model.Content = info.Content;
           model.DateT = info.DateT;
           model.dayNum = info.dayNum;
           model.month = info.month;
           model.UserId = info.UserId;
           model.Id = info.Id;

            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [HttpPost]
        public ActionResult Edit(CalendarAndSchedulingModel model)
        {
              if (ModelState.IsValid)
            {
        var user= Session["User"] as User;
        var info = service.GetById(model.Id);
        if (info != null)
        {
           
            info.dayNum = model.dayNum;
            info.month = Convert.ToDateTime(model.DateT).Month;
            info.Title = model.Title;
            info.DateT = model.DateT;
            info.Content = model.Content;
            info.ImportantLevel = model.ImportantLevel;
            info.UserId = user.Id;
            service.Update(info);
        }
        return Json(new AjaxResult() { Result = Result.Success });
            }
              return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            CalendarAndSchedulingModel model = new CalendarAndSchedulingModel();
            var info = service.GetById(id);

            model.ImportantLevel = info.ImportantLevel;
            model.Content = info.Content;
            model.DateT = info.DateT;
            model.dayNum = info.dayNum;
            model.month = info.month;
            model.UserId = info.UserId;

            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            CalendarAndSchedulingModel model = new CalendarAndSchedulingModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View();


        }
    }
}
