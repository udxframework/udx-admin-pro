using Udx.Configs;

namespace Udx.Admin.Components;
public partial class UdxLogo 
{
    [Parameter] public string Style { get; set; }
    [Parameter] public string Class { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public RenderFragment Logo { get; set; }
    protected override void OnInitialized()
    {
        Style += Style ?? "";
        Class += Class ?? "logo";
        Logo = Logo ?? AppSetting.UdxSettings.GetLogo.RenderFragment();
        base.OnInitialized();
    }   
}