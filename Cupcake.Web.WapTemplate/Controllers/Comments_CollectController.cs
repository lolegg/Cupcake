using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Services;
using Cupcake.Web.WapTemplate.Models;


namespace Cupcake.Web.WapTemplate.Controllers
{
    public class Comments_CollectController : Controller
    {
        //
        // GET: /Comments_Collect/
        public ActionResult Index()
        { 

            //var user = Session["User"] as User;
            //if(user==null){
                ViewBag.UserId = "9393ed42-7b3f-e711-afa3-002564ba5c19";
                ViewBag.Name = "reloadsoft";
                //ViewBag.UserId = user.Id;
                //ViewBag.Name = user.Name;
            //}
           
            return View();
        }
        /// <summary>
        /// 收藏列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HasListComments_CollectJson()
        {
            //var user = Session["User"] as User;
            var uId =Guid.Parse("9393ed42-7b3f-e711-afa3-002564ba5c19");
            ServiceComments_Collect service = new ServiceComments_Collect();
            Comments_CollectModel model = new Comments_CollectModel();
            List<Comments_Collect> list= service.HasListComments_Collect(uId);
            model.ListComments_CollectModel = service.TurnListComments_CollectModel(list);
            return Json(model);
        }
    
        /// <summary>
        /// 批量删除收藏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HasListDelete() {
            string arry = Request.Form["Jsondataid"];
            ServiceComments_Collect service = new ServiceComments_Collect();
            int monitor= service.HasListComments_CollectDelete(arry);
            return Json(monitor);
        }

        public ActionResult aa() {

            return View();
        }
	}
}