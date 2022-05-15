namespace Udx.Admin.App.Pages.Configs;
[ReuseTabsPageTitle("配置中心")]
[Rule(Key = "admin.configs-list", Actions = new string[] { "新增", "保存", "删除" })]
public partial class List:UdxComponentBase
{
        [Inject] public IConfigService DataService { get; set; }

    ConfigTree _selectedTreeData;
    List<ConfigDetailModel> _configDetailList;
    ITable table;
     

    protected override async Task OnInitializedAsync()
        {
        _configDetailList = new List<ConfigDetailModel>();
    }

  

    
    /// <summary>
    /// detail查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task OnConfigDetailInit(string id)
    {
        try
        {
            SaveOperater operaterType = string.IsNullOrEmpty(id) ? SaveOperater.Add : SaveOperater.Edit;
            _configDetailList = new List<ConfigDetailModel>();
            if (SaveOperater.Edit == operaterType && !string.IsNullOrEmpty(id))
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = id,
                    User = User
                };
                var dataResult = await DataService.GetConfigDetailListAsync(request);
                if (dataResult.Successful)
                {
                    _configDetailList = dataResult.ObjectData;
                }
                else
                {
                    await MessageBox.Error(dataResult.Message);

                }
            }
        }
        catch (Exception ex)
        {
            var dataResponse = ex.Message.ToObject<DataResponse>();
            await MessageBox.Error(dataResponse.Message);
        }
        StateHasChanged();
    }


    public async Task OnTreeSelectAsync(ConfigTree tree)
    {
        _selectedTreeData = tree;
        await OnConfigDetailInit(tree.Id);
    }

    #region ToolBar

  


    [Rule("保存")]

    private async Task OnSaveClick()
    {
        try
        {
            ConfigSaveModel saveModel = new ConfigSaveModel();
           
            //子表验证
            saveModel.ConfigId = _selectedTreeData.Id;
            saveModel.ConfigDetails = _configDetailList.Mapper<List<ConfigDetailEdit>>();
            var request = new DataRequest<ConfigSaveModel>()
            {
                ObjectData = saveModel,
                User = User
            };
            var dataResult = await DataService.SaveConfigAsync(request);
            if (dataResult.Successful)
            {
                await MessageBox.Success("保存成功！", 5);
            }
            else
            {
                await MessageBox.Error(dataResult.Message, 5);
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            await MessageBox.Error(ex.Message, 5);
        }
    }

   


    #region Detail
    [Rule("新增")]
    private async Task OnAddDetailClick()
    {
        if (_selectedTreeData != null && !string.IsNullOrEmpty(_selectedTreeData.Id))
        {
            _configDetailList.Add(new ConfigDetailModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                ConfigId = _selectedTreeData.Id

            });
        }
        else
        {

            await MessageBox.Error("请先选择或新增配置!");
        }
        StateHasChanged();
    }

    [Rule("删除")]
    void OnDeteteDetailClick(ConfigDetailModel model)
    {
        _configDetailList.Remove(model);
        StateHasChanged();
    }
    
 

    #endregion
    #endregion


}
