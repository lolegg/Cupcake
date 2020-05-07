using Cupcake.Web.WapTemplate.Models;
using Cupcake.Web.WapTemplate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.WapTemplate.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        public PartialViewResult Index()
        {
            NewsService service = new NewsService();
            NewsModels model = new NewsModels();
            ViewBag.Newslist = service.GetNewslist();
            return PartialView();
        }

        public ActionResult Details(Guid id)
        {
            NewsService service = new NewsService();
            var info = service.GetbyNews(id);
            var model = new NewsModels().ToModel(info);
            return View(model);
        }

        public ActionResult SeeMore(int pagings = 0)
        {
            NewsModels model = new NewsModels();
            NewsService service = new NewsService();
            model.Condition.PageSize = 3;
            model.Condition.NewsType = service.GetActivityTypeList("新闻类型").Where(n => n.Name == "要点新闻").FirstOrDefault().Id;
            Paging paging = new Paging(model.Condition);
            paging.PageIndex = pagings;
            var news = service.SearchNews(model.Condition, ref paging);
            NewsModelsListModel models = new NewsModelsListModel(news);
            models.Paging = paging;
            models.Total = paging.Total.ToString();
            models.Index = paging.PageIndex.ToString();
            models.Size = paging.PageSize.ToString();
            models.DataDictionary = service.GetActivityTypeList("新闻类型");
            return View(models);
        }

        public JsonResult FenyeSeeMoreNew(string type = "", int pagings = 0)
        {
            NewsModels model = new NewsModels();
            NewsService service = new NewsService();
            model.Condition.PageSize = 3;
            model.Condition.NewsType = new Guid(type);
            Paging paging = new Paging(model.Condition);
            paging.PageIndex = pagings;
            var news = service.SearchNews(model.Condition, ref paging);
            NewsModelsListModel models = new NewsModelsListModel(news);
            models.Paging = paging;
            models.Total = paging.Total.ToString();
            models.Index = paging.PageIndex.ToString();
            models.Size = paging.PageSize.ToString();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        ///查看更多 切换显示Ajax
        public JsonResult SeeAjaxIndex(string NewsType, int pagings = 0)
        {
            NewsModels model = new NewsModels();
            NewsService service = new NewsService();
            model.Condition.PageSize = 3;
            model.Condition.NewsType = new Guid(NewsType);
            Paging paging = new Paging(model.Condition);
            var ser = service.SearchNews(model.Condition, ref paging);
            NewsModelsListModel models = new NewsModelsListModel(ser);
            models.Paging = paging;
            models.Total = paging.Total.ToString();
            models.Index = paging.PageIndex.ToString();
            models.Size = paging.PageSize.ToString();
            return Json(models, JsonRequestBehavior.AllowGet);

        }
	}
}