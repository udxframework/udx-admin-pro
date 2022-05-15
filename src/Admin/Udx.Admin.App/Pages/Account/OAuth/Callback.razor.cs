using System.Net;

namespace Udx.Admin.App.Pages.Account;

public partial class Callback : ComponentBase
{
    [Parameter][SupplyParameterFromQuery] public string type { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    public string message { get; set; }
    protected override async Task OnInitializedAsync()
    {
       
        await OAuthLogin(type);
    }

    public async Task OAuthLogin(string type )
    {
        var redirectUrl = "";
        switch (type.ToLower())
        {
            case "baidu":
                {
                    //redirectUrl = baiduOAuth.GetAuthorizeUrl();
                    break;
                }
            case "wechat":
                {
                   // redirectUrl = wechatOAuth.GetAuthorizeUrl();
                    break;
                }
            default:
                message = $"没有实现【{type}】登录方式！";
                return;
        }
        _navigationManager.NavigateTo(redirectUrl, true);
    }


}