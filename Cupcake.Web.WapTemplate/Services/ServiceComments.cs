using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Services;
using Cupcake.Web.WapTemplate.Models;
using System.Data.SqlClient;
using System.Data;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceComments
    {

        //评论List
        public List<Comments> GetPlList(Guid ChildId)
        {

            using (CupcakeEntities db = new CupcakeEntities())
            {
               // string sqlstr = string.Format(@" select A.Id,A.ImgUrl,A.UserId,A.Name,A.Content,A.PublicDate,A.ChildId,A.TypeChildId,A.PraiseNum,A.ToMessageId,A.ToCommentsId,B.UserId as dzUserId,b.ChildId as dzChildId  from Comments as A left join Comments_Praise as B on A.Id=B.ChildId ");
                //if(id!=null){
                //    sqlstr += string.Format(@" where A.ChildId={0} ", id);
                //}
                string sqlstr = string.Format(@" select* from  Comments ");
                if (ChildId != null)
                {
                    //判断信息所属Id
                    sqlstr += string.Format(@" where ChildId='{0}' ", ChildId);
                }

                sqlstr += string.Format(@" order by PraiseNum desc,PublicDate desc");
                return db.Database.SqlQuery<Comments>(sqlstr).ToList();
            }
        }

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool GetPlAdd(Guid? ChildId, string Content, Guid UserId, string UserName, string Title)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {

                Comments model = new Comments();
                model.Id = Guid.NewGuid();
                model.IsDelete = false;
                model.PublicDate = DateTime.Now;
                model.ChildId = ChildId;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                model.UserId = UserId;
                model.Name = UserName;
                model.Content = Content;
                model.ImgUrl = null;
                model.MessageName = Title;
                db.Comments.Add(model);
                return db.SaveChanges() > 0;
            }

        }

        /// <summary>
        /// 所有评论数
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int GetReplyNum(Guid? Chlidid) { 

                using (CupcakeEntities db = new CupcakeEntities())
                {
                    int Count = db.Comments.Where(m => m.ChildId == Chlidid || m.ToTwoCommentsId == Chlidid).Count();
                    return Count;
                }
            
        }


        /// 转换List<T> 评论表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<CommentsModel> TurnListModel(List<Comments> info)
        {
            List<CommentsModel> List = new List<CommentsModel>();
            if (info.Count > 0)
            {
                foreach (var dt in info)
                {
                    CommentsModel model = new CommentsModel();
                    model.Id = dt.Id;
                    model.Content = dt.Content;
                    model.CreateDate = dt.CreateDate;
                    model.UpdateDate = dt.UpdateDate;
                    model.IsDelete = dt.IsDelete;
                    model.Name = dt.Name;
                    model.ChildId = dt.ChildId;
                    model.ImgUrl = dt.ImgUrl;
                    model.PraiseNum = dt.PraiseNum;
                    model.PublicDate = dt.PublicDate;
                    model.UserId = dt.UserId;
                    model.ToCommentsId = dt.ToCommentsId;
                    List.Add(model);
                }
            }
            return List;
        }




        /// <summary>
        /// 点赞+1/-1
        /// </summary>
        /// <param name="ChildId"></param>
        /// <returns></returns>
        public int PraisePlusOne(Guid Id, int result1)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {

                //result1 =1  新增   +1
                //result1 =2  删除   -1
                var info = db.Comments.Where(m => m.Id == Id && m.IsDelete == false).SingleOrDefault();
                int result = 0;
                if(result1==1){
                    info.PraiseNum = info.PraiseNum + 1;
                }
                if (result1==2) {
                    info.PraiseNum = info.PraiseNum - 1;
                }
                int bc= db.SaveChanges();
                if (bc > 0 && result1 == 1)
                {
                    result = 1;
                }
                else if (bc > 0 && result1 == 2)
                {
                    result = 2; 
                }
                return result;
            }
        }
        /// <summary>
        /// 二级评论列表
        /// </summary>
        /// <param name="ChildId"></param>
        /// <returns></returns>
        public List<Comments> GetTwoPlList(Guid ToCommentsId)
        {
            using (CupcakeEntities db=new CupcakeEntities())
            {
                string sqlstr = string.Format(@" select* from  Comments");
                if (ToCommentsId!=null)
                {
                    sqlstr += string.Format(@" where ToCommentsId='{0}'", ToCommentsId);
                }
                sqlstr += string.Format(@" order by CreateDate desc");
                return db.Database.SqlQuery<Comments>(sqlstr).ToList();
            }
        }
        /// <summary>
        /// 新增二级评论
        /// </summary>
        /// <returns></returns>
        public bool GetTwoPlAdd(Guid ToCommentsId, Guid ToTwoCommentsId, string Content, Guid UserId, string UserName)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {
                Comments model = new Comments();
                model.Id = Guid.NewGuid();
                model.IsDelete = false;
                model.PublicDate = DateTime.Now;
                model.ToCommentsId = ToCommentsId;
                model.ToTwoCommentsId = ToTwoCommentsId;
                model.ChildId = null;
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                model.UserId = UserId;
                model.Name = UserName;
                model.Content = Content;
                model.ImgUrl = null;
                model.PraiseNum = 0;
                model.ReplyNum = 0;
                model.MessageName=db.Comments.Where(m => m.Id == ToCommentsId && m.IsDelete == false).SingleOrDefault().MessageName;
                model.TypeChildId = Guid.Parse("00000000-0000-0000-0000-000000000000");
                model.CreatorId = null;
                db.Comments.Add(model);
                db.Configuration.ValidateOnSaveEnabled = false;
                return db.SaveChanges() > 0;
            }

        }
        /// <summary>
        /// 新增回复数量
        /// </summary>
        /// <param name="ChildId"></param>
        /// <returns></returns>

        public bool GetGetTwoReplyNum(Guid ToCommentsId)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {
                var list = db.Comments.Where(m => m.Id == ToCommentsId && m.IsDelete == false).SingleOrDefault();
                list.ReplyNum = list.ReplyNum + 1;
                return db.SaveChanges()>0;
            }
        }
      
    }
}