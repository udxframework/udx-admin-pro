using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    /// <summary>
    /// 用户服务
    /// </summary>	
    public interface IUserService
	{
        Task<DataResponse<AuthLoginResponse>> GetLoginUserAsync(DataRequest<string> request);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<UserGetOutput>> GetAsync(DataRequest<string> request);

        Task<DataResponse<QueryResponse<IEnumerable<UserListOutput>>>> PageQueryAsync(DataRequest<QueryModel> request);

        Task<DataResponse<ExportModel>> ExportAsync(DataRequest<QueryModel> request);

        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<UserEditModel>> request);
        Task<DataResponse<bool>> SaveListAsync(DataRequest<SaveModel<List<UserEditModel>>> request);
        Task<DataResponse<bool>> ResetPwdAsync(DataRequest<List<UserEditModel>> request);

        Task<DataResponse<bool>> ChangePwdAsync(DataRequest<ChangePwdModel> request);

    }
}
