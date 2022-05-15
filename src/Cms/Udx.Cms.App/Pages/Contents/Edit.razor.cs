namespace Udx.Cms.App.Pages.Contents;
[ReuseTabsPageTitle("内容编辑")]
[Rule(Key = "cms.contents-edit", Actions = new string[] { "保存"})]
public partial class Edit : UdxComponentBase
{
    [Inject] public ICmsContentService DataService { get; set; }
    [Inject] public ICmsCategoryService _cmsCategoryService { get; set; }
    IEnumerable<CmsCategoryTree> _categoryTree;
    CmsContentEditModel _editModel;
    SaveModel<CmsContentEditModel> _saveModel;
    [Parameter] public string Id { get; set; }
    [Parameter] public string Title { get; set; }
    TreeSelect<CmsCategoryTree> treeSelect;
    bool Add = true;
    Udx.Admin.Components.UdxVditor Vditor;
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _editModel = new CmsContentEditModel()
        {
            MessageInfo = "请填写内容信息~"
        };
        await GetCategorys();
        await OnInit(Id);
       
    }       
    private async  Task OnInit(string id)
    {
        try {
            var operaterType = (string.IsNullOrEmpty(id)|| id=="0") ? SaveOperater.Add : SaveOperater.Edit;
                
            if (SaveOperater.Edit == operaterType && !string.IsNullOrEmpty(id))
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = id,
                    User = User
                };
                var dataResponse = await DataService.GetAsync(request);
                if (dataResponse.Successful)
                {
                    _saveModel = new SaveModel<CmsContentEditModel>()
                    {
                        Operater = SaveOperater.Edit,
                        Model = dataResponse.ObjectData.Mapper<CmsContentEditModel>()
                    };
                    _editModel = _saveModel.Model;
                  
                }
                else {

                    _editModel.MessageInfo = dataResponse.Message;
                }
            }
            else
            {
                _saveModel = new SaveModel<CmsContentEditModel>()
                {
                    Operater = SaveOperater.Add,
                    Model = _editModel
                };
            }
        }
        catch (Exception ex)
        {

            Log.Error("Exception:{@ex}",ex);
            _editModel.MessageInfo = ex.Message;
        }
        StateHasChanged();
    }
    
    //protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
    //{
       
    //   // if(!firstRender)
    //     // await treeSelect.ValueChanged.InvokeAsync(_editModel.CategoryId);

    //    return base.OnAfterRenderAsync(firstRender);
    //}
    /// <summary>
    /// 分类查询
    /// </summary>
    /// <returns></returns>
    public async Task GetCategorys()
    {
        var request = new DataRequest<QueryModel>()
        {
            ObjectData = new QueryModel()
            {
                FilterItems = new List<FilterItem>(){
                new FilterItem(){ Field=nameof(CmsCategoryModel.Status), Caption="状态",OperatorSql="="}
                }
            }
        };
        var dataResult = await _cmsCategoryService.TreeQueryAsync(request);
        if (dataResult.Successful)
        {
            _categoryTree = dataResult.ObjectData;
        }
        else
        {
            // await MessageBox.Error(dataResult.Message);
        }
    }
    CmsCategoryTree _selectItemCmsCategoryTree;
    
    void CategoryIdSelectItemChanged(CmsCategoryTree item) {

        _selectItemCmsCategoryTree = item;
        _editModel.CategoryId = item?.Id;
        _editModel.CategoryTitle = item?.Name;
        
        StateHasChanged();
    }
    [Rule("保存")]
    private async Task OnFinish(EditContext editContext)
    {
        try
        {
            if (!editContext.Validate()) {

                _editModel.MessageInfo = "数据未验证，请检查！";
                return;
            }
            _saveModel.Model = _editModel;
            var request = new DataRequest<SaveModel<CmsContentEditModel>>()
            {
                ObjectData = _saveModel,
                User = User
            };
            var dataResult = await DataService.SaveAsync(request);
            if (dataResult.Successful)
            {

                await MessageBox.Success("保存成功！", 5);
                //返回的Code是租户新增的Id，通过Id在查询一次
                Id = dataResult.Code;
                await OnInit(Id);
                _editModel.MessageInfo = "保存成功,请返回~";
                Add = false;
            }
            else
            {
                await MessageBox.Error(dataResult.Message, 5);
                _editModel.MessageInfo = dataResult.Message;
            }
               
        }
        catch (Exception ex)
        {
            Log.Error("Exception:{@ex}", ex);
            await MessageBox.Error(ex.Message, 5);
            _editModel.MessageInfo = ex.Message;
        }
    }

    private async Task OnFinishFailed(EditContext editContext)
    {
        await MessageBoxBase.Error("保存失败!");
    }
    private void ContentsChanged(string value)
    {
        _editModel.Contents = value;
    }
    private void OnBack()
    {
        NavigationManagerBase.NavigateTo($"/cms/contents-list", true);
        return;
    }
    [Rule("新增")]
    private void OnAddClick()
    {
        NavigationManagerBase.NavigateTo($"/cms/contents-edit/0/新增",true);
    }
       
}