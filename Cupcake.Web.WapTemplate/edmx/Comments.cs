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
    
    public partial class Comments
    {
        public System.Guid Id { get; set; }
        public string ImgUrl { get; set; }
        public System.Guid UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public System.DateTime PublicDate { get; set; }
        public System.Guid? ChildId { get; set; }
        public string MessageName { get; set; }
        public System.Guid TypeChildId { get; set; }
        public int PraiseNum { get; set; }
        public int ReplyNum { get; set; }
        public Nullable<System.Guid> ToCommentsId { get; set; }
        public Nullable<System.Guid> ToTwoCommentsId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.Guid> CreatorId { get; set; }
    }
}