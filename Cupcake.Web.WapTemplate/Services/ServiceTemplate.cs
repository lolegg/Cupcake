using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  Cupcake.Web.WapTemplate.edmx;
using System.Data.SqlClient;
using System.EnterpriseServices;
using Cupcake.Web.WapTemplate.Models;
using System.Linq.Expressions;


namespace Cupcake.Web.WapTemplate.Services
{
    public class ServiceTemplate
    {
        /// <summary>
        /// 根据ID来获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Get(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Users.Where(p => p.Id == id).SingleOrDefault();
            }
        }

        public List<Users> GetListByTop(int topnum, int f_kindid)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("select top {0} * from Users where Name=@f_kindid  order by CreateDate", topnum);
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@f_kindid", f_kindid)); 
                return entity.Database.SqlQuery<Users>(sqlstr, param.ToArray()).ToList();
            }
        }

        public IList<Activity_Activitys> GetListActivityByTop(int topnum)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("select top {0} * from [Cupcake].[dbo].[Activity_Activitys] where IsTop='true'  order by UpdateDate desc", topnum);
                //List<SqlParameter> param = new List<SqlParameter>();
                //param.Add(new SqlParameter("@f_kindid", f_kindid));
                return entity.Database.SqlQuery<Activity_Activitys>(sqlstr).ToList();
            }
        }

        public Activity_Activitys GetbyActivity(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Activity_Activitys.Where(p => p.Id == id&&p.IsDelete==false).FirstOrDefault();
            }
        }

        public List<Activity_Activitys> GetListActivityByid(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("select * from [Cupcake].[dbo].[Activity_Activitys] where  [ActityType]=(select [ActityType] from [Cupcake].[dbo].[Activity_Activitys] where id='{0}') and not(Id='{1}') order by UpdateDate desc", id,id);
                return entity.Database.SqlQuery<Activity_Activitys>(sqlstr).ToList();
            }
        }

        public int GetRegistrationCount(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Activity_RegistrationStatus.Where(n => n.SubordinateActivitiesID == id && n.IsDelete == false).Count();
            }
        }

        public int GetIsRegistration(Guid Id, Guid ActivityId)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Activity_RegistrationStatus.Where(n => n.UserId == Id && n.SubordinateActivitiesID == ActivityId && n.IsDelete == false).Count();
            }
        }

        public int Add(string RegistrationName, string RegistrationTel, string UserId, string Id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                Activity_RegistrationStatus info = new Activity_RegistrationStatus();
                info.Id = Guid.NewGuid();
                info.RegistrationDate = DateTime.Now;
                info.RegistrationName = RegistrationName;
                info.RegistrationTel = RegistrationTel;
                info.SubordinateActivitiesID = new Guid(Id);
                info.UpdateDate = DateTime.Now;
                info.CreateDate = DateTime.Now;
                info.UserId = new Guid(UserId);
                entity.Activity_RegistrationStatus.Add(info);
                var count = entity.SaveChanges();
                if (count > 0)
                {
                    return 0;
                }
                else {
                    return 1;
                }
            }
        }

        public List<Dictionary> GetActivityTypeList(string lx) 
        {
            CupcakeEntities entity = new CupcakeEntities();
            
                var listid = entity.DataDictionary.Where(n => n.Name == lx && n.IsDelete == false).ToList();
                if (listid.Count()>0)
                {
                    var parenid = listid.FirstOrDefault().Id;
                    return entity.DataDictionary.Where(n => n.ParentId == parenid && n.IsDelete == false).Select(item => new Dictionary() { Id=item.Id,Name=item.Name,Tips=item.Tips}).ToList();
                }
                else 
                {
                    return null;
                }
        }

        public List<Activity_Activitys> GetListActivityBytype(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("  select * from [Cupcake].[dbo].[Activity_Activitys] where [ActityType]='{0}'and IsDelete=0 order by UpdateDate desc", id);
                return entity.Database.SqlQuery<Activity_Activitys>(sqlstr).ToList();
            }
        }

        public List<Activity_ActivityStyle> GetActivityStylelist()
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("  select top 5* from [Cupcake].[dbo].[Activity_ActivityStyle] where IsDelete=0 order by UpdateDate desc");
                return entity.Database.SqlQuery<Activity_ActivityStyle>(sqlstr).ToList();
            }
        
        }

        public Activity_ActivityStyle GetbyActivityStyle(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Activity_ActivityStyle.Where(p => p.Id == id && p.IsDelete == false).FirstOrDefault();
            }
        }

        public int GetByMessageManagements(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.Activity_MessageManagements.Where(p => p.IsDelete == false&&p.SubordinateActivitiesID==id).Count();
            }
        }


        public int AddMessage(string MessageName, string MessageConent, string UserId, string Id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                Activity_MessageManagements info = new Activity_MessageManagements();
                info.Id = Guid.NewGuid();
                info.MessageDate = DateTime.Now;
                info.MessageName = MessageName;
                info.MessageConent = MessageConent;
                info.SubordinateActivitiesID = new Guid(Id);
                info.UpdateDate = DateTime.Now;
                info.CreateDate = DateTime.Now;
                //info.UserId = new Guid(UserId);
                entity.Activity_MessageManagements.Add(info);
                var count = entity.SaveChanges();
                if (count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public IList<Activity_Activitys> GetListActivity(Guid? id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sql = "select * from [Cupcake].[dbo].[Activity_Activitys] where IsDelete=0";
                if(id!=null)
                {
                    sql += "and ActityType='" + id + "'";
                }
                return entity.Database.SqlQuery<Activity_Activitys>(sql).ToList();
            }
        }

        public List<Activity_Activitys> GetListActivityMap(string lng, string lat, string Type)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                int total = 0;
                decimal latv = 0;
                latv = Convert.ToDecimal(lat);
                decimal lngv = 0;
                lngv = Convert.ToDecimal(lng);
                string sql = "select *,abs(@lng-Longitude) + abs(@lat-Dimension) as LLT from [Cupcake].[dbo].[Activity_Activitys] where 1=1 and IsDelete =0  ";
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@lng", lngv));
                param.Add(new SqlParameter("@lat", latv));
                if (Type!="")
                {
                    sql += " and ActityType=@Type";
                    param.Add(new SqlParameter("@Type", Type));
                }
                return entity.Database.SqlQuery<Activity_Activitys>(sql, param.ToArray()).ToList();
            }

        }

        public IList<Activity_Activitys> SearchActivity(ActivityCondition condition, ref Paging paging)
        {
                CupcakeEntities db = new CupcakeEntities();
                Expression<Func<Activity_Activitys, bool>> where = PredicateExtensions.True<Activity_Activitys>();
                if (condition.ActityType != null)
                {
                    where = where.And(n => n.ActityType == condition.ActityType);
                }
                where = where.And(n => n.IsDelete == false);
                paging.Total = db.Activity_Activitys.Where(n=>n.ActityType==condition.ActityType).Count();
                return db.Activity_Activitys.Where(where).OrderBy(n => n.CreateDate)
                          .Skip(paging.PageSize * paging.PageIndex)
                          .Take(paging.PageSize).ToList();
        }

        //public List<Activity_MessageManagements> GetMessageManagementsList(Guid id)
        //{
        //    using (CupcakeEntities entity = new CupcakeEntities())
        //    {
        //        return entity.Activity_MessageManagements.Where(p => p.IsDelete == false && p.UserId == id).ToList();
        //    }

        //}


        //编辑
        //public bool EditNotice(Guid id, int zqdm, EnterpriseMessage model)
        //{
        //    using (CupcakeEntities2 db = new CupcakeEntities2())
        //    {
        //        Users info = db.Users.Find(id);
        //        if (info != null )
        //        {
        //            info.f_content = model.jianjie;
        //            info.f_title = model.biaoti;
        //            info.f_keyword = model.biaoqian;
        //            info.f_commenttype = model.shezhi;
        //            info.f_fileurl = model.uploadurl;
        //            return db.SaveChanges() > 0;
        //        }
        ////  //删除
        ////  db.Users.Remove(info);
        //        return false;
        //    }
        //}

      
    }
}