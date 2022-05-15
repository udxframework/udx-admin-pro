namespace Udx.Cms.IServices;
    /// <summary>
    /// 内容分类服务
    /// </summary>	
    public interface ICmsCategoryService
	{
          /// 获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataResponse<CmsCategoryModel>> GetAsync(DataRequest<string> request);

        Task<DataResponse<QueryResponse<IEnumerable<CmsCategoryModel>>>> PageQueryAsync(DataRequest<QueryModel> request);
        Task<DataResponse<IEnumerable<CmsCategoryTree>>> TreeQueryAsync(DataRequest<QueryModel> request);
        Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<CmsCategoryEdit>> request);
    }