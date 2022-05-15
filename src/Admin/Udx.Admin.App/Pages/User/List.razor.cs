using Udx.Admin.App.Components;
using Udx.Admin.App.Models;
using Udx.App.Pages.Exception;

namespace Udx.Admin.App.Pages.Users
{
    [ReuseTabsPageTitle("�û�����")]
    [Rule(Key = "admin.users-list", Actions = new string[] { "����", "����", "ɾ��","��ɫȨ��","��������","����", "����" })]
    public partial class List : UdxComponentBase
    {
        [Inject] public IUserService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<UserListOutput>> _QueryResponse;        
        IEnumerable<UserListOutput> _SelectedRows;
        UserListOutput _SelectedSingleRow;
        public bool loading { get; set; } = false;//�ڱ�


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
                                     new FilterItem(){ Field=nameof(UserListOutput.Name), Caption="����",Operator= FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(UserListOutput.NickName), Caption="�ǳ�",Operator=FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(UserListOutput.UserName), Caption="�˺�",Operator=FilterOperator.Contains},
                                     new FilterItem(){ Field=nameof(UserListOutput.Status), Caption="״̬",Operator=FilterOperator.Equal,Type=  FilterType.Select
                                         ,DataSource=Udx.Data.ConfigOptionData.Status },
                                     //new FilterItem(){ Field=nameof(UserListOutput.CreatedTime), Caption="����ʱ��",Value=System.DateTime.Now.ToString("yyyy-MM-dd"),Operator=FilterOperator.LessEqual,Type = FilterType.DateTime}
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
        [Rule("����")]
        void OnAddClick()
        {
            NavigationManager.NavigateTo($"/admin/users-edit/0/�û�����");
        }
        [Rule("�༭")]
        async Task OnEditClick(UserListOutput model)
        {
            if (model==null)
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;
            }
            NavigationManager.NavigateTo($"/admin/users-edit/{model.Id}/�û��༭");
        }
        [Rule("ɾ��")]
        async Task OnDeleteClick(UserListOutput model)
        {
            if (model == null)
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;
            }
            //����ɾ������
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
                await MessageBox.Info("ɾ���ɹ���");
                //���²�ѯ
                await _SearchModel.SearchAction();
                StateHasChanged();
            }
            else {
                await MessageBox.Error($"ɾ��ʧ��:{result.Message}");
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

        [Rule("��������")]
        async Task OnResetPwdClick()
        {
            if (_SelectedRows == null || _SelectedRows.Count()<=0)
            {
                await MessageBox.Warning("û��ѡ����У�");
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
                    await MessageBox.Success("���óɹ���");
                }
                else
                {
                    await MessageBox.Error("����ʧ�ܣ�");
                }
            }
        }


       [Rule("����")]
        async Task OnImportClick()
        {
            _ImportModel = Udx.Configs.AppSetting.UdxSettings.Import;
            _ImportModel.Visible = true;
            StateHasChanged();
            await Task.FromResult(true);
        }
        [Rule("����")]
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
                await MessageBox.Error("ʧ��:"+response.Message);
            }
        }
      

        #endregion
        [Rule("��ɫȨ��")]
        async void OnRoleSelectClick(UserListOutput user)
        {
            if (string.IsNullOrEmpty(user.Id)) {

                await MessageBox.Warning("��ѡ���û���");
                return ;
            }
                
            var options = new ConfirmOptions()
            {
                Title = "�û���ɫѡ��",
                Style = "width:900px;height:800px",
                OkText = "ȷ��ѡ��",
                CancelText = "ȡ���ر�",
                
                
                
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
                if (!RuleHelper.GetAction("��ɫ����"))
                {
                    await MessageBox.Success("��û�С���ɫ���桿Ȩ��!");
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
                    await MessageBox.Success("���ý�ɫ�ɹ�!");
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