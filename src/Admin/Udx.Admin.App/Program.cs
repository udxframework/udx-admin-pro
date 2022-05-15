using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace Udx.Admin.App;
public  class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        await builder.Build().RunAsync();
    }

}
