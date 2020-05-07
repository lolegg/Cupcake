using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class Menu : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Href { get; set; }
        public string ClassName { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class MenuExt : FrameworkBaseEntity
    {
        public string Name { get; set; }
        public int? Sort { get; set; }
        public string Href { get; set; }
        public string ClassName { get; set; }
        public Guid? ParentId { get; set; }
        public int SubMenuNums { get; set; }
    }


    public class SysMenu
    {
        /// <summary>
        /// 一级栏目
        /// </summary>
        public Menu ParentMenu { get; set; }

        /// <summary>
        /// 二级栏目
        /// </summary>
        public IList<Menu> SubMenu { get; set; }

        /// <summary>
        /// 三级栏目
        /// </summary>
        public IList<Menu> ThreeMenu { get; set; }
    }

    public class MenuCondition : Condition
    {
        public string MenuName { get; set; }
        public string ParentMenuID { get; set; }

        public Guid GetParentMenuID { get; set; }
        public string Name { get; set; }
        public Guid NodeId { get; set; }
    }
}
