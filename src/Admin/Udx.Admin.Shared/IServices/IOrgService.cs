using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 组织机构服务
    /// </summary>	
    public interface IOrgService
	{
          /// 获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<OrgModel>> GetAsync(DataRequest<string > request);

        Task<DataResponse<QueryResponse<IEnumerable<OrgModel>>>> PageQueryAsync(DataRequest<QueryModel > request);
        Task<DataResponse<IEnumerable<OrgTree>>> TreeQueryAsync(DataRequest<QueryModel > request);

        Task<DataResponse<List<OrgUserModel>>> GetOrgUserAsync(DataRequest<string > request);
        Task<DataResponse<bool>> SaveOrgUserAsync(DataRequest<SaveModel<OrgUserModel> > request);
        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<OrgEdit> > request);
    }
}
