using Udx.Admin.App.Components;
using Udx.Admin.App.Models;
using Udx.App.Pages.Exception;

namespace Udx.Admin.App.Pages.Users
{
    [ReuseTabsPageTitle("用户管理")]
    [Rule(Key = "admin.users-list", Actions = new string[] { "新增", "保存", "删除","角色权限","重置密码","导入", "导出" })]
    public partial class List : UdxComponentBase
    {
        [Inject] public IUserService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<UserListOutput>> _QueryResponse;        
        IEnumerable<UserListOutput> _SelectedRows;
        UserListOutput _SelectedSingleRow;
        public bool loading { get; set; } = false;//遮蔽


        ImportModel _ImportModel;
        ExportModel _ExportModel;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _ImportModel = new ImportModel();
            _ExportModel = new ExportModel();
            _QueryResponse = new QueryResponse<IEnumerable<UserListOutput>>() { 
             
                Data = new List<UserListOutput>()
                 
            };
            _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(UserListOutput.Name), Caption="姓名",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(UserListOutput.NickName), Caption="昵称",Operator=FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(UserListOutput.UserName), Caption="账号",Operator=FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(UserListOutput.Status), Caption="状态",Operator=FilterOperator.Equal,Type=  FilterType.Select
                                         ,DataSource=Udx.Data.ConfigOptionData.Status },
                                     //new FilterItem(){ Field=nameof(UserListOutput.CreatedTime), Caption="创建时间",Value=System.DateTime.Now.ToString("yyyy-MM-dd"),Operator=FilterOperator.LessEqual,Type = FilterType.DateTime}
                                },
                OrderBys = new List<OrderBy>() { 
                   new OrderBy(){ Field=nameof(UserListOutput.UserName)}
                },
                SearchAction = OnSearchAsync
            };
            await _SearchModel.SearchAction();
            SetSelection();
        }

       

        #region Search           
        
        public async Task OnSearchAsync()
        {
            loading = true;
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
                Error.ProcessError(ex);
                Log.Debug("OnSearchAsync:{@ex}", ex);
                await MessageBox.Error(ex.Message);
            }
            loading = false;
            StateHasChanged();
        }       
        #endregion

        #region ToolBar
        [Rule("新增")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/admin/users-edit/0/用户新增");
        }
        [Rule("编辑")]
        async Task OnEditClick(UserListOutput model)
        {
            if (model==null)
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            NavigationManager.NavigateTo($"/admin/users-edit/{model.Id}/用户编辑");
        }
        [Rule("删除")]
        async Task OnDeleteClick(UserListOutput model)
        {
            if (model == null)
            {
                await MessageBox.Warning("没有选择的行！");
                return;
            }
            //构造删除参数
            var request = new DataRequest<SaveModel<UserEditModel>>()
                {
                    ObjectData = new SaveModel<UserEditModel>()
                    {
                        Operater = SaveOperater.Delete,
                        Model = model.Mapper<UserEditModel>() 
                    },
                    User = User
                };
                var result = await DataService.SaveAsync(request);
            if (result.Successful)
            {
                await MessageBox.Info("删除成功！");
                //重新查询
                await _SearchModel.SearchAction();
                StateHasChanged();
            }
            else {
                await MessageBox.Error($"删除失败:{result.Message}");
            }
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

        [Rule("重置密码")]
        async Task OnResetPwdClick()
        {
            if (_SelectedRows == null || _SelectedRows.Count()<=0)
            {
                await MessageBox.Warning("没有选择的行！");
            }
            else
            {
                var operaterModel = _SelectedRows.Mapper<List<UserEditModel>>();
                var request = new DataRequest<List<UserEditModel>>()
                {
                    ObjectData = operaterModel,
                    User = User
                };
                var result = await DataService.ResetPwdAsync(request);
                if(result.Successful)
                {
                    await MessageBox.Success("重置成功！");
                }
                else
                {
                    await MessageBox.Error("重置失败！");
                }
            }
        }


       [Rule("导入")]
        async Task OnImportClick()
        {
            _ImportModel = Udx.Configs.AppSetting.UdxSettings.Import;
            _ImportModel.Visible = true;
            StateHasChanged();
            await Task.FromResult(true);
        }
        [Rule("导出")]
        async Task OnExportClick()
        {
            var request = new DataRequest<QueryModel>()
            {
                ObjectData = new QueryModel()
                {
                    PageSize = _SearchModel.PageSize,
                    CurrentPage = _SearchModel.CurrentPage,
                    FilterItems = _SearchModel.FilterItems,
                    OrderBys = _SearchModel.OrderBys,

                },
                User = User
            };
            var response = await DataService.ExportAsync(request);
            if (response.Successful)
            {
                _ExportModel = response.ObjectData;
                _ExportModel.Visible = true;
                StateHasChanged();
            }
            else
            {
                await MessageBox.Error("失败:"+response.Message);
            }
        }
      

        #endregion
        [Rule("角色权限")]
        async void OnRoleSelectClick(UserListOutput user)
        {
            if (string.IsNullOrEmpty(user.Id)) {

                await MessageBox.Warning("请选择用户。");
                return ;
            }
                
            var options = new ConfirmOptions()
            {
                Title = "用户角色选择",
                Style = "width:900px;height:800px",
                OkText = "确认选择",
                CancelText = "取消关闭",
                
                
                
            };
            var dialogModel = new RoleDialogModel()
            {    
                Type= RoleDialogType.User,
                Id= user.Id,
                SelectType =  SelectType.checkbox,
                Name = user.Name

            };

             var confirmRef = ModalService.CreateConfirmAsync<RoleDialog, Udx.Admin.App.Models.RoleDialogModel, List<RoleUserModel>>(options, dialogModel);

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
                if (!RuleHelper.GetAction("角色保存"))
                {
                    await MessageBox.Success("你没有【角色保存】权限!");
                    return;
                }
                var request = new DataRequest<RoleUserSaveModel>()
                {
                    ObjectData = new RoleUserSaveModel()
                    {
                        UserId = user.Id,
                        Models = result
                    },
                    User = User
                };
                var response = await RuleService.SaveRoleUserAsync(request);
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
}