using Cupcake.Web.Models;
using Cupcake.Core.Domain;

using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Core;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Controllers
{
    public class SelectPermissionsController : BaseTreeController<HasPermissionsCondition>
    {
        #region 重写
        public override JsonResult EditTree()
        {
            string id = Request["id"];
            string name = Request["name"];
            MenuService service = new MenuService();
            service.Update(id, name);
            return Json(new TreeAjaxResult { Result = Result.Success, CurrentNodeId = id }, JsonRequestBehavior.AllowGet);
        }

        public override JsonResult DeleteTree()
        {
            string id = Request["id"];
            return Json(new TreeAjaxResult { Result = Result.Success, CurrentNodeId = id }, JsonRequestBehavior.AllowGet);
        }

        public override JsonResult InsertTree()
        {
            string pid = Request["pid"];
            string name = Request["name"];
            Menu menu = new Menu();
            menu.Name = name;
            menu.ParentId = new Guid(pid);
            menu.Id = Guid.NewGuid();

            MenuService service = new MenuService();
            service.Insert(menu);
            return Json(new TreeAjaxResult { Result = Result.Success, CurrentNodeId = menu.Id.ToString() }, JsonRequestBehavior.AllowGet);
        }


        public override JsonResult CheckTree()
        {
            Guid roleId = Guid.Empty;
            if (!Guid.TryParse(Request["roleid"].ToString(), out roleId))
            {
                throw new NopException("角色Id不合法!");
            }
            else
            {
                var checkNodes = base.GetCheckedNodeIds();
                RoleService roleService = new RoleService();
                roleService.SetRoleHasMenus(roleId, checkNodes);
            }
            return Json(new TreeAjaxResult { Result = Result.Success, Message = "菜单权限设置成功" }, JsonRequestBehavior.AllowGet);
        }

        public override JsonResult LoadTree()
        {
            MenuService menuService = new MenuService();
            IList<Menu> roleHasMenus = new List<Menu>();
            Guid roleId = Guid.Empty;
            if (!Guid.TryParse(Request["roleid"].ToString(), out roleId))
            {
                throw new NopException("角色Id不合法!");
            }
            else
            {
                roleHasMenus = menuService.GetRoleHasMenus(roleId);
            }

            var treeNodes = new List<TreeNode>();
            var menus = menuService.GetAllMenusForTree();
            foreach (dynamic menu in menus)
            {
                var treeNode = new TreeNode();
                treeNode.id = menu.Id.ToString();
                treeNode.parentid = menu.ParentId == null ? "0" : menu.ParentId.ToString();
                treeNode.name = menu.Name;
                treeNode.isParent = menu.ChildrenNumber > 0 ? "true" : "false";
                if (roleHasMenus.Any(rm => rm.Id == menu.Id))
                {
                    treeNode.check = "true";
                }
                treeNodes.Add(treeNode);
            }

            return Json(treeNodes, JsonRequestBehavior.AllowGet);
        }

        //重写设置，屏蔽删除按钮
        public override JsonResult GetTreeSetting()
        {
            TreeSetting ts = new TreeSetting();
            ts.ShowDelete = false;
            //ts.ShowTreeData = false;
            ts.ShowAdd = false;
            ts.ShowEdit = false;
            ts.IsAsync = false;
            //ts.ShowParentTreeData = true;
            ts.TreeName = "菜单";
            return Json(ts, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult NodeView(HasPermissionsCondition condition)
        {
            if (condition.RoleId == null || condition.NodeId == null)
            {
                throw new NopException("参数没有赋值!");
            }
            ViewBag.RoleId = condition.RoleId.ToString();
            ViewBag.MenuId = condition.NodeId.ToString();

            //该菜单所有的自定义权限
            var customPermissionsService = new CustomPermissionsService();
            var customPermissions = customPermissionsService.GetCustomPermissionsByMenu(condition.NodeId);
            //该角色对应菜单的自定义权限
            var hasPermissionsService = new HasPermissionsService();
            var hasPermissions = hasPermissionsService.GetRoleHasPermissions(condition.RoleId, condition.NodeId);

            List<RoleHasPermissionsModel> models = new List<RoleHasPermissionsModel>();
            customPermissions.ForEach(p =>
            {
                var model = new RoleHasPermissionsModel()
                {
                    Id = p.Id,
                    CustomPermissionsName = p.Name,
                    HasPermission = hasPermissions.Any(hp => hp.PermissionId == p.Id)
                };
                models.Add(model);
            });

            return PartialView(models);
        }

        [HttpPost]
        public ContentResult SaveMenuPermission(string menuid,string roleid,string permission) 
        {
            MenuPermissionService service = new MenuPermissionService();
            if (!string.IsNullOrEmpty(menuid) && !string.IsNullOrEmpty(roleid))
            {
                service.Add(menuid, roleid, permission.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries).ToList());
                return Content("0");
            }
            return Content("-1");
        }

        #endregion


        #region 其他功能

    
        public ActionResult Details(int id)
        {
            //PermissionService service = new PermissionService();
            //service.many2many();
            return View();
        }

        //
        // GET: /Permission/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Permission/Create

        [HttpPost]
        //public ActionResult Create(BasePermissionModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CustomPermissions service = new CustomPermissions();
        //        if (service.IsDuplicate(model.Name))
        //        {
        //            ModelState.AddModelError("Name", "权限名重复");
        //        }
        //        else
        //        {
        //            service.Add(model.ToPermission());
        //            return JavaScript("window.parent.close();");
        //        }
        //    }
        //    return View(model);
        //}



        public ActionResult Edit(int id)
        {
            //PermissionService service = new PermissionService();
            //service.GetPermissionsByUser(new Guid("383ED413-748A-E511-98A7-002564BA5C19"));
            return View();
        }



        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Permission/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Permission/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

    }
}
