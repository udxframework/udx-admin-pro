using Serilog;
using Udx.Admin;
using Udx.Configs;
using Udx.DBUtility;

namespace Udx.EventBus
{
    /// <summary>
    /// 类名和配置节点的key一致，通过类名nameof(AdminDb)来找到配置节点
    /// </summary>
    public class EventBusDb
    {
        public static DbConfig DbConfig { get { return Udx.Dbs.DbsStartup.DbConfigs[nameof(AdminDb)]; } }
    }

    public sealed class EventBusContext : DbContextBase
    {
     
        /// <summary>
        /// EventBusContext 数据实体
        /// </summary>
        public EventBusContext() : base(ContextHelper<EventBusContext>.GetOptions(EventBusDb.DbConfig))
        {
            DbConfig = EventBusDb.DbConfig;
        }
        /// <summary>
        /// 消息发布
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static async Task PublishAsync(EventData eventData)
        {
            try
            {
                var eventPublisher = Udx.DependencyInjection.ServiceProviderManager.GetEventPublisher();
                eventData.MessageType = eventData.Payload.GetType().ToString();
                eventData.PublishDate = System.DateTime.UtcNow;
                await eventPublisher.PublishAsync(eventData);
                using var ctx = new EventBusContext();
                ctx.EventDataEntity.Add(new EventDataEntity()
                {
                    Id = eventData.Id,
                    EventId = eventData.EventId,
                    EventData = eventData.ToJson(),
                    PublishDate = eventData.PublishDate,
                    ResultStatus = "未消费",
                    CreatedUserId = eventData.User.Id,
                    CreatedUserName = eventData.User.UserName,
                    CreatedTime = DateTime.Now,
                    TenantId = eventData.User.TenantId
                });
                var result = await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex = ex.InnerException ?? ex;
                Log.Error("EventBusHelper.PublishAsync异常: {@eventData},@ex", eventData, ex);
            }

        }
        #region DB Table        

        /// <summary>
        /// 事件消息表
        /// </summary>
        public DbSet<EventDataEntity> EventDataEntity { get; set; }

        #endregion

        #region ViewModel

        #endregion
    }
}
