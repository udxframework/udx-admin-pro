using Udx.Admin.IServices;

namespace Udx.Admin.Components.Models;
internal class ConfigOptionModel
{
    [Inject]
    static IConfigCacheService ConfigCacheService { get; set; }
    [Inject] static IdentityUserState identityUserStateBase { get; set; }
    public static async Task<ConfigOption> GetConfigOptionAsync(string key)
    {
        ConfigOption configOption = null;
        var user = await identityUserStateBase.GetAuthUser();
        var request = new DataRequest<string>() { ObjectData = key, User = user };
        var response = await ConfigCacheService.GetConfigOptionAsync(request);
        if (response.Successful)
        {
            configOption = response.ObjectData;
        }
        else
        {
            Log.Debug(response.Message);
        }
        return configOption;
    }

}