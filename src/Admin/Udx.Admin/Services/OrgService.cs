using Udx.Admin.IServices;
using Udx.Admin.Repositorys;

namespace Udx.Admin.Services
{
    /// <summary>
    /// Org服务
    /// </summary>	
    public class OrgService : AdminService, IOrgService
    {
        private readonly IUser _user;
        private readonly IOrgRepository _OrgRepository;
        /// <summary>
        /// Org服务
        /// </summary>
        /// <param name="user"></param>
        /// <param name="OrgRepository"></param>
        public OrgService(
            IUser user,
            IOrgRepository OrgRepository
        )
        {
            _user = user;
            _OrgRepository = OrgRepository;
        }

        /// <summary>
        /// 根据Id获取Org信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<OrgModel>> GetAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            var entity = await _OrgRepository.GetAsync(id);
            var data = entity.Mapper<OrgModel>();
            
            if (data!=null)
                 return DataResponse<OrgModel>.Success("成功",data);
            else
                return DataResponse<OrgModel>.Fail("没有查询到数据！", data);

        }

        /// <summary>
        /// 分页获取信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<QueryResponse<IEnumerable<OrgModel>>>> PageQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _OrgRepository.PageLinqQueryAsync<OrgModel>(queryRequest);
            return DataResponse<QueryResponse<IEnumerable<OrgModel>>>.Success(result);
        }
        /// <summary>
        /// 获取模块信息的树结构信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<IEnumerable<OrgTree>>> TreeQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _OrgRepository.GetOrgTreeAsync(queryRequest);

            return DataResponse<IEnumerable<OrgTree>>.Success(result);
        }
        /// <summary>
        /// 获取组织机构用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<List<OrgUserModel>>> GetOrgUserAsync(DataRequest<string> request)
        {
            var orgId = request.ObjectData;
            var result = await _OrgRepository.GetOrgUserAsync(orgId);
           
            return DataResponse<List<OrgUserModel>>.Success(result);
        }
        [HttpPost]
        public async Task<DataResponse<bool>> SaveOrgUserAsync(DataRequest<SaveModel<OrgUserModel>> request)
        {
            var operaterModel = request.ObjectData;
            if (operaterModel.Operater == SaveOperater.Add)
            {
                operaterModel.Model.Id = operaterModel.Model.Id ?? System.Guid.NewGuid().ToString();

            }
            var saveModel = operaterModel.Mapper<SaveModel<OrgUserEntity>>();
            var result = await _OrgRepository.SaveOrgUserAsync(saveModel) > 0;
            return DataResponse<bool>.Result(result, result);
           
        }
        /// <summary>
        /// 保存操作实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<OrgEdit>> request)
        {
            var operaterModel = request.ObjectData;
            if (operaterModel.Operater == SaveOperater.Add)
                {
                    operaterModel.Model.Id = operaterModel.Model.Id??System.Guid.NewGuid().ToString();
                    
                }
                var saveModel = operaterModel.Mapper<SaveModel<OrgEntity>>();
                var result = await _OrgRepository.SaveModelAsync(saveModel) > 0;
            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
                Code = operaterModel.Model.Id
            };
        }

        
    }
}
