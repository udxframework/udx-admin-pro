using Udx.DBUtility;
using Udx.Models;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// TenantRepository
    /// </summary>
    public class TenantRepository : AdminRepositoryBase<TenantEntity>, ITenantRepository
    {
      
       /// <summary>
       /// 
       /// </summary>
       /// <param name="dbContext"></param>
       /// <param name="user"></param>
        public TenantRepository(AdminContext dbContext, IUser user) : base(dbContext, user) { }

       

    }
}
