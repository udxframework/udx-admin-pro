namespace Udx.Dbs.Entities;

    /// <summary>
    /// 流水号规则配置表
    /// </summary>
    [Comment("流水号规则配置表")]
    [Table("udx_admin_number")]
    [Index(nameof(TenantId),nameof(Name), Name = "Index_udx_admin_number", IsUnique = true)]
    public class NumberEntity: EntityFull
    {

        /// <summary>
        /// 名称描述，根据这个取当前流水号的配置和更新
        /// </summary>
        /// <summary>
        /// 名称
        /// </summary>
        [Comment("名称")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 模板（常量+变量+日期格式+流水号）根据
        /// </summary>
        [Comment("模板")]
        [StringLength(50)]
        public string Template { get; set; }


        /// <summary>
        /// 当前最大流水号
        /// </summary>
        [Comment("当前最大流水号")]
        public int MaxNumber { get; set; } = 0;

        /// <summary>
        /// 最后生成的流水号
        /// </summary>
        [Comment("最后生成的流水号")]
        [StringLength(50)]
        public string LastNumber { get; set; } 
    }