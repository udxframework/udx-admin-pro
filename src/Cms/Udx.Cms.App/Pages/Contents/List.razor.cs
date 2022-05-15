using Udx.Dbs.Entities;

namespace Udx.Cms.App.Pages.Contents
{
    [ReuseTabsPageTitle("���ݹ���")]
    [Rule(Key = "cms.contents-list", Actions = new string[] { "����", "����", "ɾ��" })]
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
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Title), Caption="����",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Author), Caption="����",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Contents), Caption="����",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(CmsContentEntity.Summary), Caption="ժҪ",Operator= FilterOperator.Contains}
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
                filter.Add(new FilterItem() { Field = nameof(CmsContentEntity.CategoryId), Caption = "����", Operator = FilterOperator.Equal,Value= _cmsCategoryTree?.Id });
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
        [Rule("����")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/cms/contents-edit/0/add",true);
        }
        [Rule("�༭")]
        async Task OnEditClick(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;
            }
            NavigationManager.NavigateTo($"/cms/contents-edit/{id}/edit", true);
        }
        [Rule("Ԥ��")]
        async Task OnViewClick(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;
            }
            NavigationManager.NavigateTo($"/cms/preview/{id}", true);
        }
        [Rule("ɾ��")]
        async Task OnDeleteClick(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;
            }
            //����ɾ������
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
            //���²�ѯ
            await _SearchModel.SearchAction();           
            StateHasChanged();
        }
      
     

        #endregion
      
       
    }
}