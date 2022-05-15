using Udx.Configs;

namespace Udx.ClientServer;
public static class Startup
{
  
    public static IApplicationBuilder UdxClient(this WebApplication app)
    {
        if (AppSetting.UdxSettings.IsWasm)
        {

            app.MapFallbackToFile("/dms/sprint/sprint/{Id}/{Title}", "index.html");
            app.MapFallbackToFile("/dms/project/project-story-bug/{Id}/{Title}", "index.html");
            app.MapFallbackToFile("/dms/test/test-case/{_libraryId}/{_libraryName}", "index.html");
            app.MapFallbackToFile("/dms/test/test-plan/{_libraryId}/{_libraryName}", "index.html");
            app.MapFallbackToFile("index.html");
        }
        else {

            app.MapFallbackToPage("/dms/sprint/sprint/{Id}/{Title}", "/_Host");
            app.MapFallbackToPage("/dms/project/project-story-bug/{Id}/{Title}", "/_Host");
            app.MapFallbackToPage("/dms/test/test-case/{_libraryId}/{_libraryName}", "/_Host");
            app.MapFallbackToPage("/dms/test/test-plan/{_libraryId}/{_libraryName}", "/_Host");
            app.MapFallbackToPage("/_Host");
        }
        return app;
    }
   
}