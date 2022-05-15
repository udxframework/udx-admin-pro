namespace Udx.Admin.App.Pages.Orgs;
[ReuseTabsPageTitle("��֯��������")]
[Rule(Key = "admin.orgs-list", Actions = new string[] { "����", "����", "ɾ��" })]
public partial class OrgEditTree : UdxComponentBase
{
        [Inject] public IOrgService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        IEnumerable<OrgTree> _TreeData;
    OrgTree _selectedTreeData;        
        string searchKey { get; set; }
        Form<ConfigEdit> _Form;
        ConfigEdit _EditModel { get; set; }
        SaveModel<ConfigEdit> _SaveModel { get; set; }
         public string Id { get; set; }
         public bool formVisible { get; set; }
    private DynamicComponent? _categoryEditForm;
    Type type = typeof(OrgEditForm);


    public ConfigOption CmsCategoryGroup { get; set; }

    [Parameter]
    public EventCallback<OrgTree> OnTreeSelect { get; set; }


    protected override async Task OnInitializedAsync()
        {
        _TreeData = new List<OrgTree>();
        _EditModel = new ConfigEdit()
        {
            MessageInfo = ""

        };

        _SaveModel = new SaveModel<ConfigEdit>()
        {
            Operater = SaveOperater.Add
        };
            _SearchModel = new SearchModel<List<FilterItem>>() {
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


    public async Task OnTreeSelectAsync(TreeEventArgs<OrgTree> args)
    {
        var item = args.Node.DataItem;
        _selectedTreeData = item;
        Id = _selectedTreeData?.Id;        
        await  OnTreeSelect.InvokeAsync(item);
    }

    #region ToolBar
    [Rule("����")]
    private async Task OnTreeAddClick(TreeNode<OrgTree> originNode)
        {
        _selectedTreeData = originNode?.DataItem;
        type = typeof(Text);
        type = typeof(OrgEditForm);
        var edit = _categoryEditForm?.Instance as OrgEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Add;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("�༭")]
    private async Task OnTreeEditClick(TreeNode<OrgTree> originNode)
    {
        _selectedTreeData = originNode.DataItem;

        type = typeof(Text);
        type = typeof(OrgEditForm);
        var edit = _categoryEditForm?.Instance as OrgEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Edit;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("ɾ��")]
    private async Task OnTreeDeleteClick(TreeNode<OrgTree> originNode)
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

    private async Task OnDetete(OrgTree selectedTreeData)
    {
        try
        {
            var request = new DataRequest<SaveModel<OrgEdit>>()
            {
                ObjectData = new SaveModel<OrgEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = new OrgEdit() { 
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
