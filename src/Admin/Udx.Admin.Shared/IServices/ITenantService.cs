using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udx.Admin.Models;
using Udx.Models;

namespace Udx.Admin.IServices
{
    public interface ITenantService
    {
        Task<DataResponse<TenantGetOutput>> GetAsync(DataRequest<string> request);


        Task<DataResponse<QueryResponse<IEnumerable<TenantListOutput>>>> PageQueryAsync(DataRequest<QueryModel> request);
        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<TenantEditModel>> request);

    }
}
