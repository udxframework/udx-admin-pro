namespace Udx.Dbs.Entities;

/// <summary>
/// Cms分类表（udx_cms_category）实体
/// </summary>
[Comment("Cms分类表")]
[Table("udx_cms_category")]
[Index(nameof(TenantId),nameof(ParentId), Name = "Index_CmsCategory_ParentId", IsUnique = false)]
public class CmsCategoryEntity : EntityFull
    {
    /// <summary>
    /// 上级Id
    [Comment("上级Id")]
    /// </summary>
    [StringLength(36)]
    public string ParentId { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [Comment("名称")]
    [StringLength(50)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 分类状态
    /// </summary>
    [Comment("状态")]
    [StringLength(20)]
    public virtual string Status { get; set; }

    /// <summary>
    /// 分类分组(根据不同的分组，可以过滤出来不同的分类)
    /// </summary>
    [Comment("分类分组")]
    [StringLength(20)]
    public virtual string Group { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Comment("排序")]
    [Column(TypeName = "decimal(18,2)")]
    public virtual decimal Sort { get; set; }

    }