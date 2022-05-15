using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Udx.Admin.IServices;
using Udx.Admin.Repositorys;
using Udx.Admin.Services;

namespace Udx.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public static class AdminStartup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddUdxAdmin(this IServiceCollection services, IConfiguration Configuration)
        {

           

            #region 注册持久层

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrgRepository, OrgRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleUserRepository, RoleUserRepository>();
            services.AddScoped<IRoleModuleRepository, RoleModuleRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IConfigRepository, ConfigRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            #endregion

            #region 注册服务层
            services.AddScoped<IAuthService,AuthService>();
            #endregion

            return services;
        }

      
    }
}