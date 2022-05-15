using System.Reflection;
using Udx.Admin.IServices;
using Udx.App.Pages.Exception;

namespace Udx.Admin.Components
{
    public partial class UdxComponentBase : ComponentBase, IHandleEvent
    {
        [Inject] public AntDesign.MessageService MessageBoxBase { get; set; }
        [Inject] public NavigationManager NavigationManagerBase { get; set; }
        [Inject]
        public IRuleService RuleServiceBase { get; set; }
        [Inject] public IdentityUserState identityUserStateBase { get; set; }
        public RuleComponentModel RuleHelper { get; set; } //= new RuleComponentModel(new List<RuleModule>());
        public static Hashtable ConfigOptionCache;
        /// <summary>
        /// 登录用户
        /// </summary>
        public AuthUser User { get; set; }
        public List<RuleModule> Modules { get; private set; }
        public RuleAttribute PageRuleAttribute { get; private set; }
        [CascadingParameter] public Error Error { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            await SetPageRule();
            try
            {
                await base.SetParametersAsync(ParameterView.Empty);
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
                Log.Error("SetParametersAsync {@ex}", ex);
            }
        }
        public async Task SetPageRule()
        {
            try
            {
                User = await identityUserStateBase.GetAuthUser();
                if (User == null)
                {
                    NavigationManagerBase.NavigateTo("/account/login");
                    await Task.Delay(1000);
                }
                else
                {
                    PageRuleAttribute = this.GetType().GetCustomAttribute<RuleAttribute>();
                    if (PageRuleAttribute != null)
                    {
                        if (Modules == null)
                            await GetUserModulesAsync();
                        await GetRule(Modules);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
                Log.Error("SetPageRule {@ex}", ex);
            }
        }
        protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (PageRuleAttribute == null)
                {
                    Log.Debug("页面没有设置权限！firstRender");
                }
                else
                {
                    //Log.Debug("OnAfterRenderAsync:{@User}", User);
                    if (RuleHelper.IsEnabled && !RuleHelper.GetModule())
                    {
                        Log.Debug("没有权限无法操作！{@url}", NavigationManagerBase.Uri);
                        NavigationManagerBase.NavigateTo($"/exception/NotAuthorized?returnUrl={Uri.EscapeDataString(NavigationManagerBase.Uri)}");
                    }
                    else
                    {
                        Log.Debug("有权限！firstRender");

                    }
                }

            }
            return base.OnAfterRenderAsync(firstRender);
        }


        Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem item, object arg)
        {
            var field = typeof(EventCallbackWorkItem).GetField("_delegate", BindingFlags.NonPublic | BindingFlags.Instance);
            var obj = field?.GetValue(item) as Delegate;
            var rule = obj?.Method.GetCustomAttribute<RuleAttribute>();
            if (rule != null)
            {
                var show = RuleHelper.GetAction(rule.ActionValue);
                if (!show)
                {
                    Log.Debug("没有权限无法操作！");
                    return Task.CompletedTask;
                }
            }
            return item.InvokeAsync(arg);
        }
        public async Task GetRule(List<RuleModule> ruleModule)
        {
            if (PageRuleAttribute != null) //设置了权限属性
            {
                RuleHelper = new RuleComponentModel(ruleModule)
                {
                    IsEnabled = true,
                    Key = PageRuleAttribute.Key,
                    Actions = PageRuleAttribute.Actions
                };
            }
            else
            {
                RuleHelper = new RuleComponentModel(ruleModule)
                {
                    IsEnabled = false
                };
            }
            await Task.FromResult(0);
        }
        public async Task GetUserModulesAsync()
        {
           
            Modules = await identityUserStateBase.GetUserRuleModule(User.Id);
            if (Modules == null)
            {
                var request = new DataRequest<string>() { ObjectData = User.Id, User = User };
                var response = await RuleServiceBase.GetUserModulesAsync(request);
                if (response.Successful)
                {
                    Modules = response.ObjectData;
                }
                else
                {
                    await MessageBoxBase.Error(response.Message);
                    Log.Debug(response.Message);
                }
            }
        }
        public virtual async Task<ConfigOption> GetConfigOptionAsync(string key)
        {
            ConfigOption configOption = null;
            try
            {
                ConfigOptionCache = ConfigOptionCache ?? new Hashtable();
                var request = new DataRequest<string>() { ObjectData = key, User = User };
                var response = await ConfigCacheService.GetConfigOptionAsync(request);
                if (response.Successful)
                {
                    configOption = response.ObjectData;
                    ConfigOptionCache[key] = configOption;
                }
                else
                {
                    // configOption = new ConfigOption() { Options = new List<ConfigOption>() };
                    Log.Debug(response.Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SetPageRule {@ex}", ex);
            }
            return configOption;
        }

    }
}