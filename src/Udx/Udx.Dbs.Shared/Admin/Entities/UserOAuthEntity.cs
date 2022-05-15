namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 用户账户绑定
    /// </summary>
    [Comment("用户账户OAuth绑定表")]
    [Table("udx_admin_user_oauth")]
    [Index(nameof(TenantId), nameof(UserId), nameof(OAuthName), Name = "Index_User__OAuthName_TenantId", IsUnique =true)]
    public class UserOAuthEntity: EntityFull
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Comment("用户Id")]
        [StringLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// OAuthName
        /// </summary>
        [Comment("OAuthName")]
        [StringLength(50)]
        public virtual string OAuthName { get; set; }

        /// <summary>
        /// Unionid
        /// </summary>
        [Comment("Unionid")]
        [StringLength(255)]
        public virtual string Unionid { get; set; }

        /// <summary>
        /// Openid
        /// </summary>
        [Comment("Openid")]
        [StringLength(255)]
        public virtual string Openid { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Comment("昵称")]
        [StringLength(255)]
        public virtual string NickName { get; set; }

        
        /// <summary>
        /// 头像
        /// </summary>
        [Comment("头像")]
        [StringLength(250)]
        public virtual string Avatar { get; set; }

        /// <summary>
        /// 绑定状态
        /// </summary>
        [Comment("绑定状态(已绑定/未绑定)")]
        [StringLength(50)]
        public virtual string BindStatus { get; set; } = "已绑定";

        /// <summary>
        /// 消息提醒
        /// </summary>
        [Comment("消息提醒(已开启/未开启)")]
        [StringLength(50)]
        public virtual string MsgTip { get; set; } = "已开启";


        /// <summary>
        /// 绑定结果
        /// </summary>
        [Comment("绑定结果")]
        [StringLength(-1)]
        public virtual string OAuthResult { get; set; }        

       
    }
}
