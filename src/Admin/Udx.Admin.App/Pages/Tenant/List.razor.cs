using Udx.Admin.App.Components;
using Udx.Admin.App.Models;

namespace Udx.Admin.App.Pages.Tenants
{
    [ReuseTabsPageTitle("租户管理")]
    [Rule(Key = "admin.tenants-list", Actions = new string[] { "新增", "保存", "删除" })]
    public partial class List:UdxComponentBase
    {
        [Inject] public ITenantService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<TenantListOutput>> _QueryResponse;        
        IEnumerable<TenantListOutput> _SelectedRows;
        TenantListOutput _SelectedSingleRow;
        ITable table;

        protected override async Task OnInitializedAsync()
        {           
            _QueryResponse = new QueryResponse<IEnumerable<TenantListOutput>>() { 
             
                Data = new List<TenantListOutput>()
            };
            _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(TenantListOutput.Code), Caption="编码",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(TenantListOutput.Name), Caption="名称",Operator= FilterOperator.Contains}
                                   
                                },
                SearchAction = OnSearchAsync
            };
            await _SearchModel.SearchAction();
            SetSelection();
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
                var request = new DataRequest<QueryModel>()
                {
                    ObjectData = new QueryModel()
                    {
                        FilterItems = _SearchModel.FilterItems,
                        OrderBys = _SearchModel.OrderBys,
                        PageSize = _SearchModel.PageSize,
                        CurrentPage = _SearchModel.CurrentPage,
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
        void OnRowClick(AntDesign.TableModels.RowData<TenantListOutput> selectedRow)
        {
            table.SetSelection(new string[] { selectedRow.Data.Id });
        }
        #endregion

        #region ToolBar
        [Rule("新增")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/admin/tenants-edit?reload={System.DateTime.Now.ToLongTimeString()}");
        }
        [Rule("编辑")]
        async Task OnEditClick()
        {
            SetSelection();
            if (_SelectedSingleRow == null)
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            NavigationManager.NavigateTo($"/admin/tenants-edit?reload={System.DateTime.Now.ToLongTimeString()}&id={_SelectedSingleRow.Id}");
        }
        [Rule("删除")]
        async Task OnDeleteClick()
        {
            SetSelection();
            if (_SelectedSingleRow == null)
            {
                await MessageBox.Warning("没有选择的行！");
                return;

            }
            //构造删除参数
            var request = new DataRequest<SaveModel<TenantEditModel>>()
            {
                ObjectData = new SaveModel<TenantEditModel>()
                {
                    Operater = SaveOperater.Delete,
                    Model = _SelectedSingleRow.Mapper<TenantEditModel>()
                },
                User = User
            };
            var result = await DataService.SaveAsync(request);
            //重新查询
            await _SearchModel.SearchAction();           
            StateHasChanged();
        }
        void SetSelection()
        {
            if (_SelectedRows != null && _SelectedRows.Any())
                _SelectedSingleRow = _SelectedRows.FirstOrDefault();
            else
            {
                _SelectedRows = null;
                _SelectedSingleRow = null;
            }
        }

        #endregion
      
       
    }
}