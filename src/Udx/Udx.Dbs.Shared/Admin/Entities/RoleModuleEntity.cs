using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Udx.Entites.BaseEntites;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 角色模块表
    /// </summary>
    [Comment("角色模块表")]
    [Table("udx_admin_role_module")]
    [Index(nameof(TenantId), nameof(RoleId), nameof(ModuleId), Name = "Index_Role_ModuleId", IsUnique = true)]
    public class RoleModuleEntity: EntityFull
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Comment("角色Id")]
        [StringLength(36)]
        public string RoleId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        [Comment("模块Id")]
        [StringLength(36)]
        public string ModuleId { get; set; }

        /// <summary>
        /// 模块Actions
        /// </summary>
        [Comment("模块Actions")]
        public string ModuleActions { get; set; }
        
        

    }

}
