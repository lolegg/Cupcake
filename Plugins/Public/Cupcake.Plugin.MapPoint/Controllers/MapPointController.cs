using AutoMapper;
using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.MapPoint.Domain;
using Cupcake.Plugin.MapPoint.Models;
using Cupcake.Plugin.MapPoint.Services;
using Cupcake.Web.Framework;
using System;
using System.Web.Mvc;
using CPMD = Cupcake.Plugin.MapPoint.Domain;
using System.Linq;
using Cupcake.Services;

namespace Cupcake.Plugin.MapPoint.Controllers
{
    public class MapPointController : Controller
    {
        private readonly IMapPointService mapPointService;
        private readonly IMapService mapService;

        public MapPointController(IMapPointService mapPointService, IMapService mapService)
        {
            this.mapPointService = mapPointService;
            this.mapService = mapService;
        }
        public ActionResult Index(MapCondition condition)
        {
            var entitys = mapService.Search(condition);
            var models = new PagedList<MapModel>(entitys.Select(n => n.ToModel()), entitys.Paging);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new MapModel();
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        [ValidateInput(false)]
        public ActionResult Create(MapModel model)
        {
            if (ModelState.IsValid)
            {
                if (mapService.AlreadyExists(model.Name, Guid.Empty))
                {
                    ModelState.AddModelError("Name", "名称重复");
                }
                else if (model.ImageIds.Count != 1)
                {
                    ModelState.AddModelError("ImageIds", "只能选择一张图片");
                }
                else
                {
                    var entity = model.ToEntity();
                    mapService.Add(entity);
                    MediaHelper.SaveHasMedias(entity.Id, model.ImageIds);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);
        }
        //public ActionResult Details(Guid id)
        //{
        //    var user = mapService.Get(id);
        //    var model = Mapper.Map<MapInformationModel>(user);
        //    return View(model);
        //}


        //public ActionResult Create(int? menuid)
        //{
        //    MapInformationModel model = new MapInformationModel();

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Create(MapInformationModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        mapInformationService.Add(model.ToInfo());
        //        return Json(new AjaxResult() { Result = Result.Success, Message = "成功添加!" });

        //    }
        //    return View(model);
        //}

        //public ActionResult Modify(Guid id)
        //{
        //    var user = mapInformationService.Get(id);
        //    var model = Mapper.Map<MapInformationModel>(user);
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Modify(MapInformationModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MapInformationService service = new MapInformationService();
        //        var m = mapInformationService.Get(model.Id);
        //        Mapper.Map(model, m);
        //        service.Modify(m);
        //        return Json(new AjaxResult() { Result = Result.Success, Message = "" });

        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Delete(Guid id)
        //{
        //    var Introduce = mapInformationService.Get(id);
        //    Introduce.IsDelete = true;
        //    mapInformationService.Modify(Introduce);
        //    return Json(new AjaxResult() { Result = Result.Success, Message = "删除成功！" });
        //}

        //public ActionResult Coordinate(Guid Id)
        //{
        //    //MapCoordinateService mservice = new MapCoordinateService();
        //    var user = mapInformationService.Get(Id);
        //    var model = Mapper.Map<MapInformationModel>(user);
        //    //model.MapCoordinateListModel = mservice.GetMapCoordinateListById(Id).ToList();
        //    return View(model);
        //}
        //public JsonResult CoordinateList(Guid Id)
        //{
        //    //MapInformationService service = new MapInformationService();
        //    //var user = service.Get(Id);
        //    //var model = Mapper.Map<MapInformationModel>(user);
        //    MapInformationModel model = new MapInformationModel();
        //    model.MapCoordinateListModel = mapCoordinateService.GetMapCoordinateListById(Id).ToList();
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddCoordinate(Guid Id, string name, string adderss, string type, string imageurl)
        //{
        //    MapCoordinateModel model = new MapCoordinateModel();
        //    model.n = "0";
        //    MapCoordinateService mservice = new MapCoordinateService();
        //    try
        //    {
        //        MapCoordinate info = new MapCoordinate();
        //        info.MapId = Id.ToString();
        //        info.CoordinateAddress = adderss;
        //        info.CoordinateName = name;
        //        info.CoordinateX = "0";
        //        info.CoordinateY = "0";
        //        info.Id = Guid.NewGuid();
        //        if (type == "1")
        //            info.CoordinateImageUrl = "/Content/img/dangzhibu.png";
        //        else if (type == "2")
        //            info.CoordinateImageUrl = "/Content/img/liangxin.png";
        //        else if (type == "3")
        //            info.CoordinateImageUrl = "/Content/img/quyuhua.png";
        //        else if (type == "4")
        //            info.CoordinateImageUrl = "/Content/img/erzhibu.png";
        //        else if (type == "5")
        //            info.CoordinateImageUrl = "/Content/img/loudao.png";
        //        else if (type == "6")
        //            info.CoordinateImageUrl = "/Content/img/yangtai.png";
        //        else if (type == "7")
        //            info.CoordinateImageUrl = "/Content/img/zizhi.png";
        //        else
        //            info.CoordinateImageUrl = imageurl;
        //        mservice.Add(info);
        //        model.Id = info.Id;
        //        model.CoordinateName = info.CoordinateName;
        //        model.CoordinateAddress = info.CoordinateAddress;
        //        model.CoordinateImageUrl = info.CoordinateImageUrl;
        //        model.CoordinateX = info.CoordinateX;
        //        model.CoordinateY = info.CoordinateY;
        //        model.n = "1";
        //    }
        //    catch
        //    {


        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult Add(string Id, string List)
        //{
        //    MapCoordinateService mservice = new MapCoordinateService();
        //    JavaScriptSerializer Serializer = new JavaScriptSerializer();
        //    List<dian> objs = Serializer.Deserialize<List<dian>>(List);
        //    string tt = "0";
        //    if (List != "[]")
        //    {
        //        //mservice.DeleteByMapId(Guid.Parse(Id));
        //        foreach (var item in objs)
        //        {
        //            var mp = mservice.Get(Guid.Parse(item.id));
        //            mp.CoordinateX = item.X;
        //            mp.CoordinateY = item.Y;
        //            mservice.Modify(mp);
        //        }
        //        tt = "1";
        //    }
        //    return Json(tt, JsonRequestBehavior.AllowGet);
        //}
        //public class dian
        //{
        //    public string id { get; set; }
        //    public string X { get; set; }
        //    public string Y { get; set; }

        //}

        //public JsonResult GetCordinateDetail(Guid Id)
        //{
        //    MapCoordinateService mservice = new MapCoordinateService();
        //    var coodinate = mservice.Get(Id);
        //    MapCoordinateModel model = new MapCoordinateModel();
        //    model.CoordinateName = coodinate.CoordinateName;
        //    model.Id = coodinate.Id;
        //    model.CoordinateAddress = coodinate.CoordinateAddress;
        //    model.CoordinateX = coodinate.CoordinateX;
        //    model.CoordinateY = coodinate.CoordinateY;
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult DeleteCoordinate(Guid Id)
        //{
        //    MapCoordinateService mservice = new MapCoordinateService();
        //    string cc = "0";
        //    try
        //    {
        //        var coodinate = mservice.Get(Id);
        //        mservice.Remove(coodinate);
        //        cc = "1";
        //    }
        //    catch
        //    {
        //        Exception e;
        //    }
        //    return Json(cc, JsonRequestBehavior.AllowGet);
        //}
    }
}
