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

namespace Cupcake.Web.Controllers
{
    public class MenuController : BaseTreeController<MenuCondition>
    {
        private readonly MenuService menuService;
        public MenuController()
        {
            menuService = new MenuService();
        }
        public ActionResult Index(MenuCondition condtion)
        {
            MenuListModel model = null;
            MenuService service = new MenuService();
            if (Request["ParentMenu"] != null && !Request["ParentMenu"].Equals("0"))
            {
                condtion.ParentMenuID = Request["ParentMenu"];
            }
            if (!string.IsNullOrEmpty(Request["Name"]))
            {
                condtion.MenuName = Request["Name"]; ;
            }
            Paging paging = new Paging(condtion);
            var list = service.GetListByCondition(condtion, ref paging);
            model = new MenuListModel(list);
            model.ParentMenu = condtion.ParentMenuID;
            model.Paging = paging;
            ViewBag.condtion = condtion;
            return View(model);
        }

        //public JsonResult MenuTree()
        //{
        //    MenuService service = new MenuService();
        //    var menus = service.GetAll().ToList();
        //    var root = menus.FirstOrDefault(m => m.ParentId == null);
        //    List<NodeModel> list = new List<NodeModel>();
        //    NodeModel rootNode = new NodeModel()
        //    {
        //        text = root.Name,
        //        id = root.Id.ToString()
        //    };
        //    CreateTreeView(menus, rootNode);
        //    list.Add(rootNode);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [ChildActionOnly]
        public ActionResult Initialize()
        {
            
            List<Menu> menus = new List<Menu>();
            if (IsSuper)
            {
                menus = menuService.GetAllMenus();
            }
            else
            {
                List<Role> listRole = CurrentRoles;
                menus = menuService.GetListByRoles(listRole);
            }
            if (menus == null || menus.Count == 0)
            {
                return PartialView("NoMenus");
            }
            var root = menus.FirstOrDefault(m => m.ParentId == null);
            MenuTreeModel menuTreeModel = new MenuTreeModel()
            {
                Id = root.Id,
                Name = root.Name,
                Href = root.Href,
                IconClass = root.ClassName
            };
            CreateTreeView(menus, menuTreeModel);
            return PartialView(menuTreeModel);
        }

        private void CreateTreeView(IEnumerable<Menu> menus, MenuTreeModel menuTreeModel)
        {
            var children = menus.Where(m => m.ParentId.GetValueOrDefault() == menuTreeModel.Id);
            if (children.Count() == 0)
            {
                menuTreeModel.Children = null;
            }


            string lastMenu = GetLastMenuState("om");
            string lastAvtive = GetLastMenuState("active");

            foreach (var item in children.OrderBy(c=>c.Sort))
            {
                MenuTreeModel child = new MenuTreeModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Href = item.Href,
                    IconClass = item.ClassName
                };
                if (item.Id.ToString() == lastMenu)
                { child.OpenClass = "open"; }
                if (item.Id.ToString() == lastAvtive)
                { child.ActiveClass = "active"; }
                menuTreeModel.Children.Add(child);
                CreateTreeView(menus, child);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var menu = menuService.Get(id);
            if (menu == null || menu.IsDelete == true)
            {
                throw new NopException("菜单不存在");
            }
            var model = menu.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MenuModel model)
        {
            if (ModelState.IsValid)
            {
                if (menuService.AlreadyExists(model.Name, model.ParentId, model.Id))
                {
                    ModelState.AddModelError("Name", "菜单名重复");
                }
                else
                {
                    var menu = menuService.Get(model.Id);
                    //忽略更新
                    model.CreateDate = menu.CreateDate;

                    menu = model.ToEntity(menu);
                    menuService.Modify(menu);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            try
            {
                MenuService service = new MenuService();
                var menu = service.Get(id);
                menu.UpdateDate = DateTime.Now;
                menu.IsDelete = true;
                service.Modify(menu);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            catch
            {
                return Json(new AjaxResult() { Result = Result.Error });
            }
        }

        private string GetLastMenuState(string name)
        {
            string lastMenu = Request[name];
            if (!string.IsNullOrEmpty(lastMenu))
            {
                Response.Cookies.Add(new HttpCookie(name, lastMenu));
            }
            else
            {
                HttpCookie cookie = Request.Cookies.Get(name);
                if (cookie != null)
                {
                    lastMenu = cookie.Value;
                }

            }
            return lastMenu;
        }


        public override JsonResult InsertTree()
        {
            Guid parentId = Guid.Empty;
            if (!Guid.TryParse(Request["pid"].ToString(), out parentId)
                || Request["name"] == null)
            {
                throw new NopException("参数不合法");
            }
            string name = Request["name"].ToString();
            if (menuService.AlreadyExists(name, parentId, Guid.Empty))
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = "菜单名重复" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var menu = new Menu() { Name = name, ParentId = parentId };
                menuService.Add(menu);
                return Json(new TreeAjaxResult() { Result = Result.Success, CurrentNodeId = menu.Id.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public override JsonResult EditTree()
        {
            throw new NotImplementedException();
        }

        public override JsonResult DeleteTree()
        {
            throw new NotImplementedException();
        }

        public override JsonResult CheckTree()
        {
            throw new NotImplementedException();
        }

        public override JsonResult LoadTree()
        {
            var treeNodes = new List<TreeNode>();
            var menus = menuService.GetAllMenusForTree();
            foreach (dynamic menu in menus)
            {
                var treeNode = new TreeNode()
                {
                    showEdit = false,
                    showRemove = false
                };
                treeNode.id = menu.Id.ToString();
                treeNode.parentid = menu.ParentId == null ? "0" : menu.ParentId.ToString();
                treeNode.name = menu.Name;
                treeNode.isParent = menu.ChildrenNumber > 0 ? "true" : "false";
                treeNodes.Add(treeNode);
            }
            return Json(treeNodes, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult NodeView(MenuCondition condition)
        {
            var menus = menuService.SearchMenus(condition);
            var models = new PagedList<Menu>(menus, menus.Paging);

            ViewBag.MenuId = condition.NodeId.ToString();
            return View(models);
        }

        public override JsonResult GetTreeSetting()
        {
            TreeSetting treeSetting = new TreeSetting();
            treeSetting.ShowDelete = false;
            treeSetting.ShowCheck = false;
            //addHoverDom总开关, ShowEdit和ShowDelete无效果, 需要在TreeNode单独设置
            treeSetting.ShowAdd = true;
            treeSetting.ShowEdit = false;
            treeSetting.ShowParentTreeData = true;
            treeSetting.IsAsync = false;
            treeSetting.TreeName = "菜单";

            return Json(treeSetting, JsonRequestBehavior.AllowGet);
        }
    }
}
