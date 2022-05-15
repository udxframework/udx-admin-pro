using System.Reflection;
using Udx.Admin.App;
using Udx.App;
using Udx.Cms.App;

namespace Udx.Client;
public static class Startup
{
    public static Type DefaultLayout => typeof(Udx.Admin.App.Shared.AdminLayout);
    public static IEnumerable<Assembly> GetUdxAppAssembly() {

        return new[] {  typeof(Udx.App.Program).Assembly,
                            typeof(Udx.Admin.App.Program).Assembly,
                            typeof(Udx.Cms.App.Program).Assembly
         };
    }

    public static IServiceCollection AddUdxClient(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddUdxApp(Configuration);
        services.AddUdxAdminApp(Configuration);
        services.AddUdxCmsApp(Configuration);
        return services;
    }
   
}