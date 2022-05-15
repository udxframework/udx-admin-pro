using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Udx.Entites.BaseEntites;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 角色用户表
    /// </summary>
    [Comment("角色用户表")]
    [Table("udx_admin_role_user")]
    [Index(nameof(TenantId), nameof(RoleId), nameof(UserId), Name = "Index_Role_UserId", IsUnique = true)]
    public class RoleUserEntity: EntityFull
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Comment("角色Id")]
        [StringLength(36)]
        public string RoleId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Comment("用户Id")]
        [StringLength(36)]
        public string UserId { get; set; }

        
        

    }

}
