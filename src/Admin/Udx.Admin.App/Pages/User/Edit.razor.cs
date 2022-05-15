namespace Udx.Admin.App.Pages.Users
{
   // [ReuseTabsPageTitle("用户编辑")]
    [Rule(Key = "admin.users-list", Actions = new string[] { "新增", "保存", "删除", "角色权限" })]
    public partial class Edit : UdxComponentBase, IReuseTabsPage
    {
        [Inject] public IUserService DataService { get; set; }

        UserEditModel _EditModel=new UserEditModel();
        SaveModel<UserEditModel> _SaveModel;
        [Parameter]
        [SupplyParameterFromQuery]
        public string Title { get; set; }
        [Parameter] [SupplyParameterFromQuery] public string Id { get; set; }

        bool Add = true;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Title = "用户新增";
            _SaveModel = new SaveModel<UserEditModel>()
            {
                Operater = SaveOperater.Add,
                Model = _EditModel
            };
            await OnInit(Id);

        }        
        private async  Task OnInit(string id)
        {
            try {
                SaveOperater operaterType = (string.IsNullOrEmpty(id)||id=="0") ? SaveOperater.Add : SaveOperater.Edit;
                _EditModel = new UserEditModel() {
                    MessageInfo = "请填写用户信息~",
                    Status = "禁用",
                    IsAdmin="否",
                };

                if (SaveOperater.Edit == operaterType && !string.IsNullOrEmpty(id))
                {
                    Title = "用户编辑";
                    var request = new DataRequest<string>()
                    {
                        ObjectData = id,
                        User = User
                    };
                    var dataResponse = await DataService.GetAsync(request);
                    if (dataResponse.Successful)
                    {
                        _SaveModel = new SaveModel<UserEditModel>()
                        {
                            Operater = SaveOperater.Edit,
                            Model = dataResponse.ObjectData.Mapper<UserEditModel>()
                        };
                        _EditModel = _SaveModel.Model;
                       
                    }
                    else {

                        _EditModel.MessageInfo = dataResponse.Message;
                    }
                }
                else
                {
                    Title = "用户新增";
                    _SaveModel = new SaveModel<UserEditModel>()
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
        [Rule("编辑")]
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                if (!editContext.Validate()) {

                    _EditModel.MessageInfo = "数据未验证，请检查！";
                    return;
                }
                _SaveModel.Model = _EditModel;
                var request = new DataRequest<SaveModel<UserEditModel>>()
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

                Console.WriteLine(ex.Message);
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
            NavigationManager.NavigateTo("/admin/users-list");
            return;
        }
        [Rule("新增")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/admin/users-edit/0/用户新增?reload={System.DateTime.Now.ToLongTimeString()}");
        }
        public RenderFragment GetPageTitle()
        {
            RenderFragment render = (builder) => builder.AddMarkupContent(0, $"<div><Icon Type = \"edit\" />{Title}</div>");
            return render;
        } 
    }
}