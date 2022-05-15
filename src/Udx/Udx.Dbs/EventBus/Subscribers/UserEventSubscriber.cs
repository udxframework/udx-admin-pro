using Udx.Admin;
using Udx.Dbs.Cache;
using Udx.EventBus;

namespace Udx.Dbs.EventBus.Subscribers
{
    /// <summary>
    /// Admin User相关事件
    /// </summary>
    public class UserEventSubscriber : BaseSubscriber,IEventSubscriber
    {
        private readonly IServiceProvider _services;
        public UserEventSubscriber(IServiceProvider services)
        {
            _services = services;
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [EventSubscribe(EventIds.UserRegister)]
        public async Task RegisterEvent(EventHandlerExecutingContext context)
        {
            var source = context.Source;            
            await EventBusHelper.SaveLogAsync(source);
            //注册的时候自动给开通默认角色
            //var roleId = await ConfigCache.GetConfigDetailValueAsync("Admin-User", "DefaultRegisterRoleId");
            //if(!string.IsNullOrEmpty(roleId))
            //{
            //    using var ctx = new AdminContext();
            //    await ctx.RoleUserEntity.AddAsync(new RoleUserEntity()
            //    {
            //        RoleId = roleId.ToString(),
            //        UserId = source.Payload.User.Id,

            //    });
            //    await ctx.SaveChangesAsync();
            //}
            await EventBusHelper.UpdateEventDataAsync(source);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [EventSubscribe(EventIds.UserLogin)]
        public async Task LoginEvent(EventHandlerExecutingContext context)
        {
            var source = context.Source;
            await EventBusHelper.SaveLogAsync(source);
            await EventBusHelper.UpdateEventDataAsync(source);
        }
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [EventSubscribe(EventIds.UserPassword)]
        public async Task PasswordEvent(EventHandlerExecutingContext context)
        {
            var source = context.Source;
            await EventBusHelper.SaveLogAsync(source);
            await EventBusHelper.UpdateEventDataAsync(source);
        }
    }
}
