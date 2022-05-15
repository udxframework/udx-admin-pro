namespace Udx.Admin.App.Shared;
public partial class AdminLayout
{
    private List<RuleModule> MenuData { get; set; } = null!;
    private List<RuleModule> ruleModules { get; set; }
    private AuthUser User { get; set; }
    bool collapsed;
    public bool _isMobile;
    [Parameter] public EventCallback<bool> OnMobileModeChanged { get; set; }
    protected override async Task OnInitializedAsync()
    {
        //顶层top页面不要被嵌套！
        await JSRuntime.InvokeVoidAsync("udx.web.gotoWindowTop");
        await base.OnInitializedAsync();
        var identityUserState = ServiceProvider.Get<IdentityUserState>();
        await identityUserState.AutoLoginAsync();
        User = await identityUserState.GetAuthUser();
        if (User == null || User.IsExpires())
            NavigationManager.NavigateTo("/account/login");
        else
        {
            try
            {
                string userId = User.Id;
                ruleModules = await GetUserModulesAsync(userId);
                MenuData = GetMenus(ruleModules);
                await identityUserState.SetUserRuleModule(userId, ruleModules);
            }
            catch (Exception ex)
            {
                Log.Debug("GetUserModulesAsync Exception{@ex}", ex);
                NavigationManager.NavigateTo("/account/login");
            }
        }
    }
    public async Task<List<RuleModule>> GetUserModulesAsync(string userId)
    {
        var list = new List<RuleModule>();
        var request = new DataRequest<string>() { ObjectData = userId, User = User };
        var response = await RuleService.GetUserModulesAsync(request);
        if (response.Successful)
        {
            list = response.ObjectData;
        }
        else
        {
            await MessageBox.Error(response.Message);
            Log.Debug(response.Message);
        }
        return list;
    }
    List<RuleModule> GetMenus(List<RuleModule> list)
    {
        List<RuleModule> result;
        List<RuleModule> GetMenu(List<RuleModule> tree)
        {
            var menu = new List<RuleModule>();
            foreach (var t in tree.OrderBy(tt => tt.Sort))
            {
                var mm = new RuleModule
                {
                    Name = t.Name,
                    Key = t.Key,
                    //Authority = t.Actions,
                    Icon = t.Icon,
                    Path = t.Path,
                    HideInMenu = t.HideInMenu
                };
                menu.Add(mm);
                var items = list.Where(m => m.ParentId == t.Id && m.HideInMenu == false);
                if (items != null)
                {
                    mm.Modules = GetMenu(items.ToList());
                    if (!mm.Modules.Any()) mm.Modules = null;
                }
            }
            return menu;
        }
        var root = list.Where(x => list.All(y => y.Id != x.ParentId)).ToList();
        result = GetMenu(root);
        return result;
    }

    void toggle()
    {
        collapsed = !collapsed;
    }
    void onCollapse(bool isCollapsed)
    {
        this.collapsed = isCollapsed;
        Console.WriteLine($"Collapsed: {isCollapsed}");
    }
    private void OnBreakpoint(BreakpointType breakpoint)
    {
        _isMobile = breakpoint.IsIn(BreakpointType.Sm, BreakpointType.Xs);
        if (OnMobileModeChanged.HasDelegate)
        {
            OnMobileModeChanged.InvokeAsync(_isMobile);
        }
    }


}