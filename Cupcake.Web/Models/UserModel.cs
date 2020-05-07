using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.Models
{
    public class UserModel : BaseModel
    {
        //[NotEqualTo("UserName", ErrorMessage = "不能与用户名的值相同")]
        //[NewPhoneMobile]
        //[NewEmail]
        //[NewIDCard]
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        public string UserTypeDesc { get; set; }
        [Required]
        [Display(Name = "用户类型")]
        public UserType UserType { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string StatusDesc { get; set; }
        [Display(Name = "用户状态")]
        public ObjectStatus Status { get; set; }

        public string NodeId { get; set; }
        public int UARCOrganizationId { get; set; }
        public Guid? OrganizationId { get; set; }
    }
    public class LoginUserModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }

    public class UserListModel : ListModel<UserModel>
    {
        public UserListModel(IList<User> userList)
        {
            List = new List<UserModel>();
            foreach (var user in userList)
            {
                UserModel userModel = Mapper.Map<UserModel>(user);
                List.Add(userModel);
            }
        }

        public UserCondition condition { get; set; }

    }
}