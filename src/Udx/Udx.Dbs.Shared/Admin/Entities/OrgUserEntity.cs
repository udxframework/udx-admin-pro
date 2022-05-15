using System.ComponentModel;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 组织机构用户表
    /// </summary>
    [Comment("组织机构用户")]
    [Table("udx_admin_org_user")]
    [Index(nameof(TenantId), nameof(OrgId), nameof(UserId), Name = "Index_Org_UserId",IsUnique =true)]
    public class OrgUserEntity: EntityFull
    {
        /// <summary>
        /// 组织机构Id
        /// </summary>
        [Description("组织机构Id")]
        [Comment("组织机构Id")]
        [StringLength(36)]
        public  string OrgId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Description("用户Id")]
        [Comment("用户Id")]
        [StringLength(36)]
        public string UserId { get; set; }

    }
}
