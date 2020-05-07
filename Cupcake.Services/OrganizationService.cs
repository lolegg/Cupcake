using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Cupcake.Services
{
    public class OrganizationService : BaseService<Organization>
    {
        public IList<Organization> GetListByCondition(OrganizationCondition usercondition, ref Paging paging)
        {
            using (var repository = new Repository<Organization>())
            {
                Expression<Func<Organization, bool>> where = PredicateExtensions.True<Organization>();
                if (!string.IsNullOrEmpty(usercondition.Name)) where = where.And(u => u.Name.Contains(usercondition.Name));
                if (usercondition.Type != null)
                {
                    where = where.And(u => u.Type == usercondition.Type);
                }
                if (usercondition.BeginDate != null) where = where.And(u => u.CreateDate >= usercondition.BeginDate);
                if (usercondition.EndDate != null) where = where.And(u => u.CreateDate <= usercondition.EndDate);

                return repository.GetPaged(ref paging, where, m => m.CreateDate).ToList();
            }
        }
        public User Verify(string userName, string password)
        {
            using (var repository = new Repository<User>())
            {
                return repository.Get(u => u.UserName == userName && u.Password == password);
            }
        }
        public User GetUserByName(string name)
        {
            using (var repository = new Repository<User>())
            {
                return repository.Get(u => u.UserName == name);
            }
        }
        public bool IsDuplicate(string name)
        {
            return base.AlreadyExists(m => m.Name == name.Trim());
        }


        public void Update(string id, string name)
        {
            using (var repository = new Repository<Organization>())
            {
                Organization model = repository.Get(Guid.Parse(id));
                model.Name = name;
                repository.Modify(model);
                repository.Commit();
            }
        }

        public void UpdateByUarcId(int id, string name)
        {
            using (var repository = new Repository<Organization>())
            {
                Organization model = repository.Get(m => m.UARCId == id);
                model.Name = name;
                repository.Modify(model);
                repository.Commit();
            }
        }

        public Organization GetByUarcId(int id)
        {
            using (var repository = new Repository<Organization>())
            {
                return repository.Get(m => m.UARCId == id);
            }
        }

        public void Insert(Organization organization)
        {
            using (var repository = new Repository<Organization>())
            {
                repository.Add(organization);
                repository.Commit();
            }

        }


        public List<OrganizationExt> GetOrganizationById(string id)
        {
            using (var repository = new Repository<OrganizationExt>())
            {
                if (string.IsNullOrEmpty(id))
                {

                    string sql = @"select * from(select  t2.SubMenuNums,t1.* from Organizations t1,
                                    (select a.Id , COUNT(b.UARCPId) SubMenuNums from Organizations a left join Organizations b on a.UARCId = b.UARCPId
                                    group by a.Id) as t2 where t1.Id = t2.Id) t3
                                "; // where t3.ParentId is null  or t3.ParentId in(select Id from menu where ParentId is null) //异步的时候只读取到维数2

                    return repository.GetwithdbSql(sql, new SqlParameter[] { }).ToList();
                }
                else
                {
                    string sql = @"select * from(select  t2.SubMenuNums,t1.* from Organizations t1,
                                    (select a.Id , COUNT(b.UARCPId) SubMenuNums from Organizations a left join Organizations b on a.UARCId = b.UARCPId
                                    group by a.Id) as t2 where t1.Id = t2.Id) t3";
                    
                     Guid g = Guid.Empty;
                     if (Guid.TryParse(id, out g))
                     {
                         sql += " where t3.Pid =@pid";
                     }
                     else {
                         sql += " where t3.UARCId =@pid";
                     }

                                 //
                    SqlParameter[] sqlParams = new SqlParameter[]{
                    new SqlParameter("@pid",id)
                   };
                    return repository.GetwithdbSql(sql, sqlParams).ToList();
                }

            }
        }

        public void AssignRoles(Guid userId, IList<Guid> selectedIds)
        {
            //var context = new EFContext();
            //var r = context.Set<User>();
            //var mo = r.Find(userId);
            //mo.Roles.Clear();
            ////var roleRep = new Repository<Role>(context);
            ////var permissionRep = new Repository<Permission>(context);
            ////Role role = roleRep.Get(roleId);
            ////role.Permissions = new List<Permission>();
            //var pset = context.Set<Role>();
            //foreach (var Id in selectedIds)
            //{
            //    Role p = pset.Find(Id);
            //    mo.Roles.Add(p);
            //}
            ////r.Modify(mo);
            ////r.Commit();
            //context.SaveChanges();
        }


        public void SaveUARCOrganization(List<Organization> list)
        {
            using (var repository = new Repository<Organization>())
            {
                try
                {
                    foreach (Organization item in list)
                    {

                        Organization dbOrg = repository.Get(m => m.UARCId == item.UARCId);
                        if (dbOrg == null)
                        {
                            repository.Add(item);
                        }
                        else
                        {
                            dbOrg.Name = item.Name;
                            dbOrg.Type = item.Type;
                            dbOrg.UARCPId = item.UARCPId;
                            repository.Modify(dbOrg);
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

        public void Delete(string id) {
            using (var repository = new Repository<Organization>())
            {
                Organization model = repository.Get(Guid.Parse(id));
                repository.Remove(model);
                repository.Commit();
            }
        }

        public void DeleteByUarcId(string id) {
            using (var repository = new Repository<Organization>())
            {
                int uid = int.Parse(id);
                Organization model = repository.Get(m => m.UARCId == uid);
                repository.Remove(model);
                repository.Commit();
            }
        }
    }
}
