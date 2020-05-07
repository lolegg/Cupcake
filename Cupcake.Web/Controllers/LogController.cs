using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class LogController : BasePublicController
    {
        private readonly LogService logService;
        public LogController()
        {
            logService = new LogService();
        }
        public ActionResult Index(LogCondition condition)
        {
            var logs = logService.SearchLogs(condition);
            var models = new PagedList<LogModel>(logs.Select(n => n.ToModel()), logs.Paging);

            return View(models);
        }

        public ActionResult Details(Guid id)
        {
            var log = logService.Get(id);
            if (log == null)
            {
                throw new NopException("日志不存在");
            }
            return View(log);
        }

        public ActionResult Delete(Guid id)
        {
            var log = logService.Get(id);
            if (log == null)
            {
                throw new NopException("日志不存在");
            }
            logService.Remove(log);
            return Json(new AjaxResult() { Result = Result.Success });
        }
    }
}
