using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Udx.Entites.BaseEntites;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 用户配置表
    /// </summary>
    [Comment("用户配置表")]
    [Table("udx_admin_user_config")]
    [Index(nameof(TenantId), nameof(UserId), Name = "Index_UserConfig_TenantId",IsUnique =true)]
    public class UserConfigEntity: EntityFull
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Comment("用户Id")]
        [StringLength(36)]
        public string ParentId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Comment("用户Id")]
        [StringLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [Comment("是否管理员")]
        public bool IsAdmin { get; set; }


        /// <summary>
        /// 主题皮肤
        /// </summary>
        [Comment("主题皮肤")]
        public string Theme { get; set; }


    }
}
