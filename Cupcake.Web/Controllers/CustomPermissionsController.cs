using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Authorize;
using Cupcake.Web.Framework;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class CustomPermissionsController : BaseTreeController<CustomPermissionsCondition>
    {
        private readonly CustomPermissionsService customPermissionsService;
        private readonly HasPermissionsService hasPermissionsService;
        public CustomPermissionsController()
        {
            customPermissionsService = new CustomPermissionsService();
            hasPermissionsService = new HasPermissionsService();
        }
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
            //List<string> list = base.GetCheckedNodeIds();
            string roleid = Request["roleid"];
            return Json(new TreeAjaxResult { Result = Result.Success, CurrentNodeId = roleid }, JsonRequestBehavior.AllowGet);
        }
        [ClientAuthorize(Permissions = "查看")]
        public override JsonResult LoadTree()
        {
            var treeNodes = new List<TreeNode>();
            MenuService menuService = new MenuService();
            var menus = menuService.GetAllMenusForTree();
            foreach (dynamic menu in menus)
            {
                var treeNode = new TreeNode();
                treeNode.id = menu.Id.ToString();
                treeNode.parentid = menu.ParentId == null ? "0" : menu.ParentId.ToString();
                treeNode.name = menu.Name;
                treeNode.isParent = menu.ChildrenNumber > 0 ? "true" : "false";
                treeNodes.Add(treeNode);
            }
            return Json(treeNodes, JsonRequestBehavior.AllowGet);
        }

        //重写设置，屏蔽删除按钮
        public override JsonResult GetTreeSetting()
        {
            TreeSetting ts = new TreeSetting();
            ts.ShowDelete = false;
            ts.ShowCheck = false;
            ts.ShowAdd = false;
            ts.ShowEdit = false;
            ts.ShowParentTreeData = false;
            ts.IsAsync = false;
            ts.TreeName = "菜单";
           
            return Json(ts, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult NodeView(CustomPermissionsCondition condition)
        {
            var customPermissions = customPermissionsService.GetCustomPermissionsByMenu(condition);
            var models = new PagedList<CustomPermissions>(customPermissions, customPermissions.Paging);

            ViewBag.MenuId = condition.NodeId.ToString();
            return View(models);
        }

        #region 其他功能

        [HttpGet]
        public ActionResult Create(Guid? id)
        {
            CustomPermissionsModel model = new CustomPermissionsModel();
            if (id.HasValue)
            {
               model.MenuId = id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CustomPermissionsModel model)
        {
            if (ModelState.IsValid)
            {
                if (customPermissionsService.AlreadyExists(model.Name, model.MenuId, Guid.Empty))
                {
                    ModelState.AddModelError("Name", "权限名重复");
                }
                else
                {
                    customPermissionsService.Add(model.ToEntity());
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var customPermission = customPermissionsService.Get(id);
            if (customPermission == null || customPermission.IsDelete == true)
            {
                throw new NopException("该自定义权限不存在");
            }
            var model = customPermission.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CustomPermissionsModel model)
        {
            if (ModelState.IsValid)
            {
                if (customPermissionsService.AlreadyExists(model.Name, model.MenuId, model.Id))
                {
                    ModelState.AddModelError("Name", "自定义权限名重复");
                }
                else
                {
                    var customPermission = customPermissionsService.Get(model.Id);
                    //忽略更新
                    model.CreateDate = customPermission.CreateDate;
                    model.MenuId = customPermission.MenuId;

                    customPermission = model.ToEntity(customPermission);
                    customPermissionsService.Modify(customPermission);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var customPermission = customPermissionsService.Get(id);
            if (customPermission == null || customPermission.IsDelete == true)
            {
                throw new NopException("该权限不存在");
            }
            if (hasPermissionsService.PermissionUsed(id))
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = "该权限正在使用中" });
            }
            else
            {
                customPermissionsService.Remove(customPermission);
                return Json(new AjaxResult() { Result = Result.Success });
            }
        }
        #endregion
    }
}
