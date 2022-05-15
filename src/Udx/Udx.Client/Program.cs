using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Udx.Configs;

namespace Udx.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var Configuration = builder.Configuration;
        var app = builder.Build();
        var services = builder.Services;
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        #region udx app
        services.AddUdxClient(Configuration);
        AppSetting.UdxSettings.SetWasm();
        #endregion

        //app.Urls.Add("http://localhost:5300");
        await builder.Build().RunAsync();
    }
}