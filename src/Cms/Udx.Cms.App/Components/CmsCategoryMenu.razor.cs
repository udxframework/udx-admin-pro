namespace Udx.Cms.App.Components;
    public partial class CmsCategoryMenu: ComponentBase
    {
        IEnumerable<CmsCategoryTree> _categoryTree;
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
                   new FilterItem(){ Field=nameof(CmsCategoryModel.Status), Caption="状态",OperatorSql="="}
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

    public static string GetLink(string key) {

        return $"/cms/list/{key}";
    }
}