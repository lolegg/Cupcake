using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cupcake.Web.WapTemplate.edmx;
using System.Data.SqlClient;
using Cupcake.Web.WapTemplate.Models;

namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceComments_Collect
    {
        /// <summary>
        /// 已收藏
        /// </summary>
        /// <returns></returns>
        public List<Comments_Collect> HasListComments_Collect(Guid UserId)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {
                string sqlstr = string.Format(@" select* from  Comments_Collect where IsDelete=0 ");
                if (UserId != null)
                {
                    sqlstr += string.Format(@" and UserId='{0}' ", UserId);
                }
                sqlstr += string.Format(@" order by CreateDate desc ");
                return db.Database.SqlQuery<Comments_Collect>(sqlstr).ToList();
            }
        }
        //转换
        public List<Comments_CollectModel> TurnListComments_CollectModel(List<Comments_Collect> info) {
            List<Comments_CollectModel> list = new List<Comments_CollectModel>();
            if (info.Count > 0)
            {
                foreach (var dt in info)
                {
                    Comments_CollectModel model = new Comments_CollectModel();
                    model.Id = dt.Id;
                    model.CreateDate = dt.CreateDate;
                    model.UpdateDate = dt.UpdateDate;
                    model.IsDelete = dt.IsDelete;
                    model.ChildId = dt.ChildId;
                    model.Title = dt.Title;
                    model.UserId = dt.UserId;
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 收藏记录
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<Comments_Collect> RecordListComments_Collect(Guid UserId)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {
                string sqlstr = string.Format(@" select* from  Comments_Collect where IsDelete=1");
                if (UserId != null)
                {
                    sqlstr += string.Format("@ and UserId='{0}'", UserId);
                }
                sqlstr += string.Format("@ order by CreateDate");
                return db.Database.SqlQuery<Comments_Collect>(sqlstr).ToList();
            }
        }
        /// <summary>
        /// 删除单个收藏
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool HasComments_CollectDelete(Guid Id)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {
                var info = db.Comments_Collect.Where(m => m.IsDelete == false && m.Id == Id).SingleOrDefault();
                db.Comments_Collect.Remove(info);
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 批量删除收藏
        /// </summary>
        /// <returns></returns>
        public int HasListComments_CollectDelete(string arry)
        {

            var monitor = 0;
            if (arry != null)
            {
                var arrylist = arry.Substring(0, arry.Length - 1).Replace("\"", "").Replace("[", "").Replace("]", "").Split(',');
                foreach (var itme in arrylist)
                {
                    if (itme != null && itme != "")
                    {
                  
                        var guid = new Guid(itme);
                        bool where = HasComments_CollectDelete(guid);
                        if (where == true)
                        {
                            ++monitor;
                        }
                    }
                }
            }
            return monitor;
        }


        /// <summary>
        /// 删除单个收藏记录
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool RecordComments_CollectDelete(Guid Id)
        {
            using (CupcakeEntities db = new CupcakeEntities())
            {
                var info = db.Comments_Collect.Where(m => m.IsDelete == true && m.Id == Id).SingleOrDefault();
                db.Comments_Collect.Remove(info);
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 批量删除收藏记录
        /// </summary>
        /// <returns></returns>
        public int RecordListComments_CollectDelete(string arry)
        {
            //   批量删除
            var monitor = 0;
            if (arry != null)
            {
                var arrylist = arry.Substring(0, arry.Length - 1).Replace("\"", "").Replace("[", "").Replace("]", "").Split(',');
                foreach (var itme in arrylist)
                {
                    if (itme != null && itme != "")
                    {
                        var guid = new Guid(itme);
                        bool where = RecordComments_CollectDelete(guid);
                        if (where == false)
                        {
                            ++monitor;
                        }
                    }
                }
            }
            return monitor;
        }

    }
}