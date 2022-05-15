using Udx.Dbs.Entities;

namespace Udx.Cms.App.Pages.Contents
{
    [ReuseTabsPageTitle("内容管理")]
    [Rule(Key = "cms.contents-list", Actions = new string[] { "新增", "保存", "删除" })]
    public partial class List:UdxComponentBase
    {
        [Inject] public ICmsContentService DataService { get; set; }
        [Inject] public IRuleService RuleService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<CmsContentListResponse>> _QueryResponse;        
        IEnumerable<CmsContentListResponse> _SelectedRows;
        CmsContentListResponse _SelectedSingleRow;
        CmsCategoryTree _cmsCategoryTree = null;
        ITable table;

        protected override async Task OnInitializedAsync()
        {           
            _QueryResponse = new QueryResponse<IEnumerable<CmsContentListResponse>>() { 
             
                Data = new List<CmsContentListResponse>()
            };
            _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Title), Caption="标题",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Author), Caption="作者",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Contents), Caption="内容",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Summary), Caption="摘要",Operator= FilterOperator.Contains}
                },
                OrderBys = new List<OrderBy> { 
                    new OrderBy() { Field = nameof(CmsContentEntity.Sort), Order = SortingOrders.Desc },
                    new OrderBy() { Field = nameof(CmsContentEntity.CreatedTime), Order = SortingOrders.Desc }
                },
                SearchAction = OnSearchAsync
            };
            await _SearchModel.SearchAction();
        }

        #region Search
        public void SearchChange()
        {
            _SearchModel.Change();
        }      
        
        public async Task OnSearchAsync()
        {
            try
            {
                var filter = _SearchModel.FilterItems.Mapper<List<FilterItem>>();
                filter.Add(new FilterItem() { Field = nameof(CmsContentEntity.CategoryId), Caption = "分类", Operator = FilterOperator.Equal,Value= _cmsCategoryTree?.Id });
                var request = new DataRequest<QueryModel>()
                {
                    ObjectData = new QueryModel()
                    {
                        CurrentPage = _SearchModel.CurrentPage,
                        PageSize = _SearchModel.PageSize,
                        FilterItems = filter,
                        OrderBys = _SearchModel.OrderBys
                    },
                    User = User
                };
                var dataResult = await DataService.PageQueryAsync(request);
                if (dataResult.Successful)
                {
                    _QueryResponse = dataResult.ObjectData;
                    _SearchModel.RowsCount = _QueryResponse.RowsCount;
                }
                else { 
                    await MessageBox.Error(dataResult.Message);

                }
            }
            catch(Exception ex) {
                var dataResponse = ex.Message.ToObject<DataResponse>();
                await MessageBox.Error(dataResponse.Message);
            }
            StateHasChanged();
        }
        public async Task OnTreeSelectAsync(CmsCategoryTree tree)
        {
               _cmsCategoryTree = tree;
               await OnSearchAsync();
        }
        #endregion

        #region ToolBar
        [Rule("新增")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/cms/contents-edit/0/add",true);
        }
        [Rule("编辑")]
        async Task OnEditClick(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            NavigationManager.NavigateTo($"/cms/contents-edit/{id}/edit", true);
        }
        [Rule("预览")]
        async Task OnViewClick(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            NavigationManager.NavigateTo($"/cms/preview/{id}", true);
        }
        [Rule("删除")]
        async Task OnDeleteClick(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            //构造删除参数
            var request = new DataRequest<SaveModel<CmsContentEditModel>>()
            {
                ObjectData = new SaveModel<CmsContentEditModel>()
                {
                    Operater = SaveOperater.Delete,
                    Model = new CmsContentEditModel() { Id=id, Title= "Title-del", Contents= "Contents-del" }
                },
                User = User
            };
            var result = await DataService.SaveAsync(request);
            //重新查询
            await _SearchModel.SearchAction();           
            StateHasChanged();
        }
      
     

        #endregion
      
       
    }
}