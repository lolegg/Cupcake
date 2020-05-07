using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly MapService service;
        public MapController()
        {
            service = new MapService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Guid? parentId, Guid? childrenId, string arrange)
        {
            MapModel model = new MapModel();
            ViewBag.ParentId = parentId;
            ViewBag.Arrange = arrange ?? "";

            var mapOverlayPoints = new List<MapOverlayPoints>();
            var listPoints = new List<MapOverlayPointsModel>();

            if (parentId != null)
            {
                if (childrenId != null)
                {
                    switch (arrange)
                    {
                        case "allchildren":
                            mapOverlayPoints = service.GetSelfChildrenMapOverlayPoints(childrenId.GetValueOrDefault());
                            break;
                        default:
                            mapOverlayPoints = service.GetMapOverlayPoints(childrenId.GetValueOrDefault());
                            break;
                    }
                }
                else
                {
                    if (arrange == "all")
                    {
                        mapOverlayPoints = service.GetAllMapOverlayPoints();
                    }
                    else
                    {
                        mapOverlayPoints = service.GetChildrenMapOverlayPoints(parentId.GetValueOrDefault());
                    }
                }
                //center
                var entity = service.Get(parentId.GetValueOrDefault());
                model.lng = entity.Longitude;
                model.lat = entity.Latitude;

                foreach (var item in mapOverlayPoints)
                {
                    listPoints.Add(new MapOverlayPointsModel() { Name = entity.Name, Color = entity.Color, MapOverlayId = item.MapOverlayId, Longitude = item.Longitude, Latitude = item.Latitude });
                }
            }

            model.listMap = listPoints;
            return View(model);
        }
        public ActionResult CheckName(string name, Guid parentId)
        {
            if (service.CheckName(name, parentId))
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }
        public ActionResult SaveOverlays(string name, Guid? parentId, string points, string color, string xy, Guid? childrenId)
        {
            Guid mainId = childrenId ?? parentId ?? Guid.Empty;
            MapOverlay parentMapOverlay = service.Get(mainId);
            if (parentMapOverlay == null)
            {
                throw new NopException("父级遮盖物不存在");
            }

            MapOverlay entity = new MapOverlay() { Name = name, ParentId = parentMapOverlay.Id, Level = parentMapOverlay.Level + 1, Color = color };
            if (!string.IsNullOrEmpty(xy))
            {
                entity.Longitude = xy.Split(',')[0];
                entity.Latitude = xy.Split(',')[1];
            }
            service.Add(entity);

            if (points != null)
            {
                var coordinates = points.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var pointsEntitys = new List<MapOverlayPoints>();
                foreach (var coordinate in coordinates)
                {
                    var tmp = coordinate.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var point = new MapOverlayPoints() { Longitude = tmp[0], Latitude = tmp[1], MapOverlayId = entity.Id };
                    pointsEntitys.Add(point);
                }
                service.BatchAdd(pointsEntitys);
            }

            return Content("1");
        }
        public ActionResult UpdateOverlays(string name, string points, string color, string xy, Guid id, bool isUpdatePoints)
        {
            MapOverlay mapOverlay = service.Get(id);
            if (mapOverlay == null)
            {
                throw new NopException("遮盖物不存在");
            }
            mapOverlay.Name = name;
            mapOverlay.Color = color;
            if (!string.IsNullOrEmpty(xy))
            {
                mapOverlay.Longitude = xy.Split(',')[0];
                mapOverlay.Latitude = xy.Split(',')[1];
            }
            service.Modify(mapOverlay);

            if (isUpdatePoints)
            {
                if (points != null)
                {
                    var coordinates = points.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    var pointsEntitys = new List<MapOverlayPoints>();
                    foreach (var coordinate in coordinates)
                    {
                        var tmp = coordinate.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var point = new MapOverlayPoints() { Longitude = tmp[0], Latitude = tmp[1], MapOverlayId = mapOverlay.Id };
                        pointsEntitys.Add(point);
                    }
                    service.DeleteMapOverlayPoints(id);
                    service.BatchAdd(pointsEntitys);
                }
            }


            return Content("1");
        }
        public ActionResult MapIndex()
        {
            return View();
        }
        public ActionResult Edit(Guid? parentId, Guid? childrenId, string all)
        {
            MapModel model = new MapModel();
            ViewBag.ParentId = parentId;

            var mapOverlayPoints = new List<MapOverlayPoints>();
            var listPoints = new List<MapOverlayPointsModel>();
            if (string.IsNullOrEmpty(all))
            {
                MapOverlay entity = null;
                if (parentId != null)
                {
                    if (childrenId != null)
                    {
                        mapOverlayPoints = service.GetMapOverlayPoints(childrenId.GetValueOrDefault());
                        entity = service.Get(childrenId.GetValueOrDefault());
                    }
                    else
                    {
                        mapOverlayPoints = service.GetChildrenMapOverlayPoints(parentId.GetValueOrDefault());
                        entity = service.Get(parentId.GetValueOrDefault());
                    }
                    //to viewModel
                    model.Id = entity.Id;
                    model.Name = entity.Name;
                    model.Level = entity.Level;
                    model.Color = entity.Color;

                    if (string.IsNullOrEmpty(entity.Longitude) || string.IsNullOrEmpty(entity.Latitude))
                    {
                        model.Coordinate = "";
                        var parentEntity = service.Get(parentId.GetValueOrDefault());
                        model.lng = parentEntity.Longitude;
                        model.lat = parentEntity.Latitude;
                    }
                    else
                    {
                        model.Coordinate = entity.Longitude + "," + entity.Latitude;
                        model.lng = entity.Longitude;
                        model.lat = entity.Latitude;
                    }

                    foreach (var item in mapOverlayPoints)
                    {
                        listPoints.Add(new MapOverlayPointsModel() { Name = entity.Name, Color = entity.Color, MapOverlayId = item.MapOverlayId, Longitude = item.Longitude, Latitude = item.Latitude });
                    }
                }
            }
            else
            {
                mapOverlayPoints = service.GetAllMapOverlayPoints();
                //center
                var entity = service.Get(parentId.GetValueOrDefault());
                model.lng = entity.Longitude;
                model.lat = entity.Latitude;

                foreach (var item in mapOverlayPoints)
                {
                    listPoints.Add(new MapOverlayPointsModel() { Name = entity.Name, Color = entity.Color, MapOverlayId = item.MapOverlayId, Longitude = item.Longitude, Latitude = item.Latitude });
                }
            }

            model.listMap = listPoints;
            return View(model);
        }
        public ActionResult Delete(Guid id)
        {
            if (service.HasChildren(id))
            {
                return Content("0");
            }
            else
            {
                service.DeleteMapOverlayPoints(id);
                var entity = service.Get(id);
                service.Remove(entity);
                return Content("1");
            }
        }
    }
}