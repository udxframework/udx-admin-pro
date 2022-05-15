namespace Udx.Admin.Components;
public partial class UdxFooter
{
    [Parameter] public string Style { get; set; }
    [Parameter] public string Class { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public RenderFragment Copyright { get; set; }
    [Parameter] public RenderFragment FooterContent { get; set; }
    protected override void OnInitialized()
    {
        Style += Style ?? "background-color: #f4f5f9;";
        Class += Class ?? "footer";
        Copyright = Copyright ?? GetRenderFragment(AppSetting.UdxSettings.Copyright);
        FooterContent = FooterContent ?? GetRenderFragment(AppSetting.UdxSettings.FooterContent);
        base.OnInitialized();
    }
    public static RenderFragment GetRenderFragment(string html)
    {
        RenderFragment render = (builder) => builder.AddMarkupContent(0, html);
        return render;
    }
}