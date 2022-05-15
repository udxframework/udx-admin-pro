using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Udx.Entites.BaseEntites;

namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 租户
    /// </summary>
    [Comment("租户表")]
    [Table("udx_admin_tenant")]
    [Index(nameof(Code), Name = "Index_Tenant_Code", IsUnique = true)]

    public class TenantEntity : EntityFull
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Comment("编码")]
        [StringLength(50)]
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Comment("名称")]
        [StringLength(50)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        [Comment("数据库类型")]
        [StringLength(50)]
        public virtual string DbType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [Comment("连接字符串")]
        [StringLength(200)]
        public virtual string ConnectionString { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [Comment("是否启用")]
        public virtual bool Enabled { get; set; } = true;

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
