using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class CustomPermissionsModel : BaseModel
    {

        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Desc { get; set; }
        public Guid? HasPermission { get; set; }

        public Guid? MenuId { get; set; }
        public CustomPermissions ToPermission()
        {
            return Mapper.Map<CustomPermissions>(this);
        }


    }

    public class BasePermissionListModel : ListModel<CustomPermissionsModel>
    {
        public Guid RoleId { get; set; }
        public string CurrentMenuName { get; set; }
        public Guid CurrentMenuId { get; set; }

       

        public BasePermissionListModel(IList<CustomPermissions> permissionList)
        {
            List = new List<CustomPermissionsModel>();
            foreach (var permission in permissionList)
            {
                CustomPermissionsModel permissionModel = Mapper.Map<CustomPermissionsModel>(permission);
                List.Add(permissionModel);
            }
        }

        public BasePermissionListModel(IList<BasePermissionExt> permissionList)
        {
            List = new List<CustomPermissionsModel>();
            foreach (var permission in permissionList)
            {
                CustomPermissionsModel permissionModel = Mapper.Map<CustomPermissionsModel>(permission);
                List.Add(permissionModel);
            }
        }


        public CustomPermissionsCondition condition { get; set; }
    }
}