using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services;

/// <summary>
/// TenantService
/// </summary>
public class TenantService : AdminService, ITenantService
{
    private readonly IUser _user;
    private readonly ITenantRepository _tenantRepository;
    /// <summary>
    /// TenantService
    /// </summary>
    /// <param name="user"></param>
    /// <param name="tenantRepository"></param>
    public TenantService(
        IUser user,
        ITenantRepository tenantRepository
    )
    {
        _user = user;
        _tenantRepository = tenantRepository;
    }
    /// <summary>
    /// 获取TenantGetOutput
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<TenantGetOutput>> GetAsync(DataRequest<string> request)
    {
        var id = request.ObjectData;
        var result = await _tenantRepository.GetAsync<TenantGetOutput>(id);
        return DataResponse<TenantGetOutput>.Success(result);
    }
    /// <summary>
    /// 获取列表分页
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
public async Task<DataResponse<QueryResponse<IEnumerable<TenantListOutput>>>> PageQueryAsync(DataRequest<QueryModel > request)
    {
        var queryRequest = request.ObjectData;
        var result = await _tenantRepository.PageLinqQueryAsync<TenantListOutput>(queryRequest);            
        return DataResponse<QueryResponse<IEnumerable<TenantListOutput>>>.Success(result);
}

    /// <summary>
    /// 保存操作实体
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<TenantEditModel> > request)
    {
        var operaterModel = request.ObjectData;
        if (operaterModel.Operater == SaveOperater.Add)
        {
            operaterModel.Model.Id = operaterModel.Model.Id ?? System.Guid.NewGuid().ToString();
            operaterModel.Model.Code = await SerialNumberHelper.GenerateNumberAsync("Tenant", new List<object>{ "" });

        }
        var saveModel = operaterModel.Mapper<SaveModel<TenantEntity>>();
        var result = await _tenantRepository.SaveModelAsync(saveModel) > 0;
        return new DataResponse<bool>()
        {
            Successful = result,
            ObjectData = result,
            Code = operaterModel.Model.Id
        };
    }
   
}
