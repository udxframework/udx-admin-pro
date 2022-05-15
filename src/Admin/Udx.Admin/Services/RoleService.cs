using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services
{
    /// <summary>
    /// Role服务
    /// </summary>	
    public class RoleService : AdminService, IRoleService
    {
        private readonly IUser _user;
        private readonly IRoleRepository _RoleRepository;
        /// <summary>
        /// Role服务
        /// </summary>
        /// <param name="user"></param>
        /// <param name="RoleRepository"></param>
        public RoleService(
            IUser user,
            IRoleRepository RoleRepository
        )
        {
            _user = user;
            _RoleRepository = RoleRepository;
        }

        /// <summary>
        /// 根据Id获取Role信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<RoleModel>> GetAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            var entity = await _RoleRepository.GetAsync(id);
            var data = entity.Mapper<RoleModel>();
            
            if (data!=null)
                 return DataResponse<RoleModel>.Success("成功",data);
            else
                return DataResponse<RoleModel>.Fail("没有查询到数据！", data);

        }

        /// <summary>
        /// 分页获取信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<QueryResponse<IEnumerable<RoleModel>>>> PageQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _RoleRepository.PageLinqQueryAsync<RoleModel>(queryRequest);           
            return DataResponse<QueryResponse<IEnumerable<RoleModel>>>.Success(result);
        }
        /// <summary>
        /// 保存操作实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<RoleEdit>> request)
        {
            var operaterModel = request.ObjectData;
            if (operaterModel.Operater == SaveOperater.Add)
                {
                    operaterModel.Model.Id = operaterModel.Model.Id??System.Guid.NewGuid().ToString();
                    
                }
                var saveModel = operaterModel.Mapper<SaveModel<RoleEntity>>();
                var result = await _RoleRepository.SaveModelAsync(saveModel) > 0;
                return new DataResponse<bool>()
                {
                    Successful = result,
                    ObjectData = result,
                    Code = operaterModel.Model.Id
                };
        }

        
    }
}
