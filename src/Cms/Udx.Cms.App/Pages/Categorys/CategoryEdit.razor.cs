namespace Udx.Cms.App.Pages.Categorys;
[ReuseTabsPageTitle("�������")]
[Rule(Key = "cms.categorys-edit", Actions = new string[] { "����", "����", "ɾ��" })]
public partial class CategoryEdit : UdxComponentBase
{
        [Inject] public ICmsCategoryService DataService { get; set; }

   
    protected override async Task OnInitializedAsync()
        {
           await base.OnInitializedAsync();
        }
}
