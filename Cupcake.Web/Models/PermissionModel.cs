using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Models
{
    public class PermissionModel:BaseModel
    {
        //public UserModel()
        //{
        //    //Mapper.Map<UserViewModel>(user);
        //}
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Desc { get; set; }
        public Guid MenuId { get; set; }

        //public Permission ToPermission()
        //{
        //    return Mapper.Map<Permission>(this);
        //}
        //public PermissionModel ToPermissionModel(Permission permission)
        //{
        //    return Mapper.Map<PermissionModel>(permission);
        //}        
    }

    public class PermissionListModel : ListModel<PermissionModel>
    {
        public Guid RoleId { get; set; }
        public string CurrentMenuName { get; set; }
        public Guid CurrentMenuId { get; set; }
        //public PermissionListModel(IList<Permission> permissionList)
        //{
        //    List = new List<PermissionModel>();
        //    foreach (var permission in permissionList)
        //    {
        //        PermissionModel permissionModel = Mapper.Map<PermissionModel>(permission);
        //        List.Add(permissionModel);
        //    }
        //}

        //public PermissionCondition condition { get; set; }
    }
}