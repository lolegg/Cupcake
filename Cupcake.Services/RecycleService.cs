using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace Cupcake.Services
{
    public class RecycleService
    {
        public IEnumerable GetAllMenusForTree()
        {
            string sql = "SELECT d.ChildrenNumber,c.* FROM Menu c LEFT JOIN " +
                         "(SELECT a.Id, COUNT(b.ParentId) ChildrenNumber FROM Menu a LEFT JOIN Menu b on a.Id = b.ParentId GROUP BY a.Id) as d " +
                         "ON c.Id = d.Id WHERE c.IsDelete = 0";

            return new Repository<IEnumerable>().DynamicSqlQuery(sql, null);
        }
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

        public User GetSupperUser()
        {
            using (var repository = new Repository<User>())
            {
                return repository.Get(u => u.UserType == UserType.Supper);
            }
        }

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
    }
}
