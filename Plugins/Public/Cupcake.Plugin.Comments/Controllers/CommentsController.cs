using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Core.Log4;
using Cupcake.Plugin.Comments.Domain;
using Cupcake.Plugin.Comments.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Plugin.Comments.Helper;
using Cupcake.Plugin.Comments.Models;
using AutoMapper;
using System.Data.SqlClient;


namespace Cupcake.Plugin.Comments.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService service;
       // private readonly IComments_PraiseService IComments_PraiseService;

        public CommentsController(ICommentsService newsService)
        {

            this.service = newsService;
           // this.IComments_PraiseService = newIComments_PraiseService;

        }
        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Index(Guid? ChildId)
        {
           
            CommentsCondition condition = new CommentsCondition();
            condition.ChildId = ChildId;
            var news = service.SearchNotice(condition);
            var info = new PagedList<CommentsInfo>(news, news.Paging);
            CommentsModel model = new CommentsModel();
            model.ListCommentsModel = Mapper.Map<List<CommentsModel>>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Index"), info);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var result=Result.Success;
            var message="";
            if (id != null)
            {
             var count=  service.GetByToCommentsId(id);
             if (count == 0)
             {
                 var info = service.GetById(id);
                 service.DeleteGoogleProduct(info);
                 result = Result.Success;
                 message="操作成功";
             }
             else if (count>0)
             {
                 result = Result.Warning;
                 message="子级下含有数据，不能操作！";
             }
            }
            return Json(new AjaxResult() { Result = result, Message = message });
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid id)
        {
            var info = service.GetById(id);
          //  CommentsModel model = new CommentsModel();
         //   model = Mapper.Map<CommentsModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), info);
        }

        /// <summary>
        /// 子级评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChildShow(Guid id)
        {
            CommentsCondition condition = new CommentsCondition();
            condition.ToCommentsId = id;
            var news = service.ChildidSearchNotice(condition);
            var info = new PagedList<CommentsInfo>(news, news.Paging);
            //CommentsModel model = new CommentsModel();
            //model.ListCommentsModel = Mapper.Map<List<CommentsModel>>(info);
            ViewBag.ChildId = id;
            return View(PluginHelper.GetViewPath(this.GetType(), "ChildShow"), info);
        }

        public ActionResult ChildDetails(Guid id)
        {
            var info = service.GetById(id);
            //  CommentsModel model = new CommentsModel();
            //   model = Mapper.Map<CommentsModel>(info);
            return View(PluginHelper.GetViewPath(this.GetType(), "Details"), info);
        }

        public ActionResult ChildDelete(Guid id, Guid ToCommentsId)
        {
            if (id != null)
            {
                var info = service.GetById(id);
                var count = service.DeleteGoogleProduct(info);
                if (count > 0)
                {
                    service.UpdateReplyNum(ToCommentsId);
                }
            }
            return Json(new AjaxResult() { Result = Result.Success, Message = "操作成功" });
        }

        public ActionResult Template() {
            return View(PluginHelper.GetViewPath(this.GetType(), "Template"));
        }
    }
}
