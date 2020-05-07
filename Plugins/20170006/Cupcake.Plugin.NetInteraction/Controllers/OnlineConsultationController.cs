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
    public class OnlineConsultationController : Controller
    {
        private readonly IOnlineConsultationService service;

        public OnlineConsultationController(IOnlineConsultationService OnlineConsultationService)
        {
            this.service = OnlineConsultationService;
        }
        public ActionResult Index(OnlineConsultationCondition condition)
        {


            var news = service.SearchAllOnlineConsultation(condition);
            var models = new PagedList<OnlineConsultation>(news, news.Paging);

            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), models);

        }

        [HttpGet]
        public ActionResult Create()
        {

            OnlineConsultationModel model = new OnlineConsultationModel();


            return View(PluginHelper.GetViewPath(this.GetType(), "Create"), model);


        }
     
        [HttpPost]
        public ActionResult Create(OnlineConsultationModel model)
        {
            ModelState.Remove("ReplyTime");
            ModelState.Remove("Reply");
            if (ModelState.IsValid)
            {

                OnlineConsultation info = new OnlineConsultation();
                info.Id = Guid.NewGuid();

                info.Title = model.Title;

                info.Content = model.Content;


               

                info.Name = model.Name;
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.Phone;

                info.IsDisplay = false;

                info.AskTime = DateTime.Now;
                info.ReplyTime = DateTime.Now;

                List<OnlineConsultation> list = service.GetserialNumber();
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

            OnlineConsultationModel model = new OnlineConsultationModel();
            var info = service.GetById(id);


            model.Content = info.Content;

            model.Id = info.Id;
            model.Title = info.Title;



            model.Name = info.Name;
            model.Address = info.Address;
            model.Email = info.Email;
            model.Phone = info.Phone;

            model.IsDisplay = false;
            model.AskTime = info.AskTime;

            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);


        }
       
        [HttpPost]
        public ActionResult Edit(OnlineConsultationModel model)
        {

            ModelState.Remove("ReplyTime");
            ModelState.Remove("Reply");
            if (ModelState.IsValid)
            {



                OnlineConsultation info = service.GetById(model.Id);

                info.Title = model.Title;

                info.Content = model.Content;

                info.Name = model.Name;
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.Phone;

                info.IsDisplay = false;

                service.Update(info);
                return Json(new AjaxResult() { Result = Result.Success });
            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Edit"), model);

        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {

            OnlineConsultationModel model = new OnlineConsultationModel();
            var info = service.GetById(id);


            model.Content = info.Content;
            model.Title = info.Title;

            model.Name = info.Name;
            model.Address = info.Address;
            model.Email = info.Email;
            model.Phone = info.Phone;

            model.IsDisplay = info.IsDisplay;


            model.Reply = info.Reply;

            model.AskTime = info.AskTime;
            model.ReplyTime = info.ReplyTime;

            model.serialNumber = info.serialNumber;
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), model);


        }

        public ActionResult Delete(Guid id)
        {

            OnlineConsultationModel model = new OnlineConsultationModel();
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
            OnlineConsultationModel model = new OnlineConsultationModel();
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
        public ActionResult Display(OnlineConsultationModel model)
        {
            if (ModelState.IsValid)
            {

                OnlineConsultation info = service.GetById(model.Id);



                info.Content = model.Content;
                info.Title = model.Title;


                info.Name = model.Name;
                info.Address = model.Address;
                info.Email = model.Email;
                info.Phone = model.Phone;

                //是否显示到前端页面
                info.IsDisplay = model.IsDisplay;

                info.Reply = model.Reply;
                info.ReplyTime = model.ReplyTime;

                service.Update(info);

                User user = Session["User"] as User;
                string sql = "insert into MemberDengJi_MessageCenter([Id],[NewsId],[Title],[Sender],[MessageType],[Type] ,[CreateDate],[UpdateDate],[IsDelete]) values('" + Guid.NewGuid() + "','" + model.Id + "','" + model.Title + "','" + user.Name + "','3',0,'" + DateTime.Now + "','" + DateTime.Now + "',0)";

                DBHelper.InsertData(sql, DBHelper.connectionstring);

                return Json(new AjaxResult() { Result = Result.Success });

            }
            return View(PluginHelper.GetViewPath(this.GetType(), "Display"), model);

        }


    }
}
