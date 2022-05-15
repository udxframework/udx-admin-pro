using Udx.Admin.IServices;
using Udx.Logs.Models;
using Udx.Logs.Repositorys;

namespace Udx.Admin.Services
{
    /// <summary>
    /// 事件总线消息
    /// </summary>

    public class LogsService : AdminService, ILogsService
    {
        private readonly IUser _user;
        private readonly ILogInfoRepository _logInfoRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="logInfoRepository"></param>
        public LogsService(
            IUser user,
            ILogInfoRepository logInfoRepository)
        {
            _user = user;
            _logInfoRepository = logInfoRepository;
         }


        /// <summary>
        /// 事件发布
        /// </summary>
        /// <param name="request">事件数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> LogWriteAsync(DataRequest<LogInfo> request)
        {
            var log = request.ObjectData;
            await _logInfoRepository.SaveLogInfoAsync(log);
            return DataResponse<bool>.Success("日志写入成功!");
        }
        
      
    }
}
