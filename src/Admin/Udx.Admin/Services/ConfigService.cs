using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services
{
    /// <summary>
    /// Config服务
    /// </summary>	
    public class ConfigService : AdminService, IConfigService
    {
        private readonly IUser _user;
        private readonly IConfigRepository _ConfigRepository;
        /// <summary>
        /// Config服务
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfigRepository"></param>
        public ConfigService(
            IUser user,
            IConfigRepository ConfigRepository
        )
        {
            _user = user;
            _ConfigRepository = ConfigRepository;
        }



        /// <summary>
        /// 根据Id获取Config信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<ConfigModel>> GetAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            var entity = await _ConfigRepository.GetAsync(id);
            var data = entity.Mapper<ConfigModel>();
            
            if (data!=null)
                 return DataResponse<ConfigModel>.Success("成功",data);
            else
                return DataResponse<ConfigModel>.Fail("没有查询到数据！", data);

        }

        

        /// <summary>
        /// 分页获取信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<QueryResponse<IEnumerable<ConfigModel>>>> PageQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _ConfigRepository.PageLinqQueryAsync<ConfigModel>(queryRequest);
            return DataResponse<QueryResponse<IEnumerable<ConfigModel>>>.Success(result);
        }
        /// <summary>
        /// 保存操作主表实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<ConfigEdit>> request)
        {
            var operaterModel = request.ObjectData;
            if (operaterModel.Operater == SaveOperater.Add)
            {
                operaterModel.Model.Id = operaterModel.Model.Id??System.Guid.NewGuid().ToString();                    
            }
            var saveModel = operaterModel.Mapper<SaveModel<ConfigEntity>>();
            var result = await _ConfigRepository.SaveModelAsync(saveModel) > 0;
            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
                Code = operaterModel.Model.Id
            };
        }
        /// <summary>
        /// 保存操作主子表实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveConfigAsync(DataRequest<ConfigSaveModel> request)
        {
            var operaterModel = request.ObjectData;
            var result = await _ConfigRepository.SaveConfigModel(operaterModel) > 0;
            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
                Code = operaterModel.ConfigId
            };
        }
        /// <summary>
        /// 删除操作主子表实体
        /// </summary>
        /// <param name="request">配置的Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> DeleteConfigAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            //判断如果有下级是否也删除？ TODO

            var result = await _ConfigRepository.DeleteConfigAsync(id) > 0;
            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
            };
        }
        /// <summary>
        /// 获取配置信息的树结构信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<IEnumerable<ConfigTree>>> TreeQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _ConfigRepository.GetConfigTreeAsync(queryRequest);

            return DataResponse<IEnumerable<ConfigTree>>.Success(result);
        }
        /// <summary>
        /// 获取配置明细列表
        /// </summary>
        /// <param name="request">Config Id</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<List<ConfigDetailModel>>> GetConfigDetailListAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            var result = await _ConfigRepository.GetConfigDetailListAsync(id);
            return DataResponse<List<ConfigDetailModel>>.Success(result.Mapper<List<ConfigDetailModel>>());
        }

       

    }
}
