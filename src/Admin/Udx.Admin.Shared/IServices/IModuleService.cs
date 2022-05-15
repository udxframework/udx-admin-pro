using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// ģ�����
    /// </summary>	
    public interface IModuleService
	{
          /// ��ȡ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<ModuleModel>> GetAsync(DataRequest<string> request);

        Task<DataResponse<QueryResponse<IEnumerable<ModuleModel>>>> PageQueryAsync(DataRequest<QueryModel> request);
        Task<DataResponse<IEnumerable<ModuleTree>>> TreeQueryAsync(DataRequest<QueryModel> request);
        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<ModuleEdit>> request);
    }
}
