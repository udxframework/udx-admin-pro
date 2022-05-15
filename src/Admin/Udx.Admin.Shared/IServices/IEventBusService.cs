using Udx.EventBus;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// ��Ϣ���߷���
    /// </summary>	
    public interface IEventBusService
    {

        Task<DataResponse<bool>> PublishAsync(DataRequest<EventData> request);
    }
}
