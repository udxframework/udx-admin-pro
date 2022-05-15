
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 权限服务
    /// </summary>	
    public interface IAuthService
	{
        Task<DataResponse<string>> LoginTokenAsync(AuthLoginRequest input);
        Task<DataResponse<AuthLoginResponse>> LoginAsync(AuthLoginRequest input);
        Task<DataResponse<bool>> RegisterAsync(DataRequest<RegisterModel> request);
    }
}
