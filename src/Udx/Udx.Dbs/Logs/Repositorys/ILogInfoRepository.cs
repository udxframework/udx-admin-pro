using Udx.EventBus;
using Udx.Logs.Models;

namespace Udx.Logs.Repositorys
{
    /// <summary>
    /// ILogInfoRepository
    /// </summary>
    public interface ILogInfoRepository : Udx.Repositorys.IUdxRepository<LogInfoEntity>
    {
        /// <summary>
        /// 保存事件日志
        /// </summary>
        /// <param name="operaterModel"></param>
        /// <returns></returns>
        Task<int> SaveEventDataAsync(IEventSource eventData);
        Task<int> SaveLogInfoAsync(LogInfo logInfo);

    }
}
