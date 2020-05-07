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
    
    public partial class Users
    {
        public Users()
        {
            this.CustomPermissions = new HashSet<CustomPermissions>();
            this.DataDictionary = new HashSet<DataDictionary>();
            this.HasMedias = new HashSet<HasMedias>();
            this.HasMenus = new HashSet<HasMenus>();
            this.HasPermissions = new HashSet<HasPermissions>();
            this.Logs = new HashSet<Logs>();
            this.Medias = new HashSet<Medias>();
            this.Menu = new HashSet<Menu>();
            this.Organizations = new HashSet<Organizations>();
            this.Roles = new HashSet<Roles>();
            this.SysSetForm = new HashSet<SysSetForm>();
            this.Users1 = new HashSet<Users>();
            this.Variables = new HashSet<Variables>();
            this.Roles1 = new HashSet<Roles>();
        }
    
        public System.Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int UserType { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public int Status { get; set; }
        public Nullable<System.Guid> OrganizationId { get; set; }
        public int UARCId { get; set; }
        public int UARCOrganizationId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<System.Guid> CreatorId { get; set; }
    
        public virtual ICollection<CustomPermissions> CustomPermissions { get; set; }
        public virtual ICollection<DataDictionary> DataDictionary { get; set; }
        public virtual ICollection<HasMedias> HasMedias { get; set; }
        public virtual ICollection<HasMenus> HasMenus { get; set; }
        public virtual ICollection<HasPermissions> HasPermissions { get; set; }
        public virtual ICollection<Logs> Logs { get; set; }
        public virtual ICollection<Medias> Medias { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<Organizations> Organizations { get; set; }
        public virtual Organizations Organizations1 { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<SysSetForm> SysSetForm { get; set; }
        public virtual ICollection<Users> Users1 { get; set; }
        public virtual Users Users2 { get; set; }
        public virtual ICollection<Variables> Variables { get; set; }
        public virtual ICollection<Roles> Roles1 { get; set; }
    }
}
