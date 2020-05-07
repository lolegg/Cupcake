using Cupcake.Web.Models;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cupcake.Core;
using Cupcake.Web.Authorize;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Controllers
{
    public class UserController : BasePublicController
    {
        private readonly UserService userService;
        public UserController()
        {
            userService = new UserService();
        }

        //[ClientAuthorize(Roles = "用户查看")]
        [ClientAuthorize(Permissions = "查看")]
        public ActionResult Index(UserCondition condition)
        {
            var users = userService.SearchUsers(condition);
            var models = new PagedList<UserModel>(users.Select(n => n.ToModel()), users.Paging);

            return View(models);
        }

        [HttpGet]
        public ActionResult AssignRoles(Guid id)
        {
            var user = userService.Get(id);
            if (user == null || user.IsDelete == true)
            {
                throw new NopException("用户不存在");
            }

            RoleService roleService = new RoleService();
            var roles = roleService.GetAll();

            var models = roles.Select(role =>
            {
                var model = role.ToModel();
                foreach (var userRole in user.Roles)
                {
                    if (userRole.Id == role.Id)
                    {
                        model.IsCheck = true;
                    }
                }
                return model;
            });
            ViewBag.UserId = id;
            return View(models);
        }

        [HttpPost]
        public ActionResult AssignRoles(Guid userId, IList<Guid> selectedRoleIds)
        {
            userService.AssignRoles(userId, selectedRoleIds);
            return Json(new AjaxResult() { Result = Result.Success });
        }

        [HttpGet]
        public ActionResult Create(string nodeId)
        {
            UserModel model = new UserModel();
            model.NodeId = nodeId;
            return View(model);
        }

        public ActionResult Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (userService.AlreadyExists(model.UserName, Guid.Empty))
                {
                    ModelState.AddModelError("UserName", "用户名重复");
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.NodeId))
                    {
                        Guid organizationId = Guid.Empty;
                        if (Guid.TryParse(model.NodeId, out organizationId))
                        {
                            //组织Guid
                            model.OrganizationId = organizationId;
                        }
                        else
                        {
                            //UARC的组织Id
                            model.UARCOrganizationId = Int32.Parse(model.NodeId);
                        }
                    }
                    userService.Add(model.ToEntity());
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var user = userService.Get(id);
            if (user == null || user.IsDelete == true)
            {
                throw new NopException("用户不存在");
            }
            var model = user.ToModel();
            model.Password = "******";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (userService.AlreadyExists(model.UserName, model.Id))
                {
                    ModelState.AddModelError("UserName", "用户名重复");
                }
                else
                {
                    var user = userService.Get(model.Id);
                    //忽略更新
                    model.CreateDate = user.CreateDate;
                    model.Password = user.Password;
                    
                    user = model.ToEntity(user);
                    userService.Modify(user);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var user = userService.Get(id);
            if (user == null || user.IsDelete == true)
            {
                throw new NopException("用户不存在");
            }
            user.Status = ObjectStatus.Disable;
            user.UpdateDate = DateTime.Now;
            userService.Modify(user);

            return Json(new AjaxResult() { Result = Result.Success });
        }
    }
}
