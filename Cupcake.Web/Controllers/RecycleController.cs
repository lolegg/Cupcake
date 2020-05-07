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
    public class RecycleController : BasePublicController
    {
        private readonly UserService userService;
        public RecycleController()
        {
            userService = new UserService();
        }

        public ActionResult Index(UserCondition condition)
        {
            var users = userService.SearchUsers(condition);
            var models = new PagedList<UserModel>(users.Select(n => n.ToModel()), users.Paging);

            return View(models);
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
