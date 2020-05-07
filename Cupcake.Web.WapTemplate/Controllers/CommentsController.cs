using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Helper;
using Cupcake.Web.WapTemplate.Models;
using Cupcake.Web.WapTemplate.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;


namespace Cupcake.Web.WapTemplate.Controllers
{
    public class CommentsController : Controller
    {
        //全局信息id
        static Guid? Baseid = Guid.Parse("00000000-0000-0000-0000-000000000000");
        //
        // GET: /Comments/
        public ActionResult PartialPlIndex(Guid? id, string Title)
        {
            ViewBag.Id = id;
            ViewBag.Title = Title;
            return View();
        }

        //评论列表
        public ActionResult PlListJson(int PgaeIndex, string sort, Guid? ChildId)
        {
            //把id赋值给全局信息id
               if (ChildId!=null)
               {
                Baseid = ChildId;
                }
               Baseid = Guid.Parse("8bab4317-63a5-4955-bfc4-05e39b6eef07");
                if (Baseid == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                     return Json(null);

                int PageSize = 5;
                using (CupcakeEntities db = new CupcakeEntities())
                {
                    ServiceComments service = new ServiceComments();
                    var model = db.Comments.GroupJoin(
                        db.Comments_Praise,
                        ListCommentsModel => ListCommentsModel.Id,
                        b => b.ChildId,
                        (ListCommentsModel, b) => new
                        {
                            ListCommentsModel,
                            b
                        }).Select(m => m).ToList();
                    var list = model.Where(m => m.ListCommentsModel.ChildId == Baseid).OrderByDescending(m => m.ListCommentsModel.PraiseNum).ThenByDescending(m => m.ListCommentsModel.PublicDate)
                           .Skip((PgaeIndex - 1) * PageSize).Take(PageSize).ToList();
                    if (sort == "PublicDate")
                    {
                        list = model.Where(m => m.ListCommentsModel.ChildId == Baseid).OrderByDescending(m => m.ListCommentsModel.PublicDate).ThenByDescending(m => m.ListCommentsModel.PraiseNum)
                         .Skip((PgaeIndex - 1) * PageSize).Take(PageSize).ToList();
                    }
                    else if (sort == "PraiseNum")
                    {
                        list = model.Where(m => m.ListCommentsModel.ChildId == Baseid).OrderByDescending(m => m.ListCommentsModel.PraiseNum).ThenByDescending(m => m.ListCommentsModel.PublicDate)
                           .Skip((PgaeIndex - 1) * PageSize).Take(PageSize).ToList();
                    }

                    CommentsModel models = new CommentsModel();
                    models.PageTotal = model.Where(m => m.ListCommentsModel.ChildId == Baseid).Count();
                    models.PageIndex = PgaeIndex;
                    models.PageSize = PageSize;
                    return Json(new { list = list, models = models }, JsonRequestBehavior.AllowGet);
                }
       }
        //新增一级评论

        public ActionResult PartialPlAdd() {
            return View();
        }
        [HttpPost]
        public ActionResult PartialPlAddd()
        {
            if (Baseid == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                return Json(null);
            var JsonData = Request.Form["JsonData"];
            //var user = Session["User"] as User;
            //User user1 = new User();
            //if (user == null)
            //{

              var  userName = "马飞飞";
               var userId = Guid.Parse("9393ed42-7b3f-e711-afa3-002564ba5c19");
            //}
            string Content = Request.Form["Content"];
            string Title = Request.Form["title"];
            ServiceComments service = new ServiceComments();
            bool isWhere = service.GetPlAdd(Baseid, Content, userId, userName,Title);
            int count= service.GetReplyNum(Baseid);
            return Json(new { isWhere = isWhere, count = count });
        }
        //首次加载显示所有评论数量
        public ActionResult AllReplyNum() {
            if (Baseid == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                return Json(null);

            ServiceComments service = new ServiceComments();
            int count = service.GetReplyNum(Baseid);
            return Json(count);
        }


        //点赞
        public ActionResult PraiseJson(string Userid, string ChildId)
        {
            //result=1  点赞成功
            //result=2  取消点赞
            //result=3  用户未登录
            int result2 = 0;

            Userid = "9393ed42-7b3f-e711-afa3-002564ba5c19";

            int num = 0;
            if (Userid != "" && ChildId != "")
            {
                ServiceComments_Praise ServiceComments_Praise = new Services.ServiceComments_Praise();
                Comments_Praise Comments_Praise = new Comments_Praise();
                Comments_Praise.UserId = Guid.Parse(Userid);
                Comments_Praise.ChildId = Guid.Parse(ChildId);
                int result1 = ServiceComments_Praise.PraiseAdd(Comments_Praise);

                if (result1 > 0)
                {
                    ServiceComments ServiceComments = new ServiceComments();
                    result2 = ServiceComments.PraisePlusOne(Guid.Parse(ChildId), result1);
                }
                using (CupcakeEntities db = new CupcakeEntities())
                {
               
                    Guid uid = Guid.Parse(Userid);
                    Guid cid = Guid.Parse(ChildId);
                    if (db.Comments.Where(m =>m.Id == cid).Count()>0)
                    {
                        num = db.Comments.Where(m =>m.Id == cid).SingleOrDefault().PraiseNum;
                    }
                };
            }
            else
            {
                result2 = 3;
            }

            return Json(new { result = result2, num = num }, JsonRequestBehavior.AllowGet);
        }

        //二级评论列表
        public ActionResult TwoPlShowJson(Guid Childid)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {

                ServiceComments service = new ServiceComments();
                var model = db.Comments.GroupJoin(
                    db.Comments_Praise,
                    ListCommentsModel => ListCommentsModel.Id,
                    b => b.ChildId,
                    (ListCommentsModel, b) => new
                    {
                        ListCommentsModel,
                        b
                    }).Select(m => m).ToList();
                var list = model.Where(m => m.ListCommentsModel.ToCommentsId == Childid).OrderByDescending(m => m.ListCommentsModel.PublicDate).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        //新增二级评论
        [HttpPost]
        public ActionResult TwoPlAddJson()
        {
            //var user = Session["User"] as User;
            //User user1 = new User();
            //if (user == null)
            //{

              var  userName = "马飞飞";
              var  userId = Guid.Parse("9393ed42-7b3f-e711-afa3-002564ba5c19");
            //}
            string Content = Request.Form["Content"];
            Guid ToTwoCommentsId = Guid.Parse(Request.Form["ToTwoCommentsId"]);
            Guid ToCommentsId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            if (!string.IsNullOrEmpty(Request.Form["ChildId"]))
            {
                ToCommentsId = Guid.Parse(Request.Form["ChildId"]);
            }
            ServiceComments service = new ServiceComments();
            bool isWhere = service.GetTwoPlAdd(ToCommentsId, ToTwoCommentsId, Content, userId, userName);
            int ReplyNum = 0;
            if(isWhere==true){
              //新增回复数量
               service.GetGetTwoReplyNum(ToCommentsId);
               using (CupcakeEntities db = new CupcakeEntities())
               {
                   ReplyNum = db.Comments.Where(m => m.Id==ToCommentsId && m.IsDelete == false).SingleOrDefault().ReplyNum;
               }
            }
            return Json(new { isWhere = isWhere, ReplyNum = ReplyNum });
        }
        public ActionResult Template()
        {

            return View();
        }

    }


}