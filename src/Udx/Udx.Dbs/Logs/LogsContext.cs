using Udx.Admin;
using Udx.Configs;
using Udx.DBUtility;

namespace Udx.Logs
{
    /// <summary>
    /// 类名和配置节点的key一致，通过类名nameof(AdminDb)来找到配置节点
    /// </summary>
    public class LogsDb
    {
        public static DbConfig DbConfig { get { return Udx.Dbs.DbsStartup.DbConfigs[nameof(AdminDb)]; } }
    }

    public sealed class LogsContext : DbContextBase
    {
        /// <summary>
        /// EventBusContext 数据实体
        /// </summary>
        public LogsContext() : base(ContextHelper<LogsContext>.GetOptions(LogsDb.DbConfig))
        {
            DbConfig = LogsDb.DbConfig; 
        }        

        #region DB Table        
      
        /// <summary>
        /// 日志信息表
        /// </summary>
        public DbSet<LogInfoEntity> LogInfoEntity { get; set; }

        #endregion

        #region ViewModel

        #endregion
    }
}
