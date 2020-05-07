using Cupcake.Web.Models;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cupcake.Web.Authorize;

namespace Cupcake.Web.Controllers
{
    /// <summary>
    /// 树模板
    /// </summary>
    public class TreeController : BasePublicController
    {
        [ClientAuthorize(Permissions = "查看")]
        public ActionResult DefaultView(string c)
        {
            ViewBag.c = c;
            return View();
        }


        public ActionResult RoleView(string c, string roleId)
        {
            ViewBag.c = c;
            ViewBag.RoleId = roleId;
            return View();
        }
    }
}
