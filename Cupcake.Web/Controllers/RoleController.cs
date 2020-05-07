using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleService roleService;
        public RoleController()
        {
            roleService = new RoleService();
        }

        public ActionResult Index(RoleCondition condition)
        {
            var roles = roleService.SearchRoles(condition);
            var models = new PagedList<Role>(roles, roles.Paging);

            return View(models);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                if (roleService.AlreadyExists(model.Name, Guid.Empty))
                {
                    ModelState.AddModelError("Name", "角色名重复");
                }
                else
                {
                    roleService.Add(model.ToEntity());
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var role = roleService.Get(id);
            if (role == null || role.IsDelete == true)
            {
                throw new NopException("角色不存在");
            }
            return View(role.ToModel());
        }

        [HttpPost]
        public ActionResult Edit(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                if (roleService.AlreadyExists(model.Name, model.Id))
                {
                    ModelState.AddModelError("Name", "角色名重复");
                }
                else
                {
                    var role = roleService.Get(model.Id);
                    //忽略更新
                    model.CreateDate = role.CreateDate;

                    role = model.ToEntity(role);
                    roleService.Modify(role);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            var role = roleService.Get(id);
            if (role == null || role.IsDelete == true)
            {
                throw new NopException("角色不存在");
            }
            if (role.Users.Count > 0)
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = "该角色正在使用中, 无法删除!" });
            }
            else
            {
                role.IsDelete = true;
                role.UpdateDate = DateTime.Now;
                roleService.Modify(role);
                return Json(new AjaxResult() { Result = Result.Success });
            }

        }

        [HttpPost]
        public ActionResult AssignPermissions(Guid? roleId, Guid? menuId, List<Guid> selectedNodeIds)
        {
            if (roleId == null || menuId == null)
            {
                return Json(new AjaxResult() { Result = Result.Success, Message = "未选择自定义权限" });
            }
            else
            {
                var hasPermissionsService = new HasPermissionsService();
                hasPermissionsService.SetRoleHasPermissions(roleId.Value, menuId.Value, selectedNodeIds);
                return Json(new AjaxResult() { Result = Result.Success });
            }
        }
    }
}
