namespace Udx.EventBus.Repositorys;

/// <summary>
/// EventBus仓储层基类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class EventBusRepositoryBase<TEntity> : Udx.Repositorys.UdxRepository<TEntity> where TEntity :class, IEntityFull,new()
{
    protected EventBusContext _eventBusContext;
    public EventBusRepositoryBase(EventBusContext dbContext, IUser identityContext) : base(dbContext, identityContext)
    {
        _eventBusContext = dbContext;
    }
           
       
}