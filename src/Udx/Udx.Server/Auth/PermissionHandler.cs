using Udx.Admin.IServices;
namespace Udx.Auth;
/// <summary>
/// 权限处理
/// </summary>
public class PermissionHandler : IPermissionHandler
{
      
    public PermissionHandler()
    {
    }

    /// <summary>
    /// 权限验证
    /// </summary>
    /// <param name="api">接口路径</param>
    /// <param name="httpMethod">http请求方法</param>
    /// <returns></returns>
    public async Task<bool> ValidateAsync(string api, string httpMethod)
    {
        var  _userService = DependencyInjection.ServiceProviderManager.Instance.GetService<IUserService>();
        /*
        var permissions = await _userService.GetPermissionsAsync();

        var valid = permissions.Any(m => 
            m.Path.NotNull() && m.Path.EqualsIgnoreCase($"/{api}")
            && m.HttpMethods.NotNull() && m.HttpMethods.Split(',').Any(n => n.NotNull() && n.EqualsIgnoreCase(httpMethod))
        );

        return valid;
        */
        await Task.FromResult(0);
        return true;
           
    }
}
