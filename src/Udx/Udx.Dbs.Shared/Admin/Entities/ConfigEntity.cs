namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 配置表
    /// </summary>
    [Comment("配置表")]
    [Table("udx_admin_config")]
    [Index(nameof(TenantId), nameof(ConfigKey), Name = "Index_Config_TenantId", IsUnique = true)]
    public class ConfigEntity : EntityFull
    {
        /// <summary>
        /// 父Id
        /// </summary>
        [Description("父Id")]
        [Comment("父Id")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 键标题
        /// </summary>
        [Comment("键标题")]
        [StringLength(50)]
        public string ConfigTitle { get; set; }

        /// <summary>
        /// 键Key
        /// </summary>
        [Comment("键Key")]
        [StringLength(50)]
        public string ConfigKey { get; set; }

       

        /// <summary>
        /// 值Value
        /// </summary>
        [Comment("值Value")]
        public string ConfigValue { get; set; }
               

        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        [StringLength(50)]
        public string Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Sort { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Comment("描述")]
        public string Description { get; set; }

    }
}
