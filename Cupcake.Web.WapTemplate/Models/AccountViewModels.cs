using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.WapTemplate.Models
{

    public class LoginModel 
    {

        [Display(Name = "用户名")]
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }

        public string messageValue { get; set; }
    }

    public class RegisterModel 
    {
        public string Name { get; set; }
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "手机号")]
        public string Tel { get; set; }
        [Display(Name = "验证码")]
        public string ValidCode { get; set; }
    }

    public class ForgetPassModel 
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "手机号")]
        public string Tel { get; set; }

    }
    public class MyCenterModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Tel { get; set; }

        public string TouXiangUrl { get; set; }


    }
}
