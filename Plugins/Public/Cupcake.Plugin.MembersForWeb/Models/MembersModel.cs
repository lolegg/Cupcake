using Cupcake.Core.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.MembersForWeb.Models
{
    public class MembersModel : BaseModel
    {
        [Required]
        [Display(Name = "用户名")]
       
        public string UserName { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Display(Name = "确认密码")]
        public string Password2 { get; set; }
        [Required]
        [Display(Name = "昵称")]
        public string UserRealName { get; set; }
       
        [Display(Name = "电话")]
        public string Tel { get; set; }
        [Required]
        [Display(Name = "用户类型")]
        public Guid UserType { get; set; }
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "用户头像")]
        public string ImageUrl { get; set; }
        [Display(Name = "用户状态")]
        public Guid Status { get; set; }

        public string TempPwd { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int LoginCount { get; set; }

        public string LastLoginIP { get; set; }
      

        public string ValidCode { get; set; }

        public bool Remember { get; set; }
        public int failLoginCount { get; set; }
    }
}
