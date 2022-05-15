namespace Udx.Dbs.Entities;
    /// <summary>
    /// 模块表
    /// </summary>
    [Comment("模块表")]
    [Table("udx_admin_module")]
    [Index(nameof(TenantId), nameof(Id), Name = "Index_Module_TenantId_ModuleKey", IsUnique =false )]
    public class ModuleEntity: EntityFull
    {
        /// <summary>
        /// 父Id
        /// </summary>
        [Description("父Id")]
        [Comment("父Id")]
        [StringLength(36)]
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 模块分类
        /// </summary>
        [Comment("模块分类")]
        [StringLength(100)]
        public virtual string ModuleType { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Comment("模块名称")]
        [StringLength(100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 模块key
        /// </summary>
        [Comment("模块Key")]
        [StringLength(100)]
        public virtual string Key { get; set; }


        /// <summary>
        /// 模块Actions
        /// </summary>
        [Comment("模块Actions")]
        public virtual string Actions { get; set; }
        /// <summary>
        /// 模块Icon
        /// </summary>
        [Comment("模块Icon")]
        [StringLength(200)]
        public virtual string Icon { get; set; }
        /// <summary>
        /// 模块RouterPath
        /// </summary>
        [Comment("模块路由Path")]
        [StringLength(200)]
        public virtual string Path { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        [StringLength(50)]
        public virtual string Status { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Comment("是否隐藏")]
        public virtual bool HideInMenu { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
       [Comment("排序")]
    [Column(TypeName = "decimal(18,2)")]
    public virtual decimal Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Comment("描述")]
        public virtual string Description { get; set; }

    }