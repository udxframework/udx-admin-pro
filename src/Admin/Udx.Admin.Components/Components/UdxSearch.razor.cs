namespace Udx.Admin.Components;
public partial class UdxSearch
{
    [Parameter]
    public SearchModel<List<FilterItem>> FetchModel { get; set; }

    [Parameter]
    public int QueryColSpan { get; set; } = 24;


    //[Parameter]
    //public EventCallback<EditContext> OnSearch { get; set; }

    //public async void Search() 
    //{
    //    await OnSearch.InvokeAsync();
    //}

    protected override Task OnInitializedAsync()
    {
        QueryColSpan = (FetchModel.FilterItems.Where(l=>l.IsShow).Sum(l => l.ColSpan) % 24) > 20 ? 24 : (24 - FetchModel.FilterItems.Where(l => l.IsShow).Sum(l => l.ColSpan) % 24);
        return base.OnInitializedAsync();
    }
}