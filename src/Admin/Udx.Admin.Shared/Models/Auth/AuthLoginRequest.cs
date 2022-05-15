using System.ComponentModel.DataAnnotations;

namespace Udx.Admin.Models
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class AuthLoginRequest
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空！")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空！")]
        public string Password { get; set; }

        /// <summary>
        /// 密码键
        /// </summary>
        public string PasswordKey { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        //[Required(ErrorMessage = "验证码不能为空！")]
        public string VerifyCode { get; set; }

        /// <summary>
        /// 验证码键
        /// </summary>
        public string VerifyCodeKey { get; set; }

        /// <summary>
        /// 单点登录Key
        /// </summary>
        public string SSOKey { get; set; }
        /// <summary>
        /// 单点登录状态
        /// </summary>
        public bool SSOResult { get; set; } = false;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string MessageInfo { get; set; }

        public string Token { get; set; }
    }
}
