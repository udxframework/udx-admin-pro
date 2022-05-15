namespace Udx.Admin.App.Pages.Configs;
[ReuseTabsPageTitle("��������")]
[Rule(Key = "admin.configs-list", Actions = new string[] { "����", "����", "ɾ��" })]
public partial class ConfigEditTree : UdxComponentBase
{
        [Inject] public IConfigService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        IEnumerable<ConfigTree> _TreeData;
        ConfigTree _selectedTreeData;        
        string searchKey { get; set; }
        Form<ConfigEdit> _Form;
        ConfigEdit _EditModel { get; set; }
        SaveModel<ConfigEdit> _SaveModel { get; set; }
         public string Id { get; set; }
         public bool formVisible { get; set; }
    private DynamicComponent? _categoryEditForm;
    Type type = typeof(ConfigEditForm);


    public ConfigOption CmsCategoryGroup { get; set; }

    [Parameter]
    public EventCallback<ConfigTree> OnTreeSelect { get; set; }


    protected override async Task OnInitializedAsync()
        {
        _TreeData = new List<ConfigTree>();
        _EditModel = new ConfigEdit()
        {
            MessageInfo = ""

        };

        _SaveModel = new SaveModel<ConfigEdit>()
        {
            Operater = SaveOperater.Add
        };
        _SearchModel = new SearchModel<List<FilterItem>>() {
            FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(ConfigModel.ConfigKey), Caption="����Key",OperatorSql="like"},
                                     new FilterItem(){ Field=nameof(ConfigModel.Status), Caption="״̬",OperatorSql="="}
                                },
            SearchAction = OnSearchAsync
            };
            await _SearchModel.SearchAction();
        }

    

        #region Search
       
        /// <summary>
        /// ����ѯ
        /// </summary>
        /// <returns></returns>
        public async Task OnSearchAsync()
        {
            try
            {
            formVisible = false;
            var request = new DataRequest<QueryModel>()
            {
                ObjectData = new QueryModel()
                {
                    IsNotPage = true,
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
            finally
            {

            }
            StateHasChanged();
    }
  
    
    #endregion


    public async Task OnTreeSelectAsync(TreeEventArgs<ConfigTree> args)
    {
        var item = args.Node.DataItem;
        _selectedTreeData = item;
        Id = _selectedTreeData?.Id;        
        await  OnTreeSelect.InvokeAsync(item);
    }

    #region ToolBar
    [Rule("����")]
    private async Task OnTreeAddClick(TreeNode<ConfigTree> originNode)
        {
        _selectedTreeData = originNode?.DataItem;
        type = typeof(Text);
        type = typeof(ConfigEditForm);
        var edit = _categoryEditForm?.Instance as ConfigEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Add;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("�༭")]
    private async Task OnTreeEditClick(TreeNode<ConfigTree> originNode)
    {
        _selectedTreeData = originNode.DataItem;

        type = typeof(Text);
        type = typeof(ConfigEditForm);
        var edit = _categoryEditForm?.Instance as ConfigEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Edit;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("ɾ��")]
    private async Task OnTreeDeleteClick(TreeNode<ConfigTree> originNode)
    {
        _selectedTreeData = originNode.DataItem;
        if (originNode.DataItem.Items.Any())
        {
            await MessageBox.Error("���¼��ڵ㲻��ɾ����", 5);
            return;
        }
        await OnDetete(_selectedTreeData);
        StateHasChanged();
    }

    private async Task OnDetete(ConfigTree selectedTreeData)
    {
        try
        {
            var request = new DataRequest<SaveModel<ConfigEdit>>()
            {
                ObjectData = new SaveModel<ConfigEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = new ConfigEdit() { 
                      Id= selectedTreeData.Id
                    }
                },
                User = User
            };

            var result = await DataService.SaveAsync(request);
            //���²�ѯ
            await _SearchModel.SearchAction();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            await MessageBox.Error(ex.Message);
        }
        finally
        {
        }
    }


    #endregion


}
