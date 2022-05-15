using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 配置中心缓存接口
    /// </summary>	
    public interface IConfigCacheService
	{
        public Task<DataResponse<ConfigOption>> GetConfigOptionAsync(DataRequest<string> request);
    }
}
