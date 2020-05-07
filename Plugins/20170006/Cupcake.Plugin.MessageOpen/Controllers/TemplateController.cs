using Cupcake.Core;
using Cupcake.Plugin.Template.Domain;
using Cupcake.Plugin.Template.Services;
using System.Web.Mvc;

namespace Cupcake.Plugin.Template.Controllers
{
    public class TemplateController : Controller
    {
        private readonly INewsService service;

        public TemplateController(INewsService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(NewsCondition condition)
        {
            var news = service.SearchNews(condition);
            var models = new PagedList<News>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
    }
}
