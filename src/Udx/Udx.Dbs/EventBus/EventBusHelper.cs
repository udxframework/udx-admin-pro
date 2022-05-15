using Serilog;
using Udx.Logs;

namespace Udx.EventBus;
public class EventBusHelper
{
    /// <summary>
    /// 消息发布
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task PublishAsync(EventData request) 
    {
        try
        {
            var eventPublisher = Udx.DependencyInjection.ServiceProviderManager.GetEventPublisher();
            request.MessageType = request.GetType().ToString();
            request.PublishDate = System.DateTime.UtcNow;
            await eventPublisher.PublishAsync(request);
        }
        catch (Exception ex)
        {
            ex = ex.InnerException ?? ex;
            Log.Error("EventBusHelper.PublishAsync异常: {@eventData},@ex", request,ex);
        }

    }



    /// <summary>
    /// 保存事件数据
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public static async Task<int> SaveEventDataAsync(IEventSource eventData)
    {
        int result = 0;
        try
        {
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
             result = await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error("EventBusHelper SaveEventDataAsync {@ex}", ex);
        }
        return result;
    }
    /// <summary>
    /// 修改EventData数据状态为已消费
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public static async Task<int> UpdateEventDataAsync(IEventSource eventData)
    {
        int result = 0;
        try
        {
            using var ctx = new EventBusContext();
            var model = await ctx.EventDataEntity.SingleOrDefaultAsync(ctx => ctx.Id == eventData.Id);
            if (model != null) {
                model.ResultStatus = "已消费";
                model.SubscriberDate = DateTime.Now;
                model.ModifiedUserId = eventData.User.Id;
                model.ModifiedUserName = eventData.User.UserName;
                result = await ctx.SaveChangesAsync();
            }            
        }
        catch (Exception ex)
        {
            Log.Error("EventBusHelper UpdateEventDataAsync {@ex}", ex);
        }
        return result;
    }

    /// <summary>
    /// 保存事件日志
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    public static async Task<int> SaveLogAsync(IEventSource eventData)
    {
        int result = 0;
        try
        {
            using var ctx = new LogsContext();
            ctx.LogInfoEntity.Add(new LogInfoEntity()
            {
                LogType = eventData.EventId,
                Logger = eventData.ToJson(),
                Description = eventData.Description,

                CreatedUserId = eventData.User.Id,
                CreatedUserName = eventData.User.UserName,
                CreatedTime = DateTime.Now,
                TenantId = eventData.User.TenantId
            });
            result = await ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error("EventBusHelper SaveLogEventDataAsync {@ex}", ex);
        }
        return result;
    }
}