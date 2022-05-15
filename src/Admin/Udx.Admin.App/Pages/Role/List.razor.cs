using Udx.Admin.App.Components;
using Udx.Admin.App.Models;

namespace Udx.Admin.App.Pages.Roles;
[ReuseTabsPageTitle("��ɫ����")]
[Rule(Key = "admin.roles-list", Actions = new string[] { "����", "����", "ɾ��" })]
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
                                     new FilterItem(){ Field=nameof(RoleModel.RoleName), Caption="��ɫ����"},
                                        new FilterItem(){ Field=nameof(RoleModel.Status), Caption="״̬",Operator=FilterOperator.Equal,Type=FilterType.Select
                                         ,DataSource=Udx.Data.ConfigOptionData.Status},
                                        new FilterItem(){ Field=nameof(RoleModel.ModuleType), Caption="ģ��",Operator=FilterOperator.Equal,Type=FilterType.Select
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
    [Rule("����")]
    void OnAddClick()
        {
        NavigationManager.NavigateTo($"/admin/roles-edit/0/����",true);
    }
    [Rule("�༭")]
    async Task OnEditClick(RoleModel model)
        {
            if (model == null)
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;
            }
            NavigationManager.NavigateTo($"/admin/roles-edit/{model.Id}/�༭",true);
    }
    [Rule("ɾ��")]
    async Task OnDeleteClick(RoleModel model)
        {
            if (model == null)
            {
                await MessageBox.Warning("û��ѡ����У�");
                return;

            }
            //����ɾ������
           
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
            //���²�ѯ
            await _SearchModel.SearchAction();           
            StateHasChanged();
        }
      

    #endregion

    DateTime checkDateTime;

    [Rule("ģ��Ȩ��")]
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

            await MessageBox.Warning("��ѡ���ɫ��");
            return;
        }
        var options = new ConfirmOptions()
        {
            Title = "��ɫģ��ѡ��",
            Style = "width:1324px;height:800px",
            OkText = "ȷ��ѡ��",
            CancelText = "ȡ���ر�",


        };
        var dialogModel = new ModuleDialogModel()
        {
            Id = model.Id,
            Name ="��ɫ:"+ model.RoleName
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

            if (!RuleHelper.GetAction("Ȩ�ޱ���")) {
                await MessageBox.Success("��û�С�Ȩ�ޱ��桿Ȩ��!");
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
