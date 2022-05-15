namespace Udx.Admin.App.Pages.Orgs;
[ReuseTabsPageTitle("��֯��������")]
[Rule(Key = "admin.orgs-list", Actions = new string[] { "����","����","ɾ��"})]
public partial class List: UdxComponentBase
{
        [Inject] public IOrgService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        IEnumerable<OrgTree> _TreeData;
        OrgTree _selectedTreeData;

    List<OrgUserModel> _OrgUserList;
    ITable table;


    protected override async Task OnInitializedAsync()
        {
        _TreeData = new List<OrgTree>();
      

        _OrgUserList = new List<OrgUserModel>();
        _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(OrgModel.OrgName), Caption="��֯��������",OperatorSql="like"},
                                      new FilterItem(){ Field=nameof(OrgModel.OrgCode), Caption="��֯��������",OperatorSql="like"},
                                     new FilterItem(){ Field=nameof(OrgModel.Status), Caption="״̬",OperatorSql="="}
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
        /// <summary>
        /// ����ѯ
        /// </summary>
        /// <returns></returns>
        public async Task OnSearchAsync()
        {
            try
            {
            var request = new DataRequest<QueryModel>()
            {
                ObjectData = new QueryModel()
                {
                    CurrentPage = _SearchModel.CurrentPage,
                    PageSize = _SearchModel.PageSize,
                    FilterItems = _SearchModel.FilterItems
                },
                User = User
            };
            var dataResult = await DataService.TreeQueryAsync(request);
                if (dataResult.Successful)
                {
                   _TreeData = dataResult.ObjectData;
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


  

    public async Task OnTreeSelectAsync(OrgTree tree)
    {
        _selectedTreeData = tree;
        await OnOrgUserDetailInit(tree.Id);
    }


    #region OrgDetail
    // <summary>
    /// detail��ѯ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task OnOrgUserDetailInit(string id)
    {
        try
        {
            SaveOperater operaterType = string.IsNullOrEmpty(id) ? SaveOperater.Add : SaveOperater.Edit;
            _OrgUserList = new List<OrgUserModel>();
            if (SaveOperater.Edit == operaterType && !string.IsNullOrEmpty(id))
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = id,
                    User = User
                };
                var dataResult = await DataService.GetOrgUserAsync(request);
                if (dataResult.Successful)
                {
                    _OrgUserList = dataResult.ObjectData;
                }
                else
                {
                    await MessageBox.Error(dataResult.Message);

                }
            }
            else {
                _OrgUserList = new List<OrgUserModel>();
            }
        }
        catch (Exception ex)
        {
            var dataResponse = ex.Message.ToObject<DataResponse>();
            await MessageBox.Error(dataResponse.Message);
        }
        StateHasChanged();
    }

    public async Task OnAddDetailClick()
    {
        if (_selectedTreeData == null) {

            await MessageBox.Warn("����ѡ��һ����֯!");
            return;
        }
        var options = new ModalOptions()
        {
            Title = "��Ա�б�",
            Style = "width:900px;height:600px",
            OkText = "ȷ��",
            CancelText = "ȡ��",
            
        };
        var DialogModel = new DialogModel()
        {
            FilterItems = new List<FilterItem>(),
            SelectType = "radio"
        };

        var confirmRef = ModalService.CreateModalAsync<UserDialog, DialogModel, List<UserModel>>(options, DialogModel);

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
            var re = new OrgUserModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                UserId = result[0].Id,
                UserName = result[0].UserName,
                OrgId = _selectedTreeData.Id
            };
            //�������          
            var request = new DataRequest<SaveModel<OrgUserModel>>()
            {
                ObjectData = new SaveModel<OrgUserModel>()
                {
                    Operater = SaveOperater.Add,
                    Model = re
                },
                User = User
            };
            var response = await DataService.SaveOrgUserAsync(request);
            if (response.Successful)
            {
                _OrgUserList.Add(re);
                StateHasChanged();
            }
            else {             
              await MessageBox.Error(response.Message);
            }        
           // return Task.CompletedTask;
        };
    }
   
    async Task OnDeteteDetailClickAsync(OrgUserModel model)
    {
        //�������
      
        var request = new DataRequest<SaveModel<OrgUserModel>>()
        {
            ObjectData = new SaveModel<OrgUserModel>()
            {
                Operater = SaveOperater.Delete,
                Model = model
            },
            User = User
        };
        var response = await DataService.SaveOrgUserAsync(request);
        if (response.Successful)
        {
            _OrgUserList.Remove(model);
            StateHasChanged();
        }
        else
        {
            await MessageBox.Error(response.Message);
        }
    }
 

    #endregion
}
