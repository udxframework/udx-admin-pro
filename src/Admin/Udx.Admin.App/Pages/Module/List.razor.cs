namespace Udx.Admin.App.Pages.Modules;
[ReuseTabsPageTitle("模块管理")]
[Rule(Key = "admin.modules-list", Actions = new string[] { "新增", "保存", "删除" })]
public partial class List : UdxComponentBase
{
        [Inject] public IModuleService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        IEnumerable<ModuleTree> _TreeData;
        ModuleTree _selectedTreeData;
     

    bool _loading = false;
    public ConfigOption ModuleType { get; set; }




    protected override async Task OnInitializedAsync()
        {
        
        _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(ModuleModel.Name), Caption="模块名称",OperatorSql="like"},
                                      new FilterItem(){ Field=nameof(ModuleModel.Key), Caption="模块Key",OperatorSql="like"},
                                     new FilterItem(){ Field=nameof(ModuleModel.Status), Caption="状态",OperatorSql="="}
                                },
                SearchAction = OnSearchAsync
            };
            await _SearchModel.SearchAction();
        }

    

        #region Search
       
        /// <summary>
        /// 树查询
        /// </summary>
        /// <returns></returns>
        public async Task OnSearchAsync()
        {
            try
            {
                _loading = true;

            var request = new DataRequest<QueryModel>()
            {
                ObjectData = new QueryModel()
                {
                    CurrentPage = _SearchModel.CurrentPage,
                    PageSize = _SearchModel.PageSize,
                    FilterItems = _SearchModel.FilterItems
                },
                User = User
            };

            var dataResult = await DataService.TreeQueryAsync(request);

           

            if (dataResult.Successful)
                {
                   _TreeData = dataResult.ObjectData;
                }
                else { 
                    await MessageBox.Error(dataResult.Message);

                }
            }
            catch(Exception ex) {
               var dataResponse = ex.Message.ToObject<DataResponse>();
                await MessageBox.Error(dataResponse.Message);
            }
            finally
            {
                _loading = false;

            }
            StateHasChanged();
    }


    #endregion

    public async Task OnTreeSelectAsync(ModuleTree tree)
    {
        _selectedTreeData = tree;
        //await OnConfigDetailInit(tree.Id);
    }


}
