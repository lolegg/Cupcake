using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Cupcake.Services
{
    public class UserService : BaseService<User>
    {
        public IPagedList<User> SearchUsers(UserCondition condition)
        {
            var query = new Repository<User>().Table;

            if (!string.IsNullOrEmpty(condition.UserName))
                query = query.Where(t => t.UserName.Contains(condition.UserName));
            if (condition.UserType != null)
                query = query.Where(t => t.UserType == condition.UserType);
            if (condition.BeginDate != null)
                query = query.Where(u => u.CreateDate >= condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(u => u.CreateDate <= condition.EndDate);
            query = query.Where(t => t.IsDelete == false);
            query = query.Where(t => t.UserType != UserType.Supper);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<User>(query, condition.PageIndex, condition.PageSize);
        }

        public bool AlreadyExists(string userName, Guid id)
        {
            return base.AlreadyExists(m => m.UserName == userName.Trim() && m.Id != id && m.IsDelete == false);
        }

        public User GetSupperUser()
        {
            using (var repository = new Repository<User>())
            {
                return repository.Get(u => u.UserType == UserType.Supper);
            }
        }
        //public List<User> GetList(string userid)
        //{
        //    using (var repository = new Repository<User>())
        //    {
        //        string sql = "select * from Users  a left join RoleUsers b on a.Id = b.User_Id right join Roles c on c.Id = b.Role_Id where a.Id =@id";
        //        SqlParameter[] paras = new SqlParameter[]{
        //             new SqlParameter("@id",userid)
        //        };
        //        return repository.GetwithdbSql(sql, paras).ToList();
        //    }
        //    return null;
        //}

        public IList<Role> GetList(Guid userid)
        {
            //string sql = "select c.* from Users a left join RoleUsers b on a.Id = b.User_Id right join Roles c on c.Id = b.Role_Id where a.Id = @id";
            //SqlParameter[] paras = new SqlParameter[] {
            //    new SqlParameter("@id",userid)
            //};
            //return repository.GetwithdbSql(sql, paras).ToList();
            using (var usersRepository = new Repository<User>())
            {
                var list = usersRepository.Get(userid);
                return list.Roles;
            }

        }

        public void test()
        {
            using (var repository = new Repository<User>())
            {
                //repository.BatchUpdate(u => u.UserName.Contains("sdf"), nu => new User() { UserName = "batchname" });

                //repository.BatchDelete(u => u.UserName == "batchname");

                //var a = repository.Get(new Guid("367F8462-3AE0-E511-98A8-002564BA5C19"));
                //a.LastLoginDate = DateTime.Now;
                //repository.UpdateSpecifiedAttribute(a, "LastLoginDate");
                //try
                //{
                //    repository.UpdateSpecifiedAttribute(new User() { Id = new Guid("367F8462-3AE0-E511-98A8-002564BA5C19"), LastLoginDate = DateTime.Now }, "LastLoginDate");
                //    repository.Commit();
                //}
                //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                //{

                //}
                SqlParameter[] parameters = {
                    new SqlParameter("@userName",SqlDbType.NVarChar)};
                parameters[0].Value = "cxy23";

                var a = repository.DynamicSqlQuery("select * from Users", null);


                repository.Commit();
            }
        }
        public User Verify(string userName, string password)
        {
            using (var repository = new Repository<User>())
            {
                return repository.Get(u => u.UserName == userName && u.Password == password);
            }
        }

        public IList<User> GetListUaRcallCondition(UserCondition usercondition, ref Paging paging, string UARCOrganizationId)
        {
            using (var repository = new Repository<User>())
            {
                Expression<Func<User, bool>> where = PredicateExtensions.True<User>();
                if (!string.IsNullOrEmpty(usercondition.UserName)) where = where.And(u => u.UserName == usercondition.UserName);
                if (usercondition.UserType != null)
                {
                    where = where.And(u => u.UserType == usercondition.UserType);
                }
                if (usercondition.BeginDate != null) where = where.And(u => u.CreateDate >= usercondition.BeginDate);
                if (usercondition.EndDate != null) where = where.And(u => u.CreateDate <= usercondition.EndDate);

                Guid g = Guid.Empty;
                if (Guid.TryParse(UARCOrganizationId, out g))
                {
                    where = where.And(u => u.OrganizationId == g);
                }
                else
                {
                    int orgid = int.Parse(UARCOrganizationId);
                    where = where.And(u => u.UARCOrganizationId == orgid);
                }


                return repository.GetPaged(ref paging, where, m => m.CreateDate).ToList();
            }
        }



        public User GetUserByName(string name)
        {
            using (var repository = new Repository<User>())
            {
                return repository.Get(u => u.UserName == name);
            }
        }

        public void AssignRoles(Guid userId, IList<Guid> selectedRoleIds)
        {
            //清空该用户角色
            var userRepository = new Repository<User>();
            var user = userRepository.Get(userId);
            if (user == null || user.IsDelete == true)
            {
                throw new NopException("用户不存在或已禁用");
            }
            if (user.Roles != null)
            {
                user.Roles.Clear();
            }

            if (selectedRoleIds != null && selectedRoleIds.Count > 0)
            {
                var roleRepository = new Repository<Role>();
                var selectRoles = roleRepository.Table.Where(m => selectedRoleIds.Contains(m.Id)).ToList();
                foreach (var item in selectRoles)
                {
                    user.Roles.Add(item);
                }
            }
            userRepository.Commit();
        }


        public void SaveUARCUser(List<User> list)
        {
            using (var repository = new Repository<User>())
            {
                try
                {
                    foreach (User item in list)
                    {

                        User dbUser = repository.Get(m => m.UARCId == item.UARCId);

                        if (dbUser == null)
                        {
                            repository.Add(item);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(dbUser.Password))
                            {
                                dbUser.Password = item.Password;
                            }
                            dbUser.Name = item.Name;
                            dbUser.UserName = item.UserName;
                            dbUser.UserType = item.UserType;
                            dbUser.UARCOrganizationId = item.UARCOrganizationId;
                            dbUser.Status = item.Status;
                            repository.Modify(dbUser);
                        }
                    }
                    repository.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

        }



    }
}
