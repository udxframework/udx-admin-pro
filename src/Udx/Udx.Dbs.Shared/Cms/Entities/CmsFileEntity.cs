namespace Udx.Dbs.Entities;
/// <summary>
/// CMS文件表（udx_cms_file）实体
/// </summary>
[Comment("CMS文件表")]
[Table("udx_cms_file")]
[Index(nameof(TenantId), nameof(ParentId), Name = "Index_CmsFile_ParentId", IsUnique = false)]
public class CmsFileEntity : EntityFull
    {
    /// <summary>
    /// 父ID
    /// </summary>
    [Comment("父ID")]
    [StringLength(36)]
    public string ParentId { get; set; }
    /// <summary>
    /// MongoDB ObjectId
    /// </summary>
    [Comment("MongoDB ObjectId")]
    [StringLength(100)]
    public string ObjectId { get; set; }
    [Comment("标题")]
    [StringLength(200)]
    public string Title { get; set; }
    [Comment("作者")]
    [StringLength(36)]
    public string Author { get; set; }

    [Comment("状态")]
    [StringLength(20)]
    public string Status { get; set; }
    [Comment("摘要")]
    [StringLength(120)]
    public string Summary { get; set; }
    /// <summary>
    /// 分类Id
    /// </summary>
    [Comment("分类Id")]
    [StringLength(36)]
    public string CategoryId { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [Comment("排序")]
    [Column(TypeName = "decimal(18,2)")]
    public virtual decimal Sort { get; set; }

    }
