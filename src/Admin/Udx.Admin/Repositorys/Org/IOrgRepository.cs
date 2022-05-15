using Udx.Dbs.Entities;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IOrgRepository
    /// </summary>
    public interface IOrgRepository : Udx.Repositorys.IUdxRepository<OrgEntity>
    {
        public Task<List<OrgTree>> GetOrgTreeAsync(QueryModel queryRequest);
        public Task<List<OrgUserModel>> GetOrgUserAsync(string orgId);
        public Task<int> SaveOrgUserAsync(SaveModel<OrgUserEntity> saveModel);
        public  Task<OrgUserEntity> GetOrgUserAsync(string orgId, string userId);
    }
}
