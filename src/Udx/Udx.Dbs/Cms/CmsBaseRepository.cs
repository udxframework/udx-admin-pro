using System;
using Udx.Auth;
using Udx.Entites.BaseEntites;

namespace Udx.Cms.Repositorys
{
    /// <summary>
    /// 内容仓储层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class CmsRepositoryBase<TEntity> :  Udx.Repositorys.UdxRepository<TEntity>  where TEntity : class,IEntityFull, new()
    {
        public CmsRepositoryBase(CmsContext dbContext, IUser identityContext) : base(dbContext, identityContext) 
        {           
        }        

       

    }

}
