namespace Udx.Dbs.Entities;

/// <summary>
/// CMS留言表（udx_cms_content_comment）实体
/// </summary>
[Comment("CMS留言表")]
[Table("udx_cms_content_comment")]
    [Index(nameof(TenantId), nameof(ContentId), Name = "Index_CmsContentComment_ContentId", IsUnique = false)]
    public class CmsContentCommentEntity : EntityFull
    {
        /// <summary>
        /// 父ID
        /// </summary>
        [Comment("父ID")]
        [StringLength(36)]
        public string ParentId { get; set; }
        /// <summary>
        /// 内容Id
        /// </summary>
        [Comment("内容Id")]
        [StringLength(36)]
        public string ContentId { get; set; }

        [Comment("作者")]
        [StringLength(36)]
        public string Author { get; set; }
        [Comment("内容")]
        [StringLength(-1)]
        public string Messages { get; set; }

        [Comment("状态")]
        [StringLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
    [Column(TypeName = "decimal(18,2)")]
    public  decimal Sort { get; set; }

    }
