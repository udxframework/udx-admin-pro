using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services
{
    /// <summary>
    /// ConfigCache配置中心缓存服务
    /// </summary>	
    public class ConfigCacheService : AdminService, IConfigCacheService
    {
        private readonly IUser _user;
        private readonly IConfigRepository _ConfigRepository;
        /// <summary>
        /// ConfigCache配置中心缓存服务
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfigRepository"></param>
        public ConfigCacheService(
            IUser user,
            IConfigRepository ConfigRepository
        )
        {
            _user = user;
            _ConfigRepository = ConfigRepository;
        }



        /// <summary>
        /// 根据key获取ConfigOption信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<ConfigOption>> GetConfigOptionAsync(DataRequest<string> request)
        {
            var key = request.ObjectData;
            var data = await Udx.Dbs.Cache.ConfigCache.GetConfigAsync(key);
            
            if (data!=null)
                 return DataResponse<ConfigOption>.Success("成功", data);
            else
                return DataResponse<ConfigOption>.Fail($"没有查询到{key} 数据配置！", null);

        }

       


    }
}
