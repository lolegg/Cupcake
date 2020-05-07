using Cupcake.Core;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Plugin.MemberDengJi.Models;
using Cupcake.Plugin.MemberDengJi.Services;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.MemberDengJi.Controllers
{
    public class GovernmentPurchasingController : Controller
    {
        private readonly IGovernmentPurchasingService service;

        public GovernmentPurchasingController(IGovernmentPurchasingService governmentpurchasing)
        {
            this.service = governmentpurchasing;
        }
        public ActionResult Index(GovernmentPurchasingCondition condition)
        {
            var news = service.SearchGovernmentPurchasing(condition);
            var models = new PagedList<GovernmentPurchasing>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new GovernmentPurchasingModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }
    }
}
