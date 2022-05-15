using Udx.Dbs.Entities;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IRoleModuleRepository
    /// </summary>
    public interface IRoleModuleRepository : Udx.Repositorys.IUdxRepository<RoleEntity>
    {
      
        public Task<List<RoleModuleTree>> GetRoleModulesAsync(string roleId);
        public Task<List<RoleModuleTree>> GetRoleModuleTreeAsync(string roleId);

        public Task<bool> SaveRoleModuleAsync(RoleModuleSaveModel saveModel);
        public Task<List<RuleModule>> GetUserModulesAsync(string userId);
    }
}
