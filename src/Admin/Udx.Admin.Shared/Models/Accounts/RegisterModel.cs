using System.ComponentModel.DataAnnotations;

namespace Udx.Admin.Models
{
    public class RegisterModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "姓名")]
        [StringLength(20, ErrorMessage = "{0} 最少 {2}个字符，最多 {1} 个字符.", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "昵称")]
        [StringLength(20, ErrorMessage = "{0} 最少 {2}个字符，最多 {1} 个字符.", MinimumLength = 1)]
        public string NickName {  get; set; }
        [Required]
        [Display(Name = "登录名")]
        [StringLength(20, ErrorMessage = "{0} 最少 {2}个字符，最多 {1} 个字符.", MinimumLength = 2)]
        public string UserName { get; set; }

        

        [Required]
        [StringLength(50, ErrorMessage = "两次密码不一致，{0} 最少 {2}个字符，最多 {1} 个字符.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }

        public string MessageInfo { get; set; }
    }
}
