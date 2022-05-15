namespace Udx.Cms.App.Components;
    public partial class CmsCategoryTreeList: ComponentBase
    {
    IEnumerable<CmsCategoryTree> _categoryTree;
    string searchKey { get; set; }
    [Parameter] public string MenuKey { get; set; }
    [Inject] public ICmsCategoryService _cmsCategoryService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            await GetCategorys();
        }
        /// <summary>
        /// 分类查询
        /// </summary>
        /// <returns></returns>
        public async Task GetCategorys()
        {
            var request = new DataRequest<QueryModel>()
            {
                ObjectData = new QueryModel()
                {
                    FilterItems = new List<FilterItem>(){
                    new FilterItem(){ Field=nameof(CmsCategoryModel.Status), Caption="状态",Operator= FilterOperator.Equal, Value="启用"},

                    new FilterItem(){ Field=nameof(CmsCategoryModel.Group), Caption="分组",Operator= FilterOperator.Equal, Value="Contents"}
                  }
                }
            };
            var dataResult = await _cmsCategoryService.TreeQueryAsync(request);
            if (dataResult.Successful)
            {
                _categoryTree = dataResult.ObjectData;
            }
            else
            {
                // await MessageBox.Error(dataResult.Message);
            }
        }
  
    public void  OnSelectAsync(TreeEventArgs<CmsCategoryTree> args)
    {
        var item = args.Node.DataItem;
        NavigationManager.NavigateTo($"/cms/list/{item.Id}",true);
    }

    public static string GetLink(string key) {

        return $"/cms/list/{key}/{System.DateTime.Now.GetFormatString("ffff")}/";
    }
}