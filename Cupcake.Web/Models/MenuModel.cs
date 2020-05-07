using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class MenuModel : BaseModel
    {
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "上级菜单")]
        public string ParentMenu { get; set; }

        [Display(Name = "排序")]
        public int Sort { get; set; }


        [Display(Name = "链接")]
        public string MenuHref { get; set; }


        [Display(Name = "图标样式")]
        public string MenClass { get; set; }


        public string ParentMenuName { get; set; }

        public Guid ParentId { get; set; }
        [Display(Name = "链接")]
        public string Href { get; set; }
        [Display(Name = "图标样式")]
        public string ClassName { get; set; }


        [Display(Name = "状态")]
        public string IsDeleteState { get; set; }

        public Menu ToMenu()
        {
            return Mapper.Map<Menu>(this);
        }


        internal static MenuModel FromData(Menu info)
        {
            return FromData<MenuModel>(info);
        }


        internal static T FromData<T>(Menu info)
        where T : MenuModel, new()
        {
            T model = new T();
            if (info.Id != null)
                model.Id = info.Id;
            model.Sort = info.Sort;
            if (!string.IsNullOrEmpty(info.Name))
                model.Name = info.Name;
            if (!string.IsNullOrEmpty(info.ClassName))
                model.MenClass = info.ClassName;
            if (!string.IsNullOrEmpty(info.Href))
                model.MenuHref = info.Href;
            if (info.ParentId != null)
                model.ParentMenu = info.ParentId.ToString();
            if (info.IsDelete == true)
                model.IsDeleteState = "禁用";
            else
                model.IsDeleteState = "启用";
            if (info.UpdateDate != null)
                model.UpdateDate = info.UpdateDate;//.ToString("yyyy-MM-dd HH:mm");
            return model;
        }


    }

    public class MenuListModel : ListModel<MenuModel>
    {
        public MenuListModel(IList<Menu> menuList)
        {
            List = new List<MenuModel>();
            foreach (var muen in menuList)
            {
                MenuModel muenModel = MenuModel.FromData(muen);
                //MuenModel muenModel = Mapper.Map<MuenModel>(muen);
                List.Add(muenModel);
            }
        }

        /// <summary>
        /// 栏目
        /// </summary>
        [Display(Name = "栏目")]
        public string ParentMenu { get; set; }


        [Display(Name = "栏目名称")]
        public string Name { get; set; }

        public IList<MenuModel> GetParentList = new List<MenuModel>();


        public string ParentMenuName { get; set; }



        [Display(Name = "更新时间")]
        public DateTime UpdateDate { get; set; }


        [Display(Name = "状态")]
        public string IsDeleteState { get; set; }

    }
    public class NodeModel
    {
        public NodeModel()
        {
            nodes = new List<NodeModel>();
        }
        public string text { get; set; }
        public string id { get; set; }
        public IList<NodeModel> nodes { get; set; }
    }
    public class MenuTreeModel
    {
        public MenuTreeModel()
        {
            Children = new List<MenuTreeModel>();
            OpenClass = "";
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string IconClass { get; set; }
        public bool IsActive { get; set; }

        public string OpenClass { get; set; }

        public string ActiveClass { get; set; }
        public IList<MenuTreeModel> Children { get; set; }
    }
}