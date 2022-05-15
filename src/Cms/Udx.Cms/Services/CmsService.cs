using Microsoft.AspNetCore.Mvc;
using Udx.Cms.IServices;
using Udx.Services;

namespace Udx.Cms.Services
{
    /// <summary>
    /// CMS服务
    /// </summary>
    [Route("Udx/Cms/I[controller]/[action]"), ApiExplorerSettings(GroupName = "UdxCms")]
    public class CmsService : UdxService, ICmsService
    {

        /// <summary>
        /// CMS服务说明
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public override string Index()
        {
           return "Cms Service";
        }
    }
}