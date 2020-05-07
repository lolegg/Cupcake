using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class AccountServices : BaseService<Members_Members>
    {
        public Members_Members LoginTempPwd(Guid? userId, string tempPassword)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                Guid statusid = DataDictionaryHelper.GetId("用户状态>启用");
                return entity.Members_Members.Where(u => u.Id == userId && u.Password == tempPassword && u.IsDelete == false && u.Status == statusid).SingleOrDefault();
            }
        }

        /// <summary>
        /// 登录次数+1
        /// </summary>
        /// <param name="id"></param>
        public void UpdateLogin(Members_Members infoold)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var info = entity.Members_Members.Where(n => n.Id == infoold.Id).FirstOrDefault();
                if (info != null)
                {
                    info.LoginCount += 1;
                    info.TempPwd = infoold.TempPwd;
                    info.LastLoginIP = infoold.LastLoginIP;
                    info.LastLoginDate = DateTime.Now;
                    info.failLoginCount = 0;
                   
                    var count = entity.SaveChanges();
                  
                }
            }

        }

        public void UpdatePassword(Guid infoId, string  password)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var info = entity.Members_Members.Where(n => n.Id == infoId).FirstOrDefault();
                if (info != null)
                {
                    info.Password = password;
                    info.failLoginCount = 0;

                    var count = entity.SaveChanges();

                }
            }

        }
        public Members_Members Verify(string userName, string password)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                Guid statusid = DataDictionaryHelper.GetId("用户状态>启用");
                return entity.Members_Members.Where(u => u.UserName == userName && u.Password == password && u.failLoginCount <= 15 && u.Status == statusid && u.IsDelete == false).SingleOrDefault();
            }
        }

        //public Members_Members Get(Expression<Func<Members_Members, bool>> filterExpression)
        //{
        //    using (CupcakeEntities entity = new CupcakeEntities())
        //    {
        //        return entity.Members_Members.Where(filterExpression).SingleOrDefault();
        //    }
        //}

        public Members_Members AddReturn1(Members_Members info)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                 Guid statusid = DataDictionaryHelper.GetId("用户状态>启用");
                 Guid usertypesid = DataDictionaryHelper.GetId("用户类型>普通用户");
                info.UpdateDate = DateTime.Now;
                info.CreateDate = DateTime.Now;
                info.Status = statusid;
                info.UserType = usertypesid;
                info.IsDelete = false;
                info.Id = Guid.NewGuid();
                entity.Members_Members.Add(info);
                return entity.Members_Members.Where(p => p.Id == info.Id).SingleOrDefault();
            }
        }
    }
}