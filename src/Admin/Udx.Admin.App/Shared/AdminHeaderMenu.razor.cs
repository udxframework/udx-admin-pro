namespace Udx.Admin.App.Shared;
public partial class AdminHeaderMenu
{
    [Parameter] public bool IsMobile { get; set; }

    public record MenuItem(string Title, string Url, int Order, List<MenuItem>? Items=null);

    List<MenuItem> _menuItems { get; set; } = new List<MenuItem>();
    AuthUser _user;
    protected override async Task OnInitializedAsync()
    {
        _user = await identityUserState.GetAuthUser();
        _menuItems = new List<MenuItem>()
        {
            new MenuItem("首页","/",1),
            new MenuItem("文档","/cms/list",2),

        };
    }
    
}