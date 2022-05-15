using Udx.EventBus;

namespace Udx.EventBus.Repositorys
{
    /// <summary>
    /// IEventDataRepository
    /// </summary>
    public interface IEventDataRepository : Udx.Repositorys.IUdxRepository<EventDataEntity>
    {
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> SaveEventDataAsync(IEventSource eventData);
        
    }
}
