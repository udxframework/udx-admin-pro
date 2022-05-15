using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Udx.Cms.Repositorys;

namespace Udx.Cms
{
    public static class CmsStartup
    {
       
        public static IServiceCollection AddUdxCms(this IServiceCollection services, IConfiguration Configuration)
        {

            #region 注册Repositorys
            services.AddScoped<ICmsCategoryRepository, CmsCategoryRepository>();
            services.AddScoped<ICmsContentRepository, CmsContentRepository>();
            #endregion
            return services;
        }
    }
}