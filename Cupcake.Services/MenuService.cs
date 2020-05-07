using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;

namespace Cupcake.Services
{
    public class MenuService : BaseService<Menu>
    {
        //public IList<Menu> GetBranchMenu()
        //{

        //}
        public void RemoveMenuAtRoot(string name, string href)
        {
            using (var repository = new Repository<Menu>())
            {
                string sql = @"delete Menu where Name=@name and Href=@href";
                SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@name",name),
                new SqlParameter("@href",href)
                };
                repository.ExecuteSql(sql, param);
            }
        }
        public void AddMenuAtRoot(string name, string href)
        {
            using (var repository = new Repository<Menu>())
            {
                Menu m = new Menu();
                m.Name = name;
                m.ParentId = GetRootId();
                m.Href = href;
                repository.Add(m);
                repository.Commit();
            }
        }
        public IList<Menu> GetRoleHasMenus(Guid roleId)
        {
            using (var repository = new Repository<Menu>())
            {
                string sql = @"SELECT a.* from Menu a " +
                              "LEFT JOIN HasMenus b on a.Id = b.MenuId " +
                              "LEFT JOIN Roles c on c.Id = b.OwnerId " +
                              "WHERE c.Id = @roleId " +
                              "AND c.IsDelete=0 " +
                              "AND a.IsDelete=0";
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@roleId", roleId.ToString()) };
                return repository.GetwithdbSql(sql, param).ToList();
            }
        }

        public IEnumerable GetAllMenusForTree()
        {
            string sql = "SELECT d.ChildrenNumber,c.* FROM Menu c LEFT JOIN " +
                         "(SELECT a.Id, COUNT(b.ParentId) ChildrenNumber FROM Menu a LEFT JOIN Menu b on a.Id = b.ParentId GROUP BY a.Id) as d " +
                         "ON c.Id = d.Id WHERE c.IsDelete = 0";

            return new Repository<IEnumerable>().DynamicSqlQuery(sql, null);
        }

        public List<Menu> GetAllMenus()
        {
            var query = new Repository<Menu>().TableNoTracking;

            query = query.Where(t => t.IsDelete == false);
            query = query.OrderBy(t => t.Sort);
            return query.ToList();
        }

        public bool AlreadyExists(string name, Guid parentId, Guid id)
        {
            return base.AlreadyExists(m => m.Name == name.Trim() && m.ParentId == parentId && m.Id != id && m.IsDelete == false);
        }

        public Guid GetRootId()
        {
            using (var repository = new Repository<Menu>())
            {
                var root = repository.NoTrackingDbSet.FirstOrDefault(n => n.Name == "Root");
                if (root != null)
                {
                    return root.Id;
                }
                else
                    return new Guid();
            }
        }

        public IPagedList<Menu> SearchMenus(MenuCondition condition)
        {
            var query = new Repository<Menu>().TableNoTracking;

            if (!string.IsNullOrEmpty(condition.Name))
                query = query.Where(t => t.Name.Contains(condition.Name));
            query = query.Where(t => t.ParentId == condition.NodeId);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Menu>(query, condition.PageIndex, condition.PageSize);
        }

        public IList<Menu> GetListByCondition(MenuCondition menudition, ref Paging paging)
        {
            using (var repository = new Repository<Menu>())
            {
                Expression<Func<Menu, bool>> where = PredicateExtensions.True<Menu>();
                if (!string.IsNullOrEmpty(menudition.MenuName)) where = where.And(u => u.Name == menudition.MenuName);
                if (menudition.ParentMenuID != null)
                {
                    where = where.And(u => u.ParentId == new Guid(menudition.ParentMenuID));
                }
                return repository.GetPaged(ref paging, where, m => m.Sort, false).ToList();
            }
        }


        public IList<Menu> GetListByIds(List<string> ids)
        {
            if (ids != null && ids.Count > 0)
            {
                using (var repository = new Repository<Menu>())
                {
                    Expression<Func<Menu, bool>> where = PredicateExtensions.True<Menu>();
                    foreach (string id in ids)
                    {
                        where.Or(m => m.Id.ToString() == id);
                    }
                    return repository.NoTrackingDbSet.Where(where).ToList();
                }
            }
            else
            {
                return null;
            }
        }


        public IList<Menu> GetListByRoles(List<string> roles)
        {
            if (roles != null && roles.Count > 0)
            {
                using (var repository = new Repository<Menu>())
                {
                    string sql = @" select a.* from Menu a left join rolemenus b on a.id = b.Menu_Id where b.role_id in (@roleid) ";
                    SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@roleid",string.Join(",",roles))
                   };
                    return repository.GetwithdbSql(sql, parms).ToList();
                }
            }
            else
            {
                return null;
            }
        }

        public List<Menu> GetListByRoles(List<Role> listRole)
        {
            if (listRole != null && listRole.Count > 0)
            {
                string roles = string.Join(",", listRole.ConvertAll<string>(m => m.Id.ToString()));

                using (var repository = new Repository<Menu>())
                {
                    string sql = @" select a.* from Menu a left join HasMenus b on a.id = b.MenuId where b.OwnerId in (@roleid) ";
                    SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@roleid",string.Join(",",roles))
                   };
                    return repository.GetwithdbSql(sql, parms).ToList();
                }
            }
            else
            {
                return new List<Menu>();
            }
        }

        public IEnumerable<Menu> GetListByParent(MenuCondition menudition)
        {
            using (var repository = new Repository<Menu>())
            {
                Expression<Func<Menu, bool>> where = PredicateExtensions.True<Menu>();
                where = where.And(m => m.ParentId == null);
                return repository.NoTrackingDbSet.Where(where);
            }
        }


        /// <summary>
        /// 根据父id获取菜单,id为空则默认加载2级结点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MenuExt> GetMenuById(string id)
        {
            using (var repository = new Repository<MenuExt>())
            {
                if (string.IsNullOrEmpty(id))
                {

                    string sql = @"select * from(
                                select  t2.SubMenuNums,t1.* from Menu t1,(select a.Id , COUNT(b.ParentId) SubMenuNums from Menu a left join Menu b on a.Id = b.ParentId
                                group by a.Id) as t2 where t1.Id = t2.Id
                                ) t3
                                
                                "; // where t3.ParentId is null  or t3.ParentId in(select Id from menu where ParentId is null) //异步的时候只读取到维数2

                    return repository.GetwithdbSql(sql, new SqlParameter[] { }).ToList();
                }
                else
                {
                    string sql = @"select * from(
                                select  t2.subcount,t1.* from Menu t1,(select a.Id , COUNT(b.ParentId) subcount from Menu a left join Menu b on a.Id = b.ParentId
                                 group by a.Id) as t2 where t1.Id = t2.Id
                                 ) t3
                                 where t3.ParentId =@pid";
                    SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("@pid",id)
                   };
                    return repository.GetwithdbSql(sql, sqlParams).ToList();
                }

            }
        }



        public void Update(string id, string name)
        {
            using (var repository = new Repository<Menu>())
            {
                Menu menu = repository.Get(new Guid(id));
                if (menu != null)
                {
                    menu.Name = name;
                    repository.Modify(menu);
                    repository.Commit();
                }
            }
        }



        public void Insert(Menu menu)
        {
            using (var repository = new Repository<Menu>())
            {
                repository.Add(menu);
                repository.Commit();
            }

        }
        public bool IsDuplicate(string menuName)
        {
            using (var repository = new Repository<Menu>())
            {
                var list = repository.NoTrackingDbSet.Where(m => m.Name == menuName.Trim()).ToList();
                if (list.Count == 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }
    }
}
