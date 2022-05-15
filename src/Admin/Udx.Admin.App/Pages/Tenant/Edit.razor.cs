namespace Udx.Admin.App.Pages.Tenants
{
    [ReuseTabsPageTitle("租户编辑")]
    [Rule(Key = "admin.tenants-edit", Actions = new string[] { "保存"})]
    public partial class Edit : UdxComponentBase
    {
        [Inject] public ITenantService DataService { get; set; }

        TenantEditModel _EditModel;
        SaveModel<TenantEditModel> _SaveModel;
        [Parameter] public string Id { get; set; }
        bool Add = true;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var query = new Uri(NavigationManager.Uri).Query;
            if (QueryHelpers.ParseQuery(query).TryGetValue("id", out var pid))
            {
                Log.Debug(NavigationManager.Uri);
                Log.Debug("Id=" + pid);
                Id = pid;
            }
            await OnInit(Id);

        }       
        private async  Task OnInit(string id)
        {
            try {
                SaveOperater operaterType = string.IsNullOrEmpty(id) ? SaveOperater.Add : SaveOperater.Edit;
                _EditModel = new TenantEditModel() {
                    MessageInfo = "请填写租户信息~"
                
                };
                if (SaveOperater.Edit == operaterType && !string.IsNullOrEmpty(id))
                {
                    var request = new DataRequest<string>()
                    {
                        ObjectData = id,
                        User = User
                    };
                    var dataResponse = await DataService.GetAsync(request);
                    if (dataResponse.Successful)
                    {
                        _SaveModel = new SaveModel<TenantEditModel>()
                        {
                            Operater = SaveOperater.Edit,
                            Model = dataResponse.ObjectData.Mapper<TenantEditModel>()
                        };
                        _EditModel = _SaveModel.Model;
                    }
                    else {

                        _EditModel.MessageInfo = dataResponse.Message;
                    }
                }
                else
                {
                    _SaveModel = new SaveModel<TenantEditModel>()
                    {
                        Operater = SaveOperater.Add,
                        Model = _EditModel
                    };
                }
            }
            catch (Exception ex)
            {

                Log.Error("Exception:{@ex}",ex);
                _EditModel.MessageInfo = ex.Message;
            }
            StateHasChanged();
        }
        [Rule("保存")]
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                if (!editContext.Validate()) {

                    _EditModel.MessageInfo = "数据未验证，请检查！";
                    return;
                }
                _SaveModel.Model = _EditModel;
                var request = new DataRequest<SaveModel<TenantEditModel>>()
                {
                    ObjectData = _SaveModel,
                    User = User
                };
                var dataResult = await DataService.SaveAsync(request);
                if (dataResult.Successful)
                {

                    await MessageBox.Success("保存成功！", 5);
                    //返回的Code是租户新增的Id，通过Id在查询一次
                    Id = dataResult.Code;
                    await OnInit(Id);
                    _EditModel.MessageInfo = "保存成功,请返回~";
                    Add = false;
                }
                else
                {
                    await MessageBox.Error(dataResult.Message, 5);
                    _EditModel.MessageInfo = dataResult.Message;
                }
               
            }
            catch (Exception ex)
            {
                Log.Error("Exception:{@ex}", ex);
                await MessageBox.Error(ex.Message, 5);
                _EditModel.MessageInfo = ex.Message;
            }
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await MessageBox.Error("保存失败!");
        }

        public  void OnBack()
        {
            NavigationManager.NavigateTo("/admin/tenants-list");
            return;
        }
        [Rule("新增")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/admin/tenants-edit?reload={System.DateTime.Now.ToLongTimeString()}");
        }
       
    }
}