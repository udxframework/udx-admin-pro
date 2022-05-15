namespace Udx.Admin.App.Shared;
public partial class AdminHeader 
{
    [Parameter] public string MenuKey { get; set; }
   
    public bool _isMobile;

    [Parameter] public bool IsHome { get; set; }

    [Parameter] public EventCallback<bool> OnMobileModeChanged { get; set; }
       
    AuthUser _user;
    protected override async Task OnInitializedAsync()
    {
        _user = await identityUserState.GetAuthUser();
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