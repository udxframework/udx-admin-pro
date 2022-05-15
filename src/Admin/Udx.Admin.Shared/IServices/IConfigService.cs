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
    public interface IConfigService
	{
          /// ��ȡ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<ConfigModel>> GetAsync(DataRequest<string> request);

        Task<DataResponse<QueryResponse<IEnumerable<ConfigModel>>>> PageQueryAsync(DataRequest<QueryModel> request);

        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<ConfigEdit>> request);
        Task<DataResponse<bool>> SaveConfigAsync(DataRequest<ConfigSaveModel> request);

        Task<DataResponse<IEnumerable<ConfigTree>>> TreeQueryAsync(DataRequest<QueryModel> request);
        /// <summary>
        /// Config Detail���б��ѯ
        /// </summary>
        /// <param name="queryRequest"></param>
        /// <returns></returns>
        Task<DataResponse<List<ConfigDetailModel>>> GetConfigDetailListAsync(DataRequest<string> request);

        /// <summary>
        /// ��������Idɾ��������������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> DeleteConfigAsync(DataRequest<string> request);
    }
}
