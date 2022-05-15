using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using Udx.Utils;
namespace Udx.Server;
public class Program
{
    public static async Task  Main(string[] args)
    {
        var options = new WebApplicationOptions
        {
            Args = args,
            ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,            
        };
        var builder = WebApplication.CreateBuilder(options);
        builder.Host.UseWindowsService().UseSystemd();
        
        builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            var _env = hostContext.HostingEnvironment;
            loggerConfiguration.ReadFrom.Configuration(new ConfigHelper().Load("logconfig", _env.EnvironmentName));
        });
       // builder.WebHost.UseStartup<Startup>();
        var startup = new Startup(builder.Configuration, builder.Environment);
        startup.ConfigureServices(builder.Services);
        //string[] urls = startup._appConfig.Urls;
        //if (urls != null && urls.Any())
        //{
        //    builder.WebHost.UseUrls(urls);
        //    urls?.ForEach(url => Console.WriteLine("BindUrl:" + url));
        //}
        //else {
        //    Console.WriteLine("No BindUrls in appconfigs.json");
        //}
        var app = builder.Build();
        startup.Configure(app, app.Environment);        
        app.MapRazorPages();
        await app.RunAsync();
    }
}