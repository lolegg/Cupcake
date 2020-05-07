using Cupcake.Core.Domain;
using Cupcake.Data;
using Cupcake.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cupcake.Services
{
    public class MenuPermissionService : BaseService<HasPermissions>
    {
        public void Add(string menuid, string roleid, List<string> permission)
        {

            using (var repository = new Repository<HasPermissions>())
            {
                string sql = "delete  MenuPermission  where roleid=@roleid and menuid =@menuid";

                SqlParameter[] parms = new SqlParameter[]{
                                new SqlParameter("@roleid",roleid),
                                new SqlParameter("@menuid",menuid),
                            };
                repository.ExecuteSql(sql, parms);
                foreach (string item in permission)
                {
                    HasPermissions model = new HasPermissions();
                    model.MenuId = new Guid(menuid);
                    //model.RoleId = new Guid(roleid);
                    model.PermissionId = new Guid(item);
                    repository.Add(model);
                    repository.Commit();
                }
            }
        }
    }
}
