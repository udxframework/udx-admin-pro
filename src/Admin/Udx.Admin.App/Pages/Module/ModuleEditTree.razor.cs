namespace Udx.Admin.App.Pages.Modules;
[ReuseTabsPageTitle("模块管理")]
[Rule(Key = "admin.modules-list", Actions = new string[] { "新增", "保存", "删除" })]
public partial class ModuleEditTree : UdxComponentBase
{
        [Inject] public IModuleService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        IEnumerable<ModuleTree> _TreeData;
    ModuleTree _selectedTreeData;        
        string searchKey { get; set; }
        Form<ModuleEdit> _Form;
        ModuleEdit _EditModel { get; set; }
        SaveModel<ModuleEdit> _SaveModel { get; set; }
         public string Id { get; set; }
         public bool formVisible { get; set; }
    private DynamicComponent? _categoryEditForm;
    Type type = typeof(ModuleEditForm);


    public ConfigOption CmsCategoryGroup { get; set; }

    [Parameter]
    public EventCallback<ModuleTree> OnTreeSelect { get; set; }


    protected override async Task OnInitializedAsync()
        {
        _TreeData = new List<ModuleTree>();
        _EditModel = new ModuleEdit()
        {
            MessageInfo = ""

        };

        _SaveModel = new SaveModel<ModuleEdit>()
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
        /// 树查询
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


    public async Task OnTreeSelectAsync(TreeEventArgs<ModuleTree> args)
    {
        var item = args.Node.DataItem;
        _selectedTreeData = item;
        Id = _selectedTreeData?.Id;        
        await  OnTreeSelect.InvokeAsync(item);
    }

    #region ToolBar
    [Rule("新增")]
    private async Task OnTreeAddClick(TreeNode<ModuleTree> originNode)
        {
        _selectedTreeData = originNode?.DataItem;
        type = typeof(Text);
        type = typeof(ModuleEditForm);
        var edit = _categoryEditForm?.Instance as ModuleEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Add;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("编辑")]
    private async Task OnTreeEditClick(TreeNode<ModuleTree> originNode)
    {
        _selectedTreeData = originNode.DataItem;

        type = typeof(Text);
        type = typeof(ModuleEditForm);
        var edit = _categoryEditForm?.Instance as ModuleEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Edit;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("删除")]
    private async Task OnTreeDeleteClick(TreeNode<ModuleTree> originNode)
    {
        _selectedTreeData = originNode.DataItem;
        if (originNode.DataItem.Items.Any())
        {
            await MessageBox.Error("有下级节点不能删除！", 5);
            return;
        }
        await OnDetete(_selectedTreeData);
        StateHasChanged();
    }

    private async Task OnDetete(ModuleTree selectedTreeData)
    {
        try
        {
            var request = new DataRequest<SaveModel<ModuleEdit>>()
            {
                ObjectData = new SaveModel<ModuleEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = new ModuleEdit() { 
                      Id= selectedTreeData.Id
                    }
                },
                User = User
            };

            var dataResult = await DataService.SaveAsync(request);
            if (dataResult.Successful)
            {
                //重新查询
                await _SearchModel.SearchAction();
            }
            else
            {
                await MessageBox.Error(dataResult.Message);

            }           
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
