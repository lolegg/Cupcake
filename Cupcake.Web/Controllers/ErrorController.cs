using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Common
        public ActionResult PageNotFound()
        {
            //this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }
        public ActionResult ErrorPage(string message)
        {
            ViewBag.Message = message == null ? "" : message;
            return View();
        }

        public ActionResult ServerError(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult NoPermission(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}