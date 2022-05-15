namespace Udx.Admin.App.Pages.Orgs;
[ReuseTabsPageTitle("组织机构管理")]
[Rule(Key = "admin.orgs-list", Actions = new string[] { "新增","保存","删除"})]
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
                                     new FilterItem(){ Field=nameof(OrgModel.OrgName), Caption="组织机构名称",OperatorSql="like"},
                                      new FilterItem(){ Field=nameof(OrgModel.OrgCode), Caption="组织机构代码",OperatorSql="like"},
                                     new FilterItem(){ Field=nameof(OrgModel.Status), Caption="状态",OperatorSql="="}
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
        /// 树查询
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
    /// detail查询
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

            await MessageBox.Warn("请先选择一个组织!");
            return;
        }
        var options = new ModalOptions()
        {
            Title = "人员列表",
            Style = "width:900px;height:600px",
            OkText = "确认",
            CancelText = "取消",
            
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
            //构造参数          
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
        //构造参数
      
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
