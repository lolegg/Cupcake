using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class RoleModel:BaseModel
    {
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }
        
        [Display(Name = "排序")]
        public int? Sort { get; set; }


        [Display(Name = "描述")]
        public string Desc { get; set; }

        public ObjectStatus Status { get; set; }


        public string StatusStr { get; set; }


        public string ParentName { get; set; }


        public string Isdeletestate { get; set; }

        public bool IsCheck { get; set; }


        internal static RoleModel FromData(Role info)
        {
            return FromData<RoleModel>(info);
        }


        internal static T FromData<T>(Role info)
        where T : RoleModel, new()
        {
            T model = new T();
            if (info.Id != null)
                model.Id = info.Id;
            if (!string.IsNullOrEmpty(info.Name))
                model.Name = info.Name;
            //model.Isdelete = info.IsDelete;
            if (info.IsDelete == true)
            {
                model.StatusStr = "禁用";
            }
            else
            {
                model.StatusStr = "启用";
            }
            return model;
        }


    }

    public class RoleListModel : ListModel<RoleModel>
    {
        public RoleListModel(IList<Role> roleList)
        {
            List = new List<RoleModel>();
            foreach (var role in roleList)
            {
                RoleModel roleModel = RoleModel.FromData(role);
                //RoleModel roleModel = Mapper.Map<RoleModel>(role);
                List.Add(roleModel);
            }
        }

        public List<RoleModel> ModelList = new List<RoleModel>();

        /// <summary>
        /// 栏目
        /// </summary>
        [Display(Name = "栏目")]
        public string Name { get; set; } 

        public string Isdelete { get; set; }

        public Guid UserId { get; set; }
    }


}