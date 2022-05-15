using Udx.Admin.IServices;
using Udx.Attributes;
using Udx.Services;

namespace Udx.Admin.Services
{

    /// <summary>
    /// Admin服务
    /// </summary>
    [Route("Udx/Admin/I[controller]/[action]"), ApiExplorerSettings(GroupName = "UdxAdmin")]
    [Permission]
    [ValidateInput]
    public class AdminService : UdxService, IAdminService
    {

        /// <summary>
        /// Admin服务说明
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public override string Index()
        {
           return "Admin Service";
        }
    }
}