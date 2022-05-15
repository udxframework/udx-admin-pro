using Microsoft.AspNetCore.Mvc;
using Udx.Auth;
using Udx.Cms.IServices;
using Udx.Cms.Repositorys;

namespace Udx.Cms.Services
{
    /// <summary>
    /// 内容服务
    /// </summary>
    public class CmsContentService : CmsService, ICmsContentService
    {
        private readonly IUser _user;
        private readonly ICmsContentRepository _CmsContentRepository;
        /// <summary>
        /// CmsContentService
        /// </summary>
        /// <param name="user"></param>
        /// <param name="CmsContentRepository"></param>
        public CmsContentService(
            IUser user,
            ICmsContentRepository CmsContentRepository
        )
        {
            _user = user;
            _CmsContentRepository = CmsContentRepository;
        }
        /// <summary>
        /// 获取CmsContentGetOutput
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<CmsContentResponse>> GetAsync(DataRequest<string> request)
        {
            var id = request.ObjectData;
            var result = await _CmsContentRepository.GetAsync<CmsContentResponse>(id);
            return DataResponse<CmsContentResponse>.Success(result);
        }
        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DataResponse<QueryResponse<IEnumerable<CmsContentListResponse>>>> PageQueryAsync(DataRequest<QueryModel> request)
        {
            var queryRequest = request.ObjectData;
            var result = await _CmsContentRepository.CmsContentQueryAsync<CmsContentListResponse>(queryRequest);
            return DataResponse<QueryResponse<IEnumerable<CmsContentListResponse>>>.Success(result);
        }

        /// <summary>
        /// 保存操作实体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<CmsContentEditModel>> request)
        {
            var operaterModel = request.ObjectData;
            if (operaterModel.Operater == SaveOperater.Add)
            {
                operaterModel.Model.Id = operaterModel.Model.Id ?? System.Guid.NewGuid().ToString();                
            }
            var saveModel = operaterModel.Mapper<SaveModel<CmsContentEntity>>();
            var result = await _CmsContentRepository.SaveModelAsync(saveModel) > 0;
            return new DataResponse<bool>()
            {
                Successful = result,
                ObjectData = result,
                Code = operaterModel.Model.Id
            };
        }

    }
}
