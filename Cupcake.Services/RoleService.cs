using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Cupcake.Services
{
    public class RoleService : BaseService<Role>
    {
        public IPagedList<Role> SearchRoles(RoleCondition condition)
        {
            var query = new Repository<Role>().Table;

            if (!string.IsNullOrEmpty(condition.Name))
                query = query.Where(t => t.Name.Contains(condition.Name));
            if (condition.BeginDate != null)
                query = query.Where(u => u.CreateDate >= condition.BeginDate);
            if (condition.EndDate != null)
                query = query.Where(u => u.CreateDate <= condition.EndDate);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Role>(query, condition.PageIndex, condition.PageSize);
        }

        public bool AlreadyExists(string roleName, Guid id)
        {
            return base.AlreadyExists(m => m.Name == roleName.Trim() && m.Id != id && m.IsDelete == false);
        }

        public void SetRoleHasMenus(Guid roleId, List<Guid> menuIds)
        {
            using (var repository = new Repository<HasMenus>())
            {
                //clear old hasMenus
                var oldHasMenus = repository.Table.Where(hm => hm.OwnerId == roleId).ToList();
                oldHasMenus.ForEach(oldHasMenu => { repository.Remove(oldHasMenu); });
                //add new hasMenus
                menuIds.ForEach(menuId =>
                {
                    var hasMenu = new HasMenus()
                    {
                        OwnerId = roleId,
                        MenuId = menuId
                    };
                    repository.Add(hasMenu);
                });
                repository.Commit();
            }
        }

        public IList<Role> GetRoleByUserName(string userName)
        {

            using (var repository = new Repository<Role>())
            {
                string sql = @"	select a.* from Roles a join UserRoles b on a.Id= b.RoleId join Users c on c.Id= b.UserId
	                     where c.UserName =@UserName";
                SqlParameter[] parameters = {
                        new SqlParameter("@userName",SqlDbType.NVarChar)};
                parameters[0].Value = userName;
                return repository.GetwithdbSql(sql, parameters).ToList();
            }

        }

        public IList<Role> GetAllRole()
        {

            using (var repository = new Repository<Role>())
            {
                string sql = @"	select a.* from Roles a join UserRoles b on a.Id= b.RoleId join Users c on c.Id= b.UserId";

                SqlParameter[] parameters = { };
                return repository.GetwithdbSql(sql, parameters).ToList();
            }

        }
    }
}
