using System.ComponentModel.DataAnnotations;

namespace Udx.Admin.Models
{
    public class ChangePwdModel
    {
        [Required]
        public string OldPassword { get; set; }



        [Required]
        [StringLength(50, ErrorMessage = "两次密码不一致，{0} 最少 {2}个字符，最多 {1} 个字符.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }



        public string OldPwdMd5 { get; set; }//加密传输
        public string PwdMd5 { get; set; }//加密传输
   
    }
}
