namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 配置子表
    /// </summary>
    [Comment("配置子表")]
    [Table("udx_admin_config_detail")]
    [Index(nameof(TenantId), nameof(ConfigDetailKey), Name = "Index_ConfigDetail_TenantId", IsUnique = true)]
    public class ConfigDetailEntity : EntityFull
    {
        /// <summary>
        /// 配置父表Id
        /// </summary>
        [Description("配置父表Id")]
        [Comment("配置父表Id")]
        [StringLength(36)]
        public string ConfigId { get; set; }

        /// <summary>
        /// 键标题
        /// </summary>
        [Comment("键标题")]
        [StringLength(50)]
        public string ConfigDetailTitle { get; set; }
        /// <summary>
        /// 键Key
        /// </summary>
        [Comment("键Key")]
        [StringLength(50)]
        public string ConfigDetailKey { get; set; }

        /// <summary>
        /// 值Value
        /// </summary>
        [Comment("值Value")]
        public string ConfigDetailValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        [StringLength(50)]
        public string Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Comment("描述")]
        public string Description { get; set; }

    }
}
