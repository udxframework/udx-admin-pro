namespace Udx.Logs.Repositorys;

/// <summary>
/// Logs仓储层基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public  class LogsRepositoryBase<TEntity> : Udx.Repositorys.UdxRepository<TEntity> where TEntity :class, IEntityFull,new()
{
    protected LogsContext _logsContext;
    public LogsRepositoryBase(LogsContext dbContext, IUser identityContext) : base(dbContext, identityContext)
    {
        _logsContext = dbContext;
    }
           
       
}