using Microsoft.EntityFrameworkCore.Infrastructure;
using Udx.Configs;
using Udx.DBUtility;
using Udx.Utils;

namespace Udx.Cms
{
    /// <summary>
    /// 类名和配置节点的key一致，通过类名nameof(CmsDb)来找到配置节点
    /// </summary>
    public class CmsDb
    {
        public static  DbConfig GetDbConfig()
        {            
            var tenantId = TenantHelper.GetTenantId();
            var tenant = TenantHelper.GetTenant(tenantId);
            var config = Udx.Dbs.DbsStartup.DbConfigs[nameof(CmsDb)].Mapper<DbConfig>(); ;
            if (tenant?.DbType == "MySql")
            {
                config.ConnectionString = tenant.ConnectionString;
                config.DbType = DBType.MySql;
            }
            else if (tenant?.DbType == "SqlServer")
            {
                config.ConnectionString = tenant.ConnectionString;
                config.DbType = DBType.SqlServer;
            }
            config.TenantId = tenantId;
            return config;
        }
    }
    
    public class CmsContext : DbContextBase
    {
        /// <summary>
        /// Cms 数据库实体
        /// </summary>
        public CmsContext() : base(ContextHelper<CmsContext>.GetOptions(CmsDb.GetDbConfig()))
        {
            DbConfig =  CmsDb.GetDbConfig();
            CacheKey = DbConfig.TenantId;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 租户间数据隔离查询
            var tenantId = TenantHelper.GetTenantId();
            if (!string.IsNullOrEmpty(tenantId))
            {
                //modelBuilder.Entity<CmsCategoryEntity>().HasQueryFilter(x => x.TenantId == tenantId);
                HasQueryFilter<ITenant>(modelBuilder, "TenantId",tenantId);
            }
        }

      

        protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            this.CacheKey = DbConfig.TenantId;            
            optionsBuilder.ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory<CmsContext>>();
            base.OnConfiguring(optionsBuilder);
        }
        #region DB Table 
        public DbSet<CmsCategoryEntity> CmsCategoryEntity { get; set; }
        public DbSet<CmsContentEntity> ContentEntity { get; set; }
        public DbSet<CmsContentCommentEntity> CmsMessageEntity { get; set; }
        public DbSet<CmsFileEntity> CmsFileEntity { get; set; }
        #endregion

        #region ViewModel

        #endregion
    }
}
