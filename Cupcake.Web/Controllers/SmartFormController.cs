using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cupcake.Web.Controllers
{
    public class SmartFormController :  BasePublicController
    {
        private readonly UserService userService;
        public SmartFormController()
        {
            userService = new UserService();
        }
        public ActionResult Index(UserCondition condition)
        {
            var users = userService.SearchUsers(condition);
            var models = new PagedList<UserModel>(users.Select(n => n.ToModel()), users.Paging);

            return View(models);
        }

        public ActionResult Create()
        {
            UserModel model = new UserModel();
            ViewBag.InputTypeHtml = EnumExtensions.GetSelectListHtml(typeof(Cupcake.Core.Domain.InputType), "InputType", "InputType", "", "display:none");
            ViewBag.ExtendedInputTypeHtml = EnumExtensions.GetSelectListHtml(typeof(Cupcake.Core.Domain.ExtendedInputType), "ExtendedInputType", "", "display:none");
            ViewBag.AssociatedObjectHtml = EnumExtensions.GetSelectListHtml(typeof(Cupcake.Core.Domain.AssociatedObject), "AssociatedObject", "", "display:none");
            return View(model);
        }
    }
}