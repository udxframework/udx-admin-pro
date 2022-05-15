using Udx.Dbs.Entities;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IModuleRepository
    /// </summary>
    public interface IModuleRepository : Udx.Repositorys.IUdxRepository<ModuleEntity>
    {
        public Task<List<ModuleTree>> GetModuleTreeAsync(QueryModel queryRequest);
    }
}
