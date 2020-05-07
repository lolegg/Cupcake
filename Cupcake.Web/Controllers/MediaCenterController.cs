using Cupcake.Web.Models;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cupcake.Core;
using Cupcake.Web.Authorize;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Controllers
{
    public class MediaCenterController : BasePublicController
    {
        private readonly MediaService mediaService;
        public MediaCenterController()
        {
            mediaService = new MediaService();
        }

        public ActionResult Index(MediaCondition condition, string selectedMediaIds)
        {
            var entities = mediaService.SearchMedias(condition);
            var models = new PagedList<Media>(entities, entities.Paging);
            ViewBag.SelectedMediaIds = selectedMediaIds;
            return View(models);
        }
    }
}
