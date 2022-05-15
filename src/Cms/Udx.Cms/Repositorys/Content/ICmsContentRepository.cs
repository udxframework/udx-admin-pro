namespace Udx.Cms.Repositorys
{
    /// <summary>
    /// ICmsContentRepository
    /// </summary>
    public interface ICmsContentRepository : Udx.Repositorys.IUdxRepository<CmsContentEntity>
    {
        Task<QueryResponse<IEnumerable<T>>> CmsContentQueryAsync<T>(QueryModel queryRequest);
    }
}
