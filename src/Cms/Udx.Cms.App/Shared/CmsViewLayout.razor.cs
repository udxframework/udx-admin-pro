using Microsoft.JSInterop;

namespace Udx.Cms.App.Shared;
public partial class CmsViewLayout
{
        private bool Collapsed { get; set; } 
    private string ContentStyle { get; set; }

    protected void OnTrigger()
    {
        OnCollapse(!Collapsed);
    }
    protected void OnCollapse(bool collapsed)
    {
        this.Collapsed = collapsed;
        ContentStyle = Collapsed ? "padding:0px; margin-top:64px;margin-left:64px" : "padding:0 10px; margin-top:64px;margin-left:300px";
    }
        private AuthUser User { get; set; }
    protected override async Task OnInitializedAsync()
    {
        //����topҳ�治Ҫ��Ƕ�ף�
        await JSRuntime.InvokeVoidAsync("udx.web.gotoWindowTop");
        await base.OnInitializedAsync();
        ContentStyle = "padding:0px; margin-top:64px;margin-left:300px";
          
    }
 }