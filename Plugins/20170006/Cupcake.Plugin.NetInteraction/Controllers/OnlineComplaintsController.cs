using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Plugin.NetInteraction.Domain;
using Cupcake.Plugin.NetInteraction.Models;
using Cupcake.Plugin.NetInteraction.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cupcake.Plugin.NetInteraction.Controllers
{
    public class OnlineComplaintsController : Controller
    {
        private readonly IOnlineComplaintsService service;

        public OnlineComplaintsController(IOnlineComplaintsService OnlineComplaintsService)
        {
            this.service = OnlineComplaintsService;
        }
        public ActionResult Index(OnlineComplaintsCondition condition)
        {


            var news = service.SearchAllOnlineComplaints(condition);
            var models = new PagedList<OnlineComplaints>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create()
        {

            OnlineComplaintsModel model = new OnlineComplaintsModel();


            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(OnlineComplaintsModel model)
        {
            ModelState.Remove("Reply");
            if (ModelState.IsValid)
            {

                OnlineComplaints info = new OnlineComplaints();
                info.Id = Guid.NewGuid();

                info.Title = model.Title;

                info.Content = model.Content;



                info.Name = model.Name;
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.Phone;



                //是否显示到前端页面
                info.IsDisplay =false;

                List<OnlineComplaints> list = service.GetserialNumber();
                if (list != null && list.Count != 0)
                {
                    info.serialNumber = list[0].serialNumber + 1;
                }
                else
                {

                    info.serialNumber = 1000;
                }

                service.Add(info);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);

        }


        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            OnlineComplaintsModel model = new OnlineComplaintsModel();
            var info = service.GetById(id);


            model.Content = info.Content;

            model.Id = info.Id;
            model.Title = info.Title;



            model.Name = info.Name;
            model.Address = info.Address;
            model.Email = info.Email;
            model.Phone = info.Phone;

        

            //是否显示到前端页面
            model.IsDisplay = info.IsDisplay;

            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(OnlineComplaintsModel model)
        {
            ModelState.Remove("Reply");
            if (ModelState.IsValid)
            {



                OnlineComplaints info = service.GetById(model.Id);

                info.Title = model.Title;

                info.Content = model.Content;


                info.Name = model.Name;
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.Phone;



                //是否显示到前端页面
                info.IsDisplay = model.IsDisplay;




                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            OnlineComplaintsModel model = new OnlineComplaintsModel();
            var info = service.GetById(id);


            model.Content = info.Content;
            model.Title = info.Title;

            model.Name = info.Name;
            model.Address = info.Address;
            model.Email = info.Email;
            model.Phone = info.Phone;

            model.serialNumber = info.serialNumber;

            //是否显示到前端页面
            model.IsDisplay = info.IsDisplay;

            model.Reply = info.Reply;

            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            OnlineComplaintsModel model = new OnlineComplaintsModel();
            var info = service.GetById(id);
            if (info != null)
            {
                service.Delete(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }

            return View();


        }

        [HttpGet]
        public ActionResult Display(Guid id)
        {
            OnlineComplaintsModel model = new OnlineComplaintsModel();
            var info = service.GetById(id);


            model.Content = info.Content;
            model.Title = info.Title;


            model.Name = info.Name;
            model.Address = info.Address;
            model.Email = info.Email;
            model.Phone = info.Phone;

            model.IsDisplay = info.IsDisplay;

            model.Reply = info.Reply;

            model.serialNumber = info.serialNumber;

            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);
        }


        [HttpPost]
        public ActionResult Display(OnlineComplaintsModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineComplaints info = service.GetById(model.Id);



                info.Content = model.Content;
                info.Title = model.Title;


                info.Name = model.Name;
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.Phone;

                //是否显示到前端页面
                info.IsDisplay = model.IsDisplay;

                info.Reply = model.Reply;


                service.Update(info);
                User user = Session["User"] as User;
                string sql = "insert into MemberDengJi_MessageCenter([Id],[NewsId],[Title],[Sender],[MessageType],[Type] ,[CreateDate],[UpdateDate],[IsDelete]) values('" + Guid.NewGuid() + "','" + model.Id + "','" + model.Title + "','" + user.Name + "','2',0,'" + DateTime.Now + "','" + DateTime.Now + "',0)";

                DBHelper.InsertData(sql, DBHelper.connectionstring);
                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);

        }


    }
}
