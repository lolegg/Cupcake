using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    /// <summary>
    /// 数据选择模板
    /// </summary>
    public class DataSelectController : BasePublicController
    {
        public ActionResult SimpleDataSelectModal()
        {
            return View();
        }
    }
}
