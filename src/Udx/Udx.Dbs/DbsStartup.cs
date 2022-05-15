using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Udx.Admin;
using Udx.Cms;
using Udx.Configs;
using Udx.Dbs.EventBus.Subscribers;
using Udx.EventBus;
using Udx.EventBus.Repositorys;
using Udx.Logs;
using Udx.Logs.Repositorys;
using Udx.Mongo;
using Udx.Mongo.Bucket;
using Udx.Tenants;

namespace Udx.Dbs;
    public static class DbsStartup
    {
        public static Dictionary<string, DbConfig> DbConfigs { get; private set; }
        public static IServiceCollection AddUdxDbs(this IServiceCollection services, IConfiguration Configuration)
        {

            #region DbConfigs
            DbConfigs = services.BuildServiceProvider().GetService<Dictionary<string, DbConfig>>();
            #region 缓存
            var cacheConfig = services.BuildServiceProvider().GetService<CacheConfig>();
            if (cacheConfig.Type == CacheType.Redis)
            {
                //注册分布式的redis缓存服务
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheConfig.Redis.ConnectionString;
                    options.InstanceName = "UdxCache";
                });
            }
            else if (cacheConfig.Type == CacheType.SqlServer)
            {
                //注册分布式的SQLServer缓存服务
                services.AddDistributedSqlServerCache(options =>
                {
                    options.ConnectionString = cacheConfig.Redis.ConnectionString;
                    options.SchemaName = "dbo";
                    options.TableName = "UdxCache";
                });
            }
            else {
                //注册分布式的MemoryCache缓存服务,单服务器
                services.AddDistributedMemoryCache();
            }
            services.AddMemoryCache();
            #endregion

            #endregion
            services.AddScoped<ITenantProvider, TenantProvider>();
            #region 注册持久层
            services.AddDbContext<AdminContext>();
            services.AddDbContext<CmsContext>();

            #region Mongodb
            services.TryAddSingleton<MongoContext>();
            services.TryAddSingleton<UploadBucket>();
            services.TryAddSingleton<ImportBucket>();
            services.TryAddSingleton<ExportBucket>();
            services.TryAddSingleton<VditorBucket>();

            services.TryAddSingleton<MongoContext>();
            #endregion

            services.AddDbContext<LogsContext>();
            services.AddDbContext<EventBusContext>();
            #endregion
            #region 注册 Logs 持久层

            services.AddScoped<ILogInfoRepository, LogInfoRepository>();
            #endregion

            #region 注册 EventBus 服务
            services.AddScoped<IEventDataRepository, EventDataRepository>();

            services.AddEventBus(buidler =>
            {
                // 替换事件源存储器
                buidler.ReplacePublisher<UdxChannelEventPublisher>();
                // 注册 Login 事件订阅者
                buidler.AddSubscriber<UserEventSubscriber>();
            });
            #endregion
            // CreateDb();
            return services;
        }


        


        public static void CreateDb() {

            using var context = new AdminContext();           
            System.Console.WriteLine($"create {nameof(AdminContext)} db");
            context.Database.Migrate();
            context.Database.EnsureCreated();

           
        }

    }