using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Udx.Entites.BaseEntites;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    [Comment("角色表")]
    [Table("udx_admin_role")]
    [Index(nameof(TenantId), nameof(RoleName), Name = "Index_Role_TenantId", IsUnique = true)]
    public class RoleEntity: EntityFull
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Comment("名称")]
        [StringLength(50)]
        public virtual string RoleName { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        [StringLength(50)]
        public virtual string Status { get; set; }

        /// <summary>
        /// 模块分类
        /// </summary>
        [Comment("模块分类")]
        [StringLength(100)]
        public virtual string ModuleType { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Comment("说明")]
        public virtual string Description { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        [Column(TypeName = "decimal(18,2)")]
        public virtual decimal Sort { get; set; }

    }

}
