using Udx.EventBus;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 消息总线服务
    /// </summary>	
    public interface IEventBusService
    {

        Task<DataResponse<bool>> PublishAsync(DataRequest<EventData> request);
    }
}
