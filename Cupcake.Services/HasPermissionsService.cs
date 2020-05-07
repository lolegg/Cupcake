using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cupcake.Services
{
    public class HasPermissionsService : BaseService<HasPermissions>
    {
        public List<HasPermissions> GetRoleHasPermissions(Guid roleId, Guid menuId)
        {
            var query = new Repository<HasPermissions>().Table;
            query = query.Where(t => t.OwnerId == roleId);
            query = query.Where(t => t.MenuId == menuId);
            query = query.Where(t => t.IsDelete == false);
            return query.ToList();
        }

        public void SetRoleHasPermissions(Guid roleId, Guid menuId, List<Guid> selectedNodeIds)
        {
            //清空该角色权限
            var hasPermissionsRepository = new Repository<HasPermissions>();
            var hasPermissions = hasPermissionsRepository.Table.Where(p => p.OwnerId == roleId && p.MenuId == menuId).ToList();
            hasPermissions.ForEach(p =>
            {
                hasPermissionsRepository.Remove(p);
            });

            if (selectedNodeIds != null && selectedNodeIds.Count > 0)
            {
                selectedNodeIds.ForEach(p =>
                {
                    hasPermissionsRepository.Add(new HasPermissions()
                    {
                        OwnerId = roleId,
                        MenuId = menuId,
                        PermissionId = p
                    });
                });
            }
            hasPermissionsRepository.Commit();
        }

        public bool PermissionUsed(Guid permissionId)
        {
            var query = new Repository<HasPermissions>().TableNoTracking;
            return query.Any(t => t.PermissionId == permissionId && t.IsDelete == false);
        }

        public bool HasPermission(Guid menuId, Guid permissionId, List<Guid> roleIds)
        {
            var query = new Repository<HasPermissions>().Table;
            return query.Any(t => roleIds.Contains(t.OwnerId) && t.MenuId == menuId && t.PermissionId == permissionId);
        }
    }
}
