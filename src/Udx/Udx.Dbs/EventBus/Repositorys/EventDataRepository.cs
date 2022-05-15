using Udx.EventBus;

namespace Udx.EventBus.Repositorys
{
    /// <summary>
    /// EventDataRepository
    /// </summary>
    public class EventDataRepository : EventBusRepositoryBase<EventDataEntity>, IEventDataRepository
    {

        /// <summary>
        /// LogInfoRepository
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public EventDataRepository(EventBusContext dbContext, IUser user) : base(dbContext, user) {

        }

        /// <summary>
        /// 保存事件数据
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task<int> SaveEventDataAsync(IEventSource eventData)
        {
            using var ctx = new EventBusContext();
            ctx.EventDataEntity.Add(new EventDataEntity()
            {
                EventId = eventData.EventId,
                EventData = eventData.ToJson(),
                PublishDate = eventData.PublishDate,
                ResultStatus = "未消费",
                CreatedUserId = _identityContext.Id,
                CreatedUserName = _identityContext.UserName,
                CreatedTime = DateTime.Now,
                TenantId = _identityContext.TenantId
            });
           var result = await ctx.SaveChangesAsync();
           return result;
        }


       
    }
}
