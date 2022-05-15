using Udx.Admin.App.Components;
using Udx.Admin.App.Models;

namespace Udx.Admin.App.Pages.Roles;
[ReuseTabsPageTitle("角色管理")]
[Rule(Key = "admin.roles-list", Actions = new string[] { "新增", "保存", "删除" })]
public partial class List : UdxComponentBase
{
        [Inject] public IRoleService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<RoleModel>> _QueryResponse;   
        ITable table;

        protected override async Task OnInitializedAsync()
        {           
            _QueryResponse = new QueryResponse<IEnumerable<RoleModel>>() { 
             
                Data = new List<RoleModel>()
            };
            _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(RoleModel.RoleName), Caption="角色名称"},
                                        new FilterItem(){ Field=nameof(RoleModel.Status), Caption="状态",Operator=FilterOperator.Equal,Type=FilterType.Select
                                         ,DataSource=Udx.Data.ConfigOptionData.Status},
                                        new FilterItem(){ Field=nameof(RoleModel.ModuleType), Caption="模块",Operator=FilterOperator.Equal,Type=FilterType.Select
                                         ,DataSource=Udx.Data.ConfigOptionData.ModuleType.Options.ToList() }
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
       
    #endregion

    #region ToolBar
    [Rule("新增")]
    void OnAddClick()
        {
        NavigationManager.NavigateTo($"/admin/roles-edit/0/新增",true);
    }
    [Rule("编辑")]
    async Task OnEditClick(RoleModel model)
        {
            if (model == null)
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            NavigationManager.NavigateTo($"/admin/roles-edit/{model.Id}/编辑",true);
    }
    [Rule("删除")]
    async Task OnDeleteClick(RoleModel model)
        {
            if (model == null)
            {
                await MessageBox.Warning("没有选择的行！");
                return;

            }
            //构造删除参数
           
        var request = new DataRequest<SaveModel<RoleEdit>>()
        {
            ObjectData = new SaveModel<RoleEdit>()
            {
                Operater = SaveOperater.Delete,
                Model = model.Mapper<RoleEdit>()
            },
            User = User
        };
        var result = await DataService.SaveAsync(request);
            //重新查询
            await _SearchModel.SearchAction();           
            StateHasChanged();
        }
      

    #endregion

    DateTime checkDateTime;

    [Rule("模块权限")]
    async Task OnModuleSetClick(RoleModel model,DateTime dateTime)
    {
        if (dateTime.Subtract(checkDateTime).TotalSeconds < 3)
        {
            return;
        }
        else
        {
            checkDateTime = dateTime;
        }

        if (string.IsNullOrEmpty(model.Id))
        {

            await MessageBox.Warning("请选择角色。");
            return;
        }
        var options = new ConfirmOptions()
        {
            Title = "角色模块选择",
            Style = "width:1324px;height:800px",
            OkText = "确认选择",
            CancelText = "取消关闭",


        };
        var dialogModel = new ModuleDialogModel()
        {
            Id = model.Id,
            Name ="角色:"+ model.RoleName
        };

        var confirmRef = ModalService.CreateConfirmAsync<RoleModuleDialog, ModuleDialogModel, List<RoleModuleModel>>(options, dialogModel);

        confirmRef.Result.OnOpen = () =>
        {
            return Task.CompletedTask;
        };

        confirmRef.Result.OnClose = () =>
        {
            return Task.CompletedTask;
        };

        confirmRef.Result.OnOk = async (result) =>
        {

            if (!RuleHelper.GetAction("权限保存")) {
                await MessageBox.Success("你没有【权限保存】权限!");
                return;
            }

            var request = new DataRequest<RoleModuleSaveModel>()
            {
                ObjectData = new RoleModuleSaveModel()
                {
                    RoleId = model.Id,
                    Models = result
                },
                User = User
            };
            var response = await RuleService.SaveRoleModuleAsync(request);
            if (response.Successful)
            {
                await MessageBox.Success("设置角色成功!");
                StateHasChanged();
            }
            else
            {
                await MessageBox.Error(response.Message);
            }
            // return Task.CompletedTask;
        };
    }
}
