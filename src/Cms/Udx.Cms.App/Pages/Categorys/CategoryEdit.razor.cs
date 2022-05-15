namespace Udx.Cms.App.Pages.Categorys;
[ReuseTabsPageTitle("分类管理")]
[Rule(Key = "cms.categorys-edit", Actions = new string[] { "新增", "保存", "删除" })]
public partial class CategoryEdit : UdxComponentBase
{
        [Inject] public ICmsCategoryService DataService { get; set; }

   
    protected override async Task OnInitializedAsync()
        {
           await base.OnInitializedAsync();
        }
}
