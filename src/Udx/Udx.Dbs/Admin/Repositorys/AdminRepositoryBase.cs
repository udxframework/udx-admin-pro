using Microsoft.Extensions.Logging;
using Udx.Auth;
using Udx.Entites.BaseEntites;

namespace Udx.Admin.Repositorys
{
    /// <summary>
    /// Admin仓储层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public  class AdminRepositoryBase<TEntity> : Udx.Repositorys.UdxRepository<TEntity> where TEntity :class, IEntityFull,new()
    {
        protected AdminContext _adminContext;
        public AdminRepositoryBase(AdminContext dbContext, IUser identityContext) : base(dbContext, identityContext)
        {
            _adminContext = dbContext;
        }
           
       
    }
}
