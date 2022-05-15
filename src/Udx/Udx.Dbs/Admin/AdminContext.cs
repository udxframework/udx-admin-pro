using Udx.Configs;
using Udx.DBUtility;
#nullable disable
namespace Udx.Admin
{
    /// <summary>
    /// 类名和配置节点的key一致，通过类名nameof(AdminDb)来找到配置节点
    /// </summary>
    public class AdminDb
    {
        public static DbConfig DbConfig { get { return Udx.Dbs.DbsStartup.DbConfigs[nameof(AdminDb)]; } }
    }

    public sealed class AdminContext : DbContextBase
    {
        /// <summary>
        /// Admin 数据实体
        /// </summary>
        public AdminContext() : base(ContextHelper<AdminContext>.GetOptions(AdminDb.DbConfig))
        {
            DbConfig = AdminDb.DbConfig; 
        }
        

        #region DB Table         
        public DbSet<TenantEntity> TenantEntity { get; set; } 

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<UserOAuthEntity> UserOAuthEntity { get; set; }
        public DbSet<UserConfigEntity> UserConfigEntity { get; set; } 


        public DbSet<RoleEntity> RoleEntity { get; set; } 
        public DbSet<RoleUserEntity> RoleUserEntity { get; set; } 
        public DbSet<RoleModuleEntity> RoleModuleEntity { get; set; } 

        public DbSet<OrgEntity> OrgEntity { get; set; } 
        public DbSet<OrgUserEntity> OrgUserEntity { get; set; } 


        public DbSet<UserPasswordEntity> UserPasswordEntity { get; set; } 

        public DbSet<ModuleEntity> ModuleEntity { get; set; } 

        public DbSet<ConfigEntity> ConfigEntity { get; set; } 

        public DbSet<ConfigDetailEntity> ConfigDetailEntity { get; set; } 



        /// <summary>
        /// 流水号配置表
        /// </summary>
        public DbSet<NumberEntity> NumberEntity { get; set; }

        /// <summary>
        /// 日志表
        /// </summary>
        public DbSet<LogInfoEntity> LogInfoEntity { get; set; }
        /// <summary>
        /// 事件消息表
        /// </summary>
        public DbSet<EventDataEntity> EventDataEntity { get; set; }

        #endregion

        #region ViewModel

        #endregion
    }
}
