using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.NetWork.Domain;
using Cupcake.Plugin.NetWork.Models;
using Cupcake.Plugin.NetWork.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.NetWork.Controllers
{
    public class ErrorReportingController : Controller
    {
        private readonly IErrorReportingService service;

        public ErrorReportingController(IErrorReportingService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(ErrorReportingCondition condition)
        {
            var news = service.SearchErrorReporting(condition);
            var models = new PagedList<ErrorReporting>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new ErrorReportingModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            ErrorReportingModel model = new ErrorReportingModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.UpdateGoogleProductRecord(info);
                return Json(new AjaxResult() { Result = Result.Success, Message = "²Ù×÷³É¹¦" });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }


    }
}
