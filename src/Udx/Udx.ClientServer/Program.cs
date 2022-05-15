using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Udx.Client;
using Udx.ClientServer;
using Udx.Utils;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};
var builder = WebApplication.CreateBuilder(options);
var Configuration = builder.Configuration;
var services = builder.Services;

var _configHelper = new ConfigHelper();

// Add services to the container.
services.AddRazorPages();
services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
services.AddUdxClient(Configuration);
services.AddSignalR(e => {
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Host.UseWindowsService().UseSystemd();
//string[] urls = Configuration["UdxSettings:Urls"]?.Split(";")!;
//if (urls!=null && urls.Any())
//{
//    builder.WebHost.UseUrls(urls);
//    urls?.ForEach(url => Console.WriteLine("BindUrl:" + url));
//}
//else
//{
//    Console.WriteLine("No BindUrls in UdxSettings:Urls");
//}
builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
{
    var _env = hostContext.HostingEnvironment;
    loggerConfiguration.ReadFrom.Configuration(new ConfigHelper().Load("logconfig", _env.EnvironmentName));
});

//���� level ���ƶ���
var logLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug); //Ĭ��Ϊ Info level

//����һ��ȫ�ֵ� Logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole()
    .MinimumLevel.ControlledBy(logLevelSwitch)
    .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
    .CreateLogger();

var app = builder.Build();
var env = app.Environment;

// Configure the HTTP request pipeline.
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHttpLogging();
}
else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   // app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapBlazorHub();
app.MapRazorPages();
app.MapControllers();
//�ͻ�������ģʽ
app.UdxClient();

await app.RunAsync();
