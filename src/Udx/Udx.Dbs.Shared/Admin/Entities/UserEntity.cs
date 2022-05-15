namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    [Comment("用户表")]
    [Table("udx_admin_user")]
    [Index(nameof(TenantId), nameof(UserName), Name = "Index_User_TenantId",IsUnique =true)]
    public class UserEntity: EntityFull
    {

        /// <summary>
        /// 账号
        /// </summary>
        [Comment("账号")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string UserName { get; set; }

   

        /// <summary>
        /// 姓名
        /// </summary>
        [Comment("姓名")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Comment("昵称")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Comment("头像")]
        [StringLength(255)]
        [MaxLength(255)]
        public virtual string Avatar { get; set; }


        /// <summary>
        /// Email
        /// </summary>
        [Comment("Email")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [Comment("Phone")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string Phone { get; set; }

        /// <summary>
        /// Signature
        /// </summary>
        [Comment("Signature")]
        [StringLength(250)]
        [MaxLength(250)]
        public virtual string Signature { get; set; }


        /// <summary>
        /// 国家Country
        /// </summary>
        [Comment("Country")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string Country { get; set; }

        /// <summary>
        /// 区域代码RegionCode
        /// </summary>
        [Comment("区域代码RegionCode")]
        [StringLength(50)]
        [MaxLength(50)]
        public virtual string RegionCode { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Comment("Address")]
        [StringLength(250)]
        [MaxLength(255)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        [StringLength(50)]
        public virtual string Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Comment("描述")]
        public virtual string Description { get; set; }

        /// <summary>
        /// 默认组织机构Id
        /// </summary>
        [Description("组织机构Id")]
        [Comment("默认组织机构Id")]
        [StringLength(36)]
        public virtual string OrgId { get; set; }


        /// <summary>
        /// 是否数据权限超级用户
        /// </summary>
        [Comment("是否数据权限超级用户")]
        [StringLength(50)]
        public virtual string IsAdmin { get; set; }
    }
}
