using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Udx.OAuth;

namespace Udx.Admin.App;
public static class AdminAppStartup
{
    public static IServiceCollection AddUdxAdminApp(this IServiceCollection services, IConfiguration Configuration)
    {
        var baseAddress = Configuration["UdxSettings:BaseAddress"];
        services.AddProxyClient(options =>
        {
            options.ClientName = "UdxAdmin";
            options.BaseAddress = baseAddress + "/Udx/Admin";
            options.AssemblyString = new[] { typeof(IAdminService).Assembly.FullName };
        });
        services.AddSingleton(new Udx.OAuth.QQ.QQOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:baidu")));
        OAuthConfig.LoadFrom(Configuration, "oauth:baidu");
        return services;
    }
      
}