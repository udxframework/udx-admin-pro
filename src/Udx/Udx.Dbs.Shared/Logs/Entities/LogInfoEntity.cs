namespace Udx.Dbs.Entities
{
    /// <summary>
    /// 日志表
    /// </summary>
    [Comment("日志表")]
    [Table("udx_log_info")]
    [Index(nameof(TenantId), nameof(LogType), Name = "Index_Log_TenantId_LogType")]
    public class LogInfoEntity: EntityFull
    {

        /// <summary>
        /// 日志分类
        /// </summary>
        [Comment("日志分类")]
        [StringLength(50)]
        public virtual string LogType { get; set; }

        /// <summary>
        /// 日志数据
        /// </summary>
        [Comment("日志数据")]
        [StringLength(-1)]
        public virtual string Logger { get; set; }

        /// <summary>
        /// 日志说明
        /// </summary>
        [Comment("日志说明")]
        public virtual string Description { get; set; }

    }
}
