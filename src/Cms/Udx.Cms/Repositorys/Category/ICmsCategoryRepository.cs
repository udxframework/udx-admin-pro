using Udx.Dbs.Entities;

namespace Udx.Cms.Repositorys
{
    /// <summary>
    /// ICmsCategoryRepository
    /// </summary>
    public interface ICmsCategoryRepository : Udx.Repositorys.IUdxRepository<CmsCategoryEntity>
    {
        public Task<List<CmsCategoryTree>> GetCmsCategoryTreeAsync(QueryModel queryRequest);
    }
}
