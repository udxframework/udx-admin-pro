using Udx.Admin.IServices;
using Udx.EventBus;

namespace Udx.Admin.Services
{
    /// <summary>
    /// 事件总线消息
    /// </summary>

    public class EventBusService : AdminService, IEventBusService
    {
        private readonly IUser _user;
        private readonly IEventPublisher _eventPublisher;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="user"></param>
       /// <param name="eventPublisher"></param>
        public EventBusService(
            IUser user,
            IEventPublisher eventPublisher)
        {
            _user = user;           
            _eventPublisher = eventPublisher;
         }


        /// <summary>
        /// 事件发布
        /// </summary>
        /// <param name="request">事件数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> PublishAsync(DataRequest<EventData> request)
        {
            var eventData = request.ObjectData;
            eventData.User = eventData.User ?? _user.Mapper<AuthUser>();
            await _eventPublisher.PublishAsync(eventData); 
            return DataResponse<bool>.Success("事件发布成功!");
        }
        
      
    }
}
