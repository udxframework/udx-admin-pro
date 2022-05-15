namespace Udx.Cms.IServices;

public interface ICmsContentService
{
    Task<DataResponse<CmsContentResponse>> GetAsync(DataRequest<string> request);


    Task<DataResponse<QueryResponse<IEnumerable<CmsContentListResponse>>>> PageQueryAsync(DataRequest<QueryModel> request);
    Task<DataResponse<bool>> SaveAsync(DataRequest<SaveModel<CmsContentEditModel>> request);
}