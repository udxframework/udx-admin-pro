using System.ComponentModel;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 组织机构表
    /// </summary>
    [Comment("组织机构")]
    [Table("udx_admin_org")]
    [Index(nameof(TenantId), nameof(OrgCode), Name = "Index_Org_TenantId",IsUnique =true)]
    public class OrgEntity: EntityFull
    {
        /// <summary>
        /// 父Id
        /// </summary>
        [Description("父Id")]
        [Comment("父Id")]
        [StringLength(36)]
        public  string ParentId { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        [Comment("组织机构名称")]
        [StringLength(100)]
        public string OrgName { get; set; }

        /// <summary>
        /// 组织机构代码
        /// </summary>
        [Comment("组织机构代码")]
        [StringLength(50)]
        public string OrgCode { get; set; }


      
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
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Sort { get; set; }
    }
}
