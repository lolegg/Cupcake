using Cupcake.Core;
using Cupcake.Plugin.MemberDengJi.Domain;
using Cupcake.Plugin.MemberDengJi.Models;
using Cupcake.Plugin.MemberDengJi.Services;
using System;
using System.Web.Mvc;

namespace Cupcake.Plugin.MemberDengJi.Controllers
{
    public class OrganizationalChangeController : Controller
    {
        private readonly IOrganizationalChangeService service;

        public OrganizationalChangeController(IOrganizationalChangeService organizationalchangeService)
        {
            this.service = organizationalchangeService;
        }
        public ActionResult Index(OrganizationalChangeCondition condition)
        {
            var news = service.SearchOrganizationalChange(condition);
            var models = new PagedList<OrganizationalChange>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new OrganizationalChangeModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }
    }
}
