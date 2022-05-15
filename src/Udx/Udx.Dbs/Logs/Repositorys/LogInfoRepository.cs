using Udx.EventBus;
using Udx.Logs.Models;

namespace Udx.Logs.Repositorys
{
    /// <summary>
    /// LogInfoRepository
    /// </summary>
    public class LogInfoRepository : LogsRepositoryBase<LogInfoEntity>, ILogInfoRepository
    {

        /// <summary>
        /// LogInfoRepository
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public LogInfoRepository(LogsContext dbContext, IUser user) : base(dbContext, user) {

        }

        /// <summary>
        /// 保存事件日志
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task<int> SaveEventDataAsync(IEventSource eventData)
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
           var result = await ctx.SaveChangesAsync();
           return result;
        }

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task<int> SaveLogInfoAsync(LogInfo logInfo)
        {
            using var ctx = new LogsContext();
            ctx.LogInfoEntity.Add(new LogInfoEntity()
            {
                LogType = logInfo.LogType,
                Logger = logInfo.LogData.ToJson(),
                Description = logInfo.Description,

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
