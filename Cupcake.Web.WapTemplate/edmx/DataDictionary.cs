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
    
    public partial class DataDictionary
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int Sort { get; set; }
        public string Tips { get; set; }
        public Nullable<int> CustomType { get; set; }
        public string CustomAttributes { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.Guid> CreatorId { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
