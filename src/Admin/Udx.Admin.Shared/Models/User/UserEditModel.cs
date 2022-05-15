using System.ComponentModel.DataAnnotations;

namespace Udx.Admin.Models
{
    /// <summary>
    /// 修改
    /// </summary>
    public partial class UserEditModel: Udx.Dbs.Entities.UserEntity
    {

        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号")]
        public override string UserName { get; set; }
       
        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "请选择状态")]
        public override string Status { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public string MessageInfo { get; set; }
    }
}
