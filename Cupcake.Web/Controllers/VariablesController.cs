using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Models;
using System;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class VariablesController : BasePublicController
    {
        private readonly VariablesService variablesService;
        public VariablesController()
        {
            variablesService = new VariablesService();
        }

        public ActionResult Index(VariablesCondition condition)
        {
            var variables = variablesService.SearchVariables(condition);
            return View(variables);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VariablesModel model)
        {
            if (ModelState.IsValid)
            {
                if (variablesService.AlreadyExists(model.Name, Guid.Empty))
                {
                    ModelState.AddModelError("Name", "变量名重复");
                }
                else
                {
                    variablesService.Add(model.ToEntity());
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var variable = variablesService.Get(id);
            if (variable == null || variable.IsDelete == true)
            {
                throw new NopException("该变量不存在");
            }
            variablesService.Remove(variable);

            return Json(new AjaxResult() { Result = Result.Success });
        }

        public ActionResult Edit(Guid id)
        {
            var variable = variablesService.Get(id);
            if (variable == null || variable.IsDelete == true)
            {
                throw new NopException("该变量不存在");
            }

            return View(variable.ToModel());
        }
        [HttpPost]
        public ActionResult Edit(VariablesModel model)
        {
            if (ModelState.IsValid)
            {
                if (variablesService.AlreadyExists(model.Name, model.Id))
                {
                    ModelState.AddModelError("Name", "变量名重复");
                }
                else
                {
                    var variable = variablesService.Get(model.Id);
                    //忽略更新
                    model.CreateDate = variable.CreateDate;

                    variable = model.ToEntity(variable);
                    variablesService.Modify(variable);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model); ;
        }
    }
}
