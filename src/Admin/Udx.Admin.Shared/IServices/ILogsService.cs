using Udx.Logs.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// ��־����
    /// </summary>	
    public interface ILogsService
    {

        Task<DataResponse<bool>> LogWriteAsync(DataRequest<LogInfo> request);

       
    }
}
