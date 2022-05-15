using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 角色服务
    /// </summary>	
    public interface IRoleService
	{
          /// 获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<RoleModel>> GetAsync(DataRequest<string> request);

        Task<DataResponse<QueryResponse<IEnumerable<RoleModel>>>> PageQueryAsync(DataRequest<QueryModel> request);
        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<RoleEdit>> request);
    }
}
