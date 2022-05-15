using Udx.Dbs.Entities;

namespace Udx.Cms.App.Pages.Cms;
public partial class List : ComponentBase
{
    [Inject] public ICmsContentService _cmsContentService { get; set; }
    SearchModel<List<FilterItem>> _searchModel;

    QueryResponse<IEnumerable<CmsContentListResponse>> _cmsContentqueryResponse;
    [Parameter] public string? Id { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _cmsContentqueryResponse = new QueryResponse<IEnumerable<CmsContentListResponse>>()
        {

            Data = new List<CmsContentListResponse>()
        };
        await GetCmsContents(Id);

    }
   
    private async  Task GetCmsContents(string categoryId)
        {
        if (categoryId == "0") categoryId = "";
        _searchModel = new SearchModel<List<FilterItem>>()
        {
            FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(CmsContentEntity.CategoryId), Caption="分类",Operator= FilterOperator.Equal,Value=categoryId},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Status), Caption="状态",Operator= FilterOperator.Equal,Value="已发布"}
           },
            OrderBys = new List<OrderBy> {
                    new OrderBy() { Field = nameof(CmsContentEntity.Sort), Order = SortingOrders.Desc },
                    new OrderBy() { Field = nameof(CmsContentEntity.CreatedTime), Order = SortingOrders.Desc }
                },
            SearchAction = OnSearchAsync
        };
        await _searchModel.SearchAction();
    }
    public async Task OnSearchAsync()
    {
        try
        {
            var request = new DataRequest<QueryModel>()
            {
                ObjectData = new QueryModel()
                {
                    CurrentPage = _searchModel.CurrentPage,
                    PageSize = _searchModel.PageSize,
                    FilterItems = _searchModel.FilterItems,
                    OrderBys = _searchModel.OrderBys
                }
            };
            var dataResult = await _cmsContentService.PageQueryAsync(request);
            if (dataResult.Successful)
            {
                _cmsContentqueryResponse = dataResult.ObjectData;
                _searchModel.RowsCount = _cmsContentqueryResponse.RowsCount;
            }
            else
            {
               // await MessageBox.Error(dataResult.Message);

            }
        }
        catch (Exception ex)
        {
            var dataResponse = ex.Message.ToObject<DataResponse>();
            //await MessageBox.Error(dataResponse.Message);
        }
        StateHasChanged();
    }


}