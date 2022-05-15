using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services;

/// <summary>
/// Module服务
/// </summary>	
public class ModuleService : AdminService, IModuleService
{
    private readonly IUser _user;
    private readonly IModuleRepository _ModuleRepository;
    /// <summary>
    /// Module服务
    /// </summary>
    /// <param name="user"></param>
    /// <param name="ModuleRepository"></param>
    public ModuleService(
        IUser user,
        IModuleRepository ModuleRepository
    )
    {
        _user = user;
        _ModuleRepository = ModuleRepository;
    }

    /// <summary>
    /// 根据Id获取Module信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<ModuleModel>> GetAsync(DataRequest<string> request)
    {
        var id = request.ObjectData;
        var entity = await _ModuleRepository.GetAsync(id);
        var data = entity.Mapper<ModuleModel>();
        
        if (data!=null)
             return DataResponse<ModuleModel>.Success("成功",data);
        else
            return DataResponse<ModuleModel>.Fail("没有查询到数据！", data);

    }

    /// <summary>
    /// 分页获取信息
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<QueryResponse<IEnumerable<ModuleModel>>>> PageQueryAsync(DataRequest<QueryModel> request)
    {
        var queryRequest = request.ObjectData;
        var result = await _ModuleRepository.PageLinqQueryAsync<ModuleModel>(queryRequest);
        return DataResponse<QueryResponse<IEnumerable<ModuleModel>>>.Success(result);
    }
    /// <summary>
    /// 获取模块信息的树结构信息
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<IEnumerable<ModuleTree>>> TreeQueryAsync(DataRequest<QueryModel> request)
    {
        var queryRequest = request.ObjectData;
        var result = await _ModuleRepository.GetModuleTreeAsync(queryRequest);

        return DataResponse<IEnumerable<ModuleTree>>.Success(result);
    }
    /// <summary>
    /// 保存操作实体
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<ModuleEdit>> request)
    {
        var operaterModel = request.ObjectData;
        if (operaterModel.Operater == SaveOperater.Add)
            {
                operaterModel.Model.Id = operaterModel.Model.Id??System.Guid.NewGuid().ToString();
                
            }
            var saveModel = operaterModel.Mapper<SaveModel<ModuleEntity>>();
            var result = await _ModuleRepository.SaveModelAsync(saveModel) > 0;
            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
                Code = operaterModel.Model.Id
            };
    }

    
}
