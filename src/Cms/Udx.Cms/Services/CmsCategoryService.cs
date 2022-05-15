using Udx.Cms.IServices;
using Udx.Cms.Repositorys;

namespace Udx.Cms.Services
{
    /// <summary>
    /// CmsCategory服务
    /// </summary>	
    public class CmsCategoryService : CmsService, ICmsCategoryService
    {
        private readonly IUser _user;
        private readonly ICmsCategoryRepository _CmsCategoryRepository;
        /// <summary>
        /// CmsCategory服务
        /// </summary>
        /// <param name="user"></param>
        /// <param name="CmsCategoryRepository"></param>
        public CmsCategoryService(
            IUser user,
            ICmsCategoryRepository CmsCategoryRepository
        )
        {
            _user = user;
            _CmsCategoryRepository = CmsCategoryRepository;
        }

        /// <summary>
        /// 根据Id获取CmsCategory信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<CmsCategoryModel>> GetAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            var entity = await _CmsCategoryRepository.GetAsync(id);
            var data = entity.Mapper<CmsCategoryModel>();
            
            if (data!=null)
                 return DataResponse<CmsCategoryModel>.Success("成功",data);
            else
                return DataResponse<CmsCategoryModel>.Fail("没有查询到数据！", data);

        }

        /// <summary>
        /// 分页获取信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<QueryResponse<IEnumerable<CmsCategoryModel>>>> PageQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _CmsCategoryRepository.PageLinqQueryAsync<CmsCategoryModel>(queryRequest);
            return DataResponse<QueryResponse<IEnumerable<CmsCategoryModel>>>.Success(result);
        }
        /// <summary>
        /// 获取分类信息的树结构信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<IEnumerable<CmsCategoryTree>>> TreeQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _CmsCategoryRepository.GetCmsCategoryTreeAsync(queryRequest);

            return DataResponse<IEnumerable<CmsCategoryTree>>.Success(result);
        }
        /// <summary>
        /// 保存操作实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<CmsCategoryEdit>> request)
        {
            var operaterModel = request.ObjectData;
            if (operaterModel.Operater == SaveOperater.Add)
                {
                    operaterModel.Model.Id = operaterModel.Model.Id??System.Guid.NewGuid().ToString();
                    
                }
                var saveModel = operaterModel.Mapper<SaveModel<CmsCategoryEntity>>();
                var result = await _CmsCategoryRepository.SaveModelAsync(saveModel) > 0;
                return new DataResponse<bool>()
                {
                    Successful = result,
                    ObjectData = result,
                    Code = operaterModel.Model.Id
                };
        }

        
    }
}
