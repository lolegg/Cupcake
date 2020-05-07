using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.TaskModule.Domain;
using Cupcake.Plugin.TaskModule.Models;
using Cupcake.Plugin.TaskModule.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Plugin.TaskModule.Controllers
{
    public class TaskDisposalController : Controller
    {
       private readonly ITaskIssuedService _googleService;
       private readonly ITaskDisposalService _googleService2;

       public TaskDisposalController(ITaskIssuedService googleService, ITaskDisposalService googleService2)
       {
            this._googleService = googleService;
            this._googleService2 = googleService2;
       }
       public ActionResult Index(TaskIssuedCondition condition)
       {
           var taskIssueds = _googleService.SearchTaskIssued(condition);
           var models = new PagedList<TaskIssuedInfo>(taskIssueds, taskIssueds.Paging);

           return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);
       }
       public ActionResult Disposal(Guid id)
       {
           var info = _googleService.GetById(id);
           var model = new TaskIssuedModel().ToModel(info);
           model.taskDisposalModel = new TaskDisposalModel();
           var user = Session["User"] as User;
           model.taskDisposalModel.DisposalPerson = user.UserName;
           return View(PluginHelper.GetViewPath(this.GetType(), "Disposal"), model);
       }

       [HttpPost]
       [ValidateInput(false)]
       public ActionResult Disposal(TaskIssuedModel model)
       {
           if (ModelState.IsValid)
           {
               TaskDisposalInfo taskDisInfo = model.taskDisposalModel.ToInfo();
               taskDisInfo.TaskIssuedId = model.Id;
               _googleService2.Add(taskDisInfo);
               var info = _googleService.GetById(model.Id);
               info.TaskManagement = true;
               _googleService.Update(info);
               return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
           }
           return View(PluginHelper.GetViewPath(this.GetType(), "Disposal"), model);
       }
       public ActionResult Details(Guid id)
       {
           var info = _googleService.GetById(id);
           var model = new TaskIssuedModel().ToModel(info);
           TaskDisposalInfo taskDis = _googleService2.GetByTaskIssuedId(id);
           if (taskDis!=null)
           {
               TaskDisposalModel taskDisModel = new TaskDisposalModel().ToModel(taskDis);
               model.taskDisposalModel = taskDisModel;
           }
           return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);
       }

    }
}
