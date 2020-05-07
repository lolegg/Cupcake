
using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Helper;
using Cupcake.Web.WapTemplate.Models;
using Cupcake.Web.WapTemplate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Cupcake.Web.WapTemplate.Controllers
{
    public class ActivityController : Controller
    {
        //
        // GET: /Activity/
        public PartialViewResult LunBoRight() 
        {
            ServiceTemplate service = new ServiceTemplate();
            var list = service.GetListActivityByTop(5);
            ActivityModelsListModel model = new ActivityModelsListModel(list);
            return PartialView(model);
        }

        public ActionResult Details(Guid id) 
        {
            ServiceTemplate service = new ServiceTemplate();
            var info= service.GetbyActivity(id);
            var model = new ActivityModels().ToModel(info);
            ViewBag.ActivityByid=service.GetListActivityByid(id);
            ViewBag.Baomingmen = service.GetRegistrationCount(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult ActivityRegister(string RegistrationName, string RegistrationTel, string UserId, string Id)
        {
            //测试用户id
            UserId = "16BB93E1-3C85-47C5-A5C6-735A5C213320";
            ServiceTemplate service = new ServiceTemplate();
            var count = service.GetIsRegistration(new Guid(UserId), new Guid(Id));
            if (count > 0)
            {
                return Json(2);
            }
            else
            {
                return Json(service.Add(RegistrationName, RegistrationTel, UserId, Id));
            }
        }

        public PartialViewResult NewActivity() 
        {
            ServiceTemplate service = new ServiceTemplate();
            ActivityModels model = new ActivityModels();
            model.DataDictionary = service.GetActivityTypeList("活动类型");
            var id= model.DataDictionary.FirstOrDefault().Id;
            ViewBag.ActivityBytype = service.GetListActivityBytype(id);
            return PartialView(model);
        }
        [HttpPost]

        public string  NewActivity(string ActivityType)
        {
            StringBuilder str = new StringBuilder();
            ServiceTemplate service = new ServiceTemplate();
            if (ActivityType != "")
            {   
                var list = service.GetListActivityBytype(new Guid(ActivityType));
                foreach (var item in list)
                {
                    str.Append("<li><a href=\"/Activity/Details?Id=" + item.Id + "\">");
                    if (item.Imgurl != null)
                    {
                        string[] imge = item.Imgurl.Split(',');
                        str.Append("<img src=" + imge[0] + " />");
                    }
                    str.Append(" <span>" + item.Title + "</span>");
                    str.Append("</a></li>");
                }
                return str.ToString();
            }
            else 
            {
                return "";
            }
        }

        public ActionResult SeeMore(int pagings = 0)
        {
            ActivityModels model = new ActivityModels();
            ServiceTemplate service = new ServiceTemplate();
            model.Condition.PageSize = 3;
            model.Condition.ActityType = service.GetActivityTypeList("活动类型").Where(n=>n.Name=="公益活动").FirstOrDefault().Id;
            Paging paging = new Paging(model.Condition);
            paging.PageIndex = pagings;
            var activity = service.SearchActivity(model.Condition, ref paging);
            ActivityModelsListModel models = new ActivityModelsListModel(activity);
            models.Paging = paging;
            models.Total = paging.Total.ToString();
            models.Index = paging.PageIndex.ToString();
            models.Size = paging.PageSize.ToString();
            models.DataDictionary = service.GetActivityTypeList("活动类型");
            return View(models);
        }
        public JsonResult FenyeSeeMoreNew(string type = "", int pagings = 0)
        {
            ActivityModels model = new ActivityModels();
            ServiceTemplate service = new ServiceTemplate();
            model.Condition.PageSize = 3;
            model.Condition.ActityType = new Guid(type);
            Paging paging = new Paging(model.Condition);
            paging.PageIndex = pagings;
            var activity = service.SearchActivity(model.Condition, ref paging);
            ActivityModelsListModel models = new ActivityModelsListModel(activity);
            models.Paging = paging;
            models.Total = paging.Total.ToString();
            models.Index = paging.PageIndex.ToString();
            models.Size = paging.PageSize.ToString();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        ///查看更多 切换显示Ajax
        public JsonResult SeeAjaxIndex(string ActivityType, int pagings = 0)
        {
            ServiceTemplate service = new ServiceTemplate();
            ActivityModels model = new ActivityModels();
            model.Condition.PageSize = 3;
            model.Condition.ActityType = new Guid(ActivityType);
            Paging paging = new Paging(model.Condition);
            var ser = service.SearchActivity(model.Condition, ref paging);
            ActivityModelsListModel models = new ActivityModelsListModel(ser);
            models.Paging = paging;
            models.Total = paging.Total.ToString();
            models.Index = paging.PageIndex.ToString();
            models.Size = paging.PageSize.ToString();
            return Json(models,JsonRequestBehavior.AllowGet);
            
        }

        public PartialViewResult ActivityStyle()
        {
            ActivityModels model = new ActivityModels();
            ServiceTemplate service = new ServiceTemplate();
            model.ActivityStyle=service.GetActivityStylelist();
            return PartialView(model);
        }

        public ActionResult ActivityStyleDetails(Guid id)
        {
            ServiceTemplate service = new ServiceTemplate();
            var info = service.GetbyActivityStyle(id);
            var model = new ActivityStyleModel().ToModel(info);
            ViewBag.count=service.GetByMessageManagements(id);
            //ViewBag.MessageManagements = service.GetMessageManagementsList(id);
            return View(model);
        
        }

        //留言评价
        public ActionResult Message(string ActivityNmae, string Text, string Id, string UserId)
        {
            //if (!String.IsNullOrEmpty(SessionHelper.userid))
            //{
                UserId = "16BB93E1-3C85-47C5-A5C6-735A5C213320";
                ServiceTemplate service = new ServiceTemplate();
                return Json(service.AddMessage(ActivityNmae, Text, UserId, Id));
            //}
            //return Json(0);
        }

        public PartialViewResult ActivityMap()
        {
            return PartialView();
        }

        public ActionResult ActivityMapDetails(Guid? id)
        {
            ServiceTemplate service = new ServiceTemplate();
            var list = service.GetListActivity(id);
            ActivityModelsListModel model = new ActivityModelsListModel(list);
            model.DataDictionary = service.GetActivityTypeList("活动类型");
            return View(model);
        }

        public JsonResult ActivityMapList(string lng, string lat, string Type)
        {
            ServiceTemplate service = new ServiceTemplate();
            var list= service.GetListActivityMap(lng, lat, Type);
            ActivityModelsListModel model = new ActivityModelsListModel(list);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public string GetDataDictionaryUrl(string id)
        //{
        //    var Url = DataDictionaryHelper.GetDataDictionaryVal(new Guid(id)).Tips;
        //    return Url;
        //}
	}
}