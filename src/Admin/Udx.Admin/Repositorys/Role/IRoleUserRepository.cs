using Udx.Dbs.Entities;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// IRoleRepository
    /// </summary>
    public interface IRoleUserRepository : Udx.Repositorys.IUdxRepository<RoleEntity>
    {
        public Task<List<RoleUserModel>> GetUserRolesAsync(string userId);

        public Task<List<RoleUserModel>> GetRoleUsersAsync(string roleId);

        public Task<bool> SaveRoleUserAsync(RoleUserSaveModel saveModel);
    }
}
