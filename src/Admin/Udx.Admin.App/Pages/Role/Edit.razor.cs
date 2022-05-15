namespace Udx.Admin.App.Pages.Roles;
[ReuseTabsPageTitle("角色编辑")]
[Rule(Key = "admin.roles-list", Actions = new string[] { "保存" })]
public partial class Edit : UdxComponentBase
{
    [Inject] public IRoleService DataService { get; set; }

    RoleEdit _EditModel;
    SaveModel<RoleEdit> _SaveModel;
    [Parameter]
    [SupplyParameterFromQuery]
    public string Title { get; set; }
    [Parameter] [SupplyParameterFromQuery] public string Id { get; set; }
    public ConfigOption ModuleType { get; set; } = new ConfigOption();
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();       
        await OnInit(Id);
        ModuleType = Udx.Data.ConfigOptionData.ModuleType;
    }

    private async Task OnInit(string id)
    {
        try
        {
            SaveOperater operaterType = (string.IsNullOrEmpty(id) || id == "0") ? SaveOperater.Add : SaveOperater.Edit;
            _EditModel = new RoleEdit()
            {
                MessageInfo = "请填写角色信息~"

            };
            if (SaveOperater.Edit == operaterType && !string.IsNullOrEmpty(id))
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = id,
                    User = User
                };

                DataResponse<RoleModel> dataResponse = await DataService.GetAsync(request);
                if (dataResponse.Successful)
                {
                    _SaveModel = new SaveModel<RoleEdit>()
                    {
                        Operater = SaveOperater.Edit,
                        Model = dataResponse.ObjectData.Mapper<RoleEdit>()
                    };
                    _EditModel = _SaveModel.Model;
                }
                else
                {

                    _EditModel.MessageInfo = dataResponse.Message;
                }
            }
            else
            {
                _SaveModel = new SaveModel<RoleEdit>()
                {
                    Operater = SaveOperater.Add,
                    Model = _EditModel
                };
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            _EditModel.MessageInfo = ex.Message;
        }
        StateHasChanged();
    }
    [Rule("保存")]
    private async Task OnFinish(EditContext editContext)
    {
        try
        {
            if (!editContext.Validate())
            {

                _EditModel.MessageInfo = "数据未验证，请检查！";
                return;
            }
            _SaveModel.Model = _EditModel;
            var request = new DataRequest<SaveModel<RoleEdit>>()
            {
                ObjectData = _SaveModel,
                User = User
            };
            var dataResult = await DataService.SaveAsync(request);
            if (dataResult.Successful)
            {

                await MessageBox.Success("保存成功！", 5);
                //返回的Code是用户新增的Id，通过Id在查询一次
                Id = dataResult.Code;
                NavigationManager.NavigateTo($"/admin/roles-edit/{Id}/编辑");
            }
            else
            {
                await MessageBox.Error(dataResult.Message, 5);
                _EditModel.MessageInfo = dataResult.Message;
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            await MessageBox.Error(ex.Message, 5);
            _EditModel.MessageInfo = ex.Message;
        }
    }

    private async Task OnFinishFailed(EditContext editContext)
    {
        await MessageBox.Error("保存失败!");
    }

    public void OnBack()
    {
        NavigationManager.NavigateTo("/admin/roles-list");
        return;
    }
    [Rule("新增")]
    void OnAddClick()
    {
        NavigationManager.NavigateTo($"/admin/roles-edit/0/新增",true);
    }
   
}
