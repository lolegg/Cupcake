//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cupcake.Web.WapTemplate.edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomPermissions
    {
        public CustomPermissions()
        {
            this.Roles = new HashSet<Roles>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public Nullable<System.Guid> MenuId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.Guid> CreatorId { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
    }
}