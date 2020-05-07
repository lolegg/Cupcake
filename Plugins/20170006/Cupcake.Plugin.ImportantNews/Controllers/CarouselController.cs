using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.ImportantNews.Domain;
using Cupcake.Plugin.ImportantNews.Models;
using Cupcake.Plugin.ImportantNews.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;
using Cupcake.Plugin.ImportantNews.Helper;

namespace Cupcake.Plugin.ImportantNews.Controllers
{
    public class CarouselController : Controller
    {
        private readonly ICarouselService service;

        public CarouselController(ICarouselService newsService)
        {
            this.service = newsService;
        }
        public ActionResult Index(CarouselCondition condition)
        {
            var news = service.SearchNews(condition);
            var models = new PagedList<CarouselInfo>(news, news.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CarouselModel model = new CarouselModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(CarouselModel model)
        {
          var lujinID="";
          if (model.DetailsType!=null)
          {
              lujinID = DataDictionaryHelper.GetValue((Guid)model.DetailsType);
          }
         
           
            if (ModelState.IsValid)
            {
                CarouselInfo info = new CarouselInfo();
                info.Id = Guid.NewGuid();
                info.Title = model.Title;
                info.PictureUrl = model.PictureUrl;
                info.PictureJumpPath = model.PictureJumpPath;
                info.CarouselType = model.CarouselType;
                info.DetailsType = model.DetailsType;
                info.LuJing = lujinID + model.PictureJumpPath;
                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }

        public ActionResult Edit(Guid id)
        {
            CarouselModel model = new CarouselModel();
            var info = service.GetById(id);
            model.Title = info.Title;
            model.PictureUrl = info.PictureUrl;
            model.PictureJumpPath = info.PictureJumpPath;
            model.CarouselType = info.CarouselType;
            model.DetailsType = info.DetailsType;
            model.Id = info.Id;
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CarouselModel model)
        {
            var lujinID = "";
            if (model.DetailsType != null)
            {
                lujinID = DataDictionaryHelper.GetValue((Guid)model.DetailsType);
            }
            if (ModelState.IsValid)
            {
                var info = service.GetById(model.Id);
                if (info != null)
                {
                    info.Id = model.Id;
                    info.Title = model.Title;
                    info.PictureUrl = model.PictureUrl;
                    info.PictureJumpPath = model.PictureJumpPath;
                    info.CarouselType = model.CarouselType;
                    info.DetailsType = model.DetailsType;
                    info.LuJing = lujinID + model.PictureJumpPath;
                    service.Update(info);
                }
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            CarouselModel model = new CarouselModel();
            var info = service.GetById(id);
            if (info != null)
            {
                info.IsDelete = true;
                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Delete"), model);
        }
        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
            var model = new CarouselModel().ToModel(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
        }
    }
}
