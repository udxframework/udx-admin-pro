namespace Udx.Cms.App.Pages.Categorys;
[ReuseTabsPageTitle("�������")]
[Rule(Key = "cms.categorys-edit", Actions = new string[] { "����", "����", "ɾ��" })]
public partial class CategoryEditTree : UdxComponentBase
{
        [Inject] public ICmsCategoryService DataService { get; set; }
        SearchModel<List<FilterItem>> _SearchModel;
        IEnumerable<CmsCategoryTree> _TreeData;
        CmsCategoryTree _selectedTreeData;        
        string searchKey { get; set; }
        Form<CmsCategoryEdit> _Form;
        CmsCategoryEdit _EditModel { get; set; }
        SaveModel<CmsCategoryEdit> _SaveModel { get; set; }
         public string Id { get; set; }
         public bool formVisible { get; set; }
    private DynamicComponent? _categoryEditForm;
    Type type = typeof(CategoryEditForm);


    public ConfigOption CmsCategoryGroup { get; set; }

    [Parameter]
    public EventCallback<CmsCategoryTree> OnTreeSelect { get; set; }


    protected override async Task OnInitializedAsync()
        {
        _TreeData = new List<CmsCategoryTree>();
        _EditModel = new CmsCategoryEdit()
        {
            MessageInfo = ""

        };

        CmsCategoryGroup = Udx.Data.ConfigOptionData.CmsCategoryType;
        _SaveModel = new SaveModel<CmsCategoryEdit>()
        {
            Operater = SaveOperater.Add
        };
        _SearchModel = new SearchModel<List<FilterItem>>() {
                FilterItems = new List<FilterItem>(){
                                     new FilterItem(){ Field=nameof(CmsCategoryModel.Name), Caption="ģ������",OperatorSql="like"},
                                     new FilterItem(){ Field=nameof(CmsCategoryModel.Status), Caption="״̬",OperatorSql="="}
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


    public async Task OnTreeSelectAsync(TreeEventArgs<CmsCategoryTree> args)
    {
        var item = args.Node.DataItem;
        _selectedTreeData = item;
        Id = _selectedTreeData?.Id;        
        await  OnTreeSelect.InvokeAsync(item);
    }

    #region ToolBar
    [Rule("����")]
    private async Task OnTreeAddClick(TreeNode<CmsCategoryTree> originNode)
        {
        _selectedTreeData = originNode?.DataItem;
        type = typeof(Text);
        type = typeof(CategoryEditForm);
        var edit = _categoryEditForm?.Instance as CategoryEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Add;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("�༭")]
    private async Task OnTreeEditClick(TreeNode<CmsCategoryTree> originNode)
    {
        _selectedTreeData = originNode.DataItem;

        type = typeof(Text);
        type = typeof(CategoryEditForm);
        var edit = _categoryEditForm?.Instance as CategoryEditForm;
        edit.Visible = true;
        edit.Id = _selectedTreeData?.Id;
        edit.Operater = SaveOperater.Edit;
        await edit.OnInit();
        StateHasChanged();
    }
    [Rule("ɾ��")]
    private async Task OnTreeDeleteClick(TreeNode<CmsCategoryTree> originNode)
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

    private async Task OnDetete(CmsCategoryTree selectedTreeData)
    {
        try
        {
            var request = new DataRequest<SaveModel<CmsCategoryEdit>>()
            {
                ObjectData = new SaveModel<CmsCategoryEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = new CmsCategoryEdit() { 
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
