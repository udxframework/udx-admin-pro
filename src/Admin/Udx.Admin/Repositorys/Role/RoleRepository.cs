using Udx.Auth;
using Udx.Dbs.Entities;
using Udx.DBUtility;
using Udx.Models;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// 角色持久层
    /// </summary>

    public class RoleRepository : AdminRepositoryBase<RoleEntity>, IRoleRepository
    {

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="user"></param>
        public RoleRepository(AdminContext dbContext, IUser user) : base(dbContext, user) { }

       
    
    }
}
