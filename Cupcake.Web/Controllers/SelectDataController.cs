using Cupcake.Core;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class SelectDataController : BasePublicController
    {
        public ActionResult Index(string controller, int maxNumber = 0, bool multipleChoice = false)
        {
            if (string.IsNullOrEmpty(controller))
            {
                throw new NopException("控制器名字不能为空");
            }
            ViewBag.Controller = controller;
            ViewBag.MaxNumber = maxNumber.ToString();
            ViewBag.MultipleChoice = multipleChoice;
            return View();
        }
    }
}
