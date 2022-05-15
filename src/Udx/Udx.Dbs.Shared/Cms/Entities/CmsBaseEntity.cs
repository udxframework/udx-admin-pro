namespace Udx.Dbs.Entities;

    /// <summary>
    /// CMS内容基础表
    /// </summary>
    public class CmsBaseEntity : EntityFull
    {
    /// <summary>
    /// 父页面ID
    /// </summary>
    [Comment("父页面ID")]
    [StringLength(36)]
    public virtual string ParentId { get; set; }

    [Comment("标题")]
    [StringLength(64)]
    public virtual string Title { get; set; }   
    
    [Comment("作者")]
    [StringLength(8)]
    public virtual string Author { get; set; } 

    [Comment("内容")]
    [StringLength(-1)]
    public virtual string Contents { get; set; }

    [Comment("封面图片")]
    [StringLength(255)]
    public virtual string CoverImgUrl { get; set; }

    [Comment("摘要")]
    [StringLength(255)]
    public virtual string Summary { get; set; }
    /// <summary>
    /// 分类Id
    /// </summary>
    [Comment("分类Id")]
    [StringLength(36)]
    public virtual string CategoryId { get; set; }

    [Comment("状态")]
    [StringLength(20)]
    public virtual string Status { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [Comment("标签")]
    [StringLength(255)]
    public virtual string Tags { get; set; }

    [Comment("是否留言")]
    public virtual bool IsComment { get; set; }

    [Comment("引用")]
    [StringLength(255)]
    public virtual string Reference { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [Comment("排序")]
    [Column(TypeName = "decimal(18,2)")]
    public virtual decimal Sort { get; set; }
    /// <summary>
    /// 阅读数
    /// </summary>
    [Comment("阅读数")]
    public virtual int BrowseCount { get; set; }
    
    /// <summary>
    /// 在看数
    /// </summary>
    [Comment("在看数")]
    public virtual int LookCount { get; set; }
    /// <summary>
    /// 回复数
    /// </summary>
    [Comment("回复数")]
    public virtual int CommentCount { get; set; }
    /// <summary>
    /// 点赞数
    /// </summary>
    [Comment("点赞数")]
    public virtual int LikeCount { get; set; }

}