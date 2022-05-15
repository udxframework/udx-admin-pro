namespace Udx.Dbs.Entities;

/// <summary>
/// CMS内容表（udx_cms_content）实体
/// </summary>
[Comment("CMS内容表")]
[Table("udx_cms_content")]
    [Index(nameof(TenantId), nameof(CategoryId), Name = "Index_CmsContent_CategoryId", IsUnique = false)]
    public class CmsContentEntity : CmsBaseEntity
    {
    

    }