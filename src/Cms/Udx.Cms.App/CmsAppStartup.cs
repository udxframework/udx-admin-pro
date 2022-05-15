using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Udx.Cms.App
{
    public static class CmsAppStartup
    {
        public static IServiceCollection AddUdxCmsApp(this IServiceCollection services, IConfiguration Configuration)
        {
            var baseAddress = Configuration["UdxSettings:BaseAddress"];
            services.AddProxyClient(options =>
            {
                options.ClientName = "UdxCms";
                options.BaseAddress = baseAddress + "/Udx/Cms";
                options.AssemblyString = new[] { typeof(ICmsService).Assembly.FullName };
            });
            return services;
        }

      
    }
}