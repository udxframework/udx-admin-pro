namespace Udx.Dbs.Entities;
    /// <summary>
    /// 用户密码表
    /// </summary>
	[Table("udx_admin_user_password")]
    [Index(nameof(TenantId), nameof(UserId), Name = "Index_UserPassword_UserId", IsUnique = true)]
    public class UserPasswordEntity: EntityFull
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Comment("用户Id")]
        [StringLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Comment("登录密码")]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// 密码1
        /// </summary>
        [Comment("密码1")]
        [StringLength(50)]
        public string Password1 { get; set; }


        /// <summary>
        /// 密码2
        /// </summary>
        [Comment("密码2")]
        [StringLength(50)]
        public string Password2 { get; set; }

        /// <summary>
        /// 密码3
        /// </summary>
        [Comment("密码3")]
        [StringLength(50)]
        public string Password3 { get; set; }
    }