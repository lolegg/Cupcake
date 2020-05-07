using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cupcake.Services
{
    public class CustomPermissionsService : BaseService<CustomPermissions>
    {
        //public IList<CustomPermissionsService> GetList(CustomPermissionsCondition condition, ref Paging paging)
        //{
        //    using (var repository = new Repository<CustomPermissionsService>())
        //    {
        //        Expression<Func<CustomPermissionsService, bool>> where = PredicateExtensions.True<CustomPermissionsService>();

        //        if (!string.IsNullOrEmpty(condition.Name))
        //        {
        //            where = where.And(p => p.Name.Contains(condition.Name));
        //        }

        //        if (!string.IsNullOrEmpty(condition.Desc))
        //        {
        //            where = where.And(p => p.Desc.Contains(condition.Desc));
        //        }

        //        return repository.GetPaged(ref paging, where, m => m.CreateDate).ToList();
        //    }
        //}

        public List<BasePermissionExt> GetList(string roleid, string menuid)
        {
            using (var repository = new Repository<BasePermissionExt>())
            {
                string sql = @"select * from ( select a.*,b.id HasPermission from BasePermission  a left join (select * from MenuPermission where roleid=@roleid and menuid=@menuid )b on a.Id = b.PermissionId ) t  where t.menuid = @menuid2  or t.menuid is null  order by t.menuid";
                SqlParameter[] parms = new SqlParameter[]{
                    new SqlParameter("@roleid",roleid),
                    new SqlParameter("@menuid",menuid),
                    new SqlParameter("@menuid2",menuid),
                    
                };
                return repository.GetwithdbSql(sql, parms).ToList();
            }



        }

        public List<CustomPermissions> GetCustomPermissionsByMenu(Guid menuId)
        {
            var query = new Repository<CustomPermissions>().TableNoTracking;
            query = query.Where(p => p.MenuId == menuId);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);
            return query.ToList();
        }

        public CustomPermissions GetCustomPermissions(Guid menuId, string name)
        {
            var query = new Repository<CustomPermissions>().TableNoTracking;
            query = query.Where(p => p.MenuId == menuId && p.Name == name);
            query = query.Where(t => t.IsDelete == false);
            return query.FirstOrDefault(p => p.MenuId == menuId && p.Name == name && p.IsDelete == false);
        }

        public IPagedList<CustomPermissions> GetCustomPermissionsByMenu(CustomPermissionsCondition condition)
        {
            var query = new Repository<CustomPermissions>().Table;

            if (!string.IsNullOrEmpty(condition.Name))
                query = query.Where(t => t.Name.Contains(condition.Name));
            query = query.Where(t => t.MenuId == condition.NodeId);
            query = query.Where(t => t.IsDelete == false);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<CustomPermissions>(query, condition.PageIndex, condition.PageSize);
        }
        public List<CustomPermissions> GetListByMenuId(CustomPermissionsCondition condition, ref Paging paging)
        {
            using (var repository = new Repository<CustomPermissions>())
            {
                Expression<Func<CustomPermissions, bool>> where = PredicateExtensions.True<CustomPermissions>();
                if (condition.NodeId != null)
                {
                    where = where.And(p => p.MenuId == condition.NodeId).Or(p => p.MenuId == null);
                }

                if (!string.IsNullOrEmpty(condition.Name))
                {
                    where = where.And(p => p.Name.Contains(condition.Name));
                }

                if (!string.IsNullOrEmpty(condition.Desc))
                {
                    where = where.And(p => p.Desc.Contains(condition.Desc));
                }

                return repository.GetPaged(ref paging, where, m => m.MenuId, true).ToList();
            }
        }

        public bool AlreadyExists(string name, Guid? menuId, Guid id)
        {
            if (menuId.HasValue)
            {
                return base.AlreadyExists(m => m.Name == name.Trim() && m.Id != id && m.MenuId == menuId && m.IsDelete == false);
            }
            else
            {
                return base.AlreadyExists(m => m.Name == name.Trim() && m.Id != id && m.IsDelete == false);
            }
        }

        public void one2many()
        {
            ////var context = new EFContext();
            //var repository = new Repository<Menu>();
            //var m = repository.Get(new Guid("53635368-86c8-e511-b550-002564ba5c19"));
            //var repository2 = new Repository<BasePermission>();
            //BasePermission model = new BasePermission() { Name = "ddd", Desc = "df" };
            //model.Menu = m;
            //repository2.Add(model);
            //repository2.Commit();
        }

        public void one2many2()
        {
            ////var context = new EFContext();
            ////var repository = new Repository<Menu>(context);
            ////var m = repository.Get(new Guid("53635368-86c8-e511-b550-002564ba5c19"));
            //var repository2 = new Repository<Permission>();
            //Permission model = new Permission() { Name = "ddd2", Desc = "df2" };
            //model.MenuId = new Guid("58D50F87-90C8-E511-B550-002564BA5C19");
            //repository2.Add(model);
            //repository2.Commit();
        }

        public void many2many()
        {
            ////var context = new EFContext();
            //var roleRep = new Repository<Role>();
            //var role = new Role() { Name = "总干事" };
            //role.Permissions = new List<Permission>();
            //for (int i = 0; i < 5; i++)
            //{
            //    Permission model = new Permission() { Name = "ddd" + i, Desc = "df", MenuId = new Guid("2579B781-B5C8-E511-B550-002564BA5C19") };
            //    role.Permissions.Add(model);
            //}

            //roleRep.Add(role);
            //roleRep.Commit();

        }

        public void AssignPermissions(Guid roleId, IList<Guid> selectedIds)
        {
            ////var context = new EFContext();
            ////var r = context.Set<Role>();
            //var r = new Repository<Role>();
            //var mo = r.Get(roleId);
            //mo.Permissions.Clear();
            ////var roleRep = new Repository<Role>(context);
            ////var permissionRep = new Repository<Permission>(context);
            ////Role role = roleRep.Get(roleId);
            ////role.Permissions = new List<Permission>();
            ////var pset = context.Set<Permission>();
            //var pset = new Repository<Permission>();
            //foreach (var Id in selectedIds)
            //{
            //    Permission p = pset.Get(Id);
            //    mo.Permissions.Add(p);
            //}
            ////r.Modify(mo);
            //r.Commit();
            ////context.SaveChanges();
        }

        public IList<string> GetPermissionsByUserName(string userName)
        {
            using (var repository = new Repository<string>())
            {
                //公共权限现在有问题 暂时不加
                string sql = @"select distinct Name from BasePermission
                            where id in
                            (
                            select * from (
                            select Distinct(MP.PermissionId) from
                            (
	                            select a.* from Roles a join RoleUsers b on a.Id= b.Role_Id join Users c on c.Id= b.User_Id
	                            where c.UserName =@userName
                            ) TROLES
                            JOIN 
                            MenuPermission MP on TROLES.Id= MP.RoleId)
                            TPermission
                            ) 
                        ";
                SqlParameter[] parameters = {
                        new SqlParameter("@userName",SqlDbType.NVarChar)};
                parameters[0].Value = userName;
                return repository.GetwithdbSql(sql, parameters).ToList();
            }
        }
        public IList<SysMenu> GetListByMenu(string uid, bool IsSuperManage = false)
        {
            IList<SysMenu> list = new List<SysMenu>();
            try
            {
                IEnumerable<Menu> listmenu = new List<Menu>();
                if (IsSuperManage == true)
                {
                    Repository<Menu> rpository = new Repository<Menu>();
                    listmenu = rpository.NoTrackingDbSet.Where(m => m.IsDelete == false).ToList();

                }
                else
                {
                    Repository<Menu> rpository = new Repository<Menu>();
                    SqlParameter[] parm = { new SqlParameter("@userid", uid) };
                    listmenu = rpository.GetWithRawSql("select * from dbo.Menu where  Id in(select pm.MenuId from  dbo.PermissionRoles pmr left join Permission pm on pmr.Permission_Id=pm.Id inner join  dbo.RoleUsers ru on  pmr.Role_Id=ru.Role_Id where ru.User_Id=@userid)", parm);
                }
                if (listmenu != null && listmenu.Count() > 0)
                {
                    IList<Menu> newlistmenu = new List<Menu>();
                    foreach (Menu item in listmenu)
                    {
                        newlistmenu.Add(item);
                    }

                    //一级栏目
                    foreach (Menu info in newlistmenu)
                    {
                        Menu parentMenu = new Menu();
                        if (info.ParentId == null)
                        {
                            parentMenu = info;
                            IList<Menu> chirldList = new List<Menu>();
                            IList<Menu> threeList = new List<Menu>();
                            //二级栏目
                            foreach (Menu infochirld in newlistmenu)
                            {
                                if (info.Id == infochirld.ParentId)
                                {
                                    Menu chiledMenu = new Menu();
                                    chiledMenu = infochirld;
                                    chirldList.Add(chiledMenu);
                                }
                            }
                            if (chirldList != null && chirldList.Count > 0)
                            {
                                //三级栏目
                                foreach (Menu infothree in chirldList)
                                {

                                    if (info.ParentId == infothree.Id)
                                    {
                                        Menu threeMenu = new Menu();
                                        threeMenu = info;
                                        threeList.Add(threeMenu);
                                    }

                                }
                            }
                            list.Add(new SysMenu
                            {
                                ParentMenu = parentMenu,
                                SubMenu = chirldList,
                                ThreeMenu = threeList
                            });
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

    }
}
