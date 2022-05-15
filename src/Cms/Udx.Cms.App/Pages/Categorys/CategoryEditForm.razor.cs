namespace Udx.Cms.App.Pages.Categorys;
[Rule(Key = "cms.categorys-edit", Actions = new string[] { "新增", "保存", "删除" })]
public partial class CategoryEditForm : UdxComponentBase
{
        [Inject] public ICmsCategoryService DataService { get; set; }
    public bool Visible { get; set; }=false;
    bool _loading = false;
    [Parameter]  public EventCallback<IEnumerable<CmsCategoryEdit>> OnOK { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnCancel { get; set; }
    Form<CmsCategoryEdit> _Form;
    public CmsCategoryEdit _EditModel { get; set; }
    public SaveModel<CmsCategoryEdit> _SaveModel { get; set; }
    public string Id { get; set; }
    public SaveOperater Operater { get; set; } = SaveOperater.Add;

    public ConfigOption CmsCategoryGroup { get; set; }


    public  async Task OnInit()
    {
        _EditModel = new CmsCategoryEdit()
        {
            MessageInfo = ""
        };
        CmsCategoryGroup = Udx.Data.ConfigOptionData.CmsCategoryType;
        _SaveModel = new SaveModel<CmsCategoryEdit>()
        {
            Operater = Operater
        };
        await OnCmsCategoryInit(Id);
    }

    #region Search
    /// <summary>
    /// 模块查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task OnCmsCategoryInit(string id)
    {
        try
        {
             _loading = true;
             _EditModel = new CmsCategoryEdit()
            {

                MessageInfo = "请填写必填信息~"

            };
            if (SaveOperater.Edit == Operater && !string.IsNullOrEmpty(id))
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = id,
                    User = User
                };
                DataResponse<CmsCategoryModel> dataResponse = await DataService.GetAsync(request);
                if (dataResponse.Successful)
                {
                    _SaveModel = new SaveModel<CmsCategoryEdit>()
                    {
                        Operater = SaveOperater.Edit
                    };
                    _EditModel = dataResponse.ObjectData.Mapper<CmsCategoryEdit>();
                    _EditModel.MessageInfo = "请填写必填信息~";
                }
                else
                {

                    _EditModel.MessageInfo = dataResponse.Message;
                }
            }
            else
            {
                _EditModel.Id = System.Guid.NewGuid().ToString();
                _EditModel.ParentId = id;
                _SaveModel = new SaveModel<CmsCategoryEdit>()
                {
                    Operater = SaveOperater.Add
                };
            }
        }
        catch (Exception ex)
        {

            Log.Error(ex.Message);
            _EditModel.MessageInfo = ex.Message;
        }
        finally {
            _loading = false;
        }
        StateHasChanged();
    }
    
    #endregion

       
    async Task OnAddClickAsync()
        {
            await OnCmsCategoryInit(null);
         }
    private async Task OnSaveFailed(EditContext editContext)
    {
        await MessageBox.Error("保存失败!");
        StateHasChanged();
    }
    [Rule("保存")]
    private async Task OnSaveClick(EditContext editContext)
    {
        try
        {
            _loading = true;
            //主表验证
            if (!editContext.Validate())
            {
                _EditModel.MessageInfo = "数据未验证，请检查！";
                return;
            }

            _SaveModel.Model = _EditModel;
            var request = new DataRequest<SaveModel<CmsCategoryEdit>>()
            {
                ObjectData = _SaveModel,
                User = User
            };
            var dataResult = await DataService.SaveAsync(request);
            if (dataResult.Successful)
            {
                await MessageBox.Success("保存成功！", 5);
                //返回的Code是用户新增的Id，通过Id在查询一次
                Id = dataResult.Code;
                _EditModel.Id = Id;
                _EditModel.MessageInfo = "保存成功，继续点击保存将编辑！";
                _SaveModel.Operater = SaveOperater.Edit;
                Operater = SaveOperater.Edit;
            }
            else
            {
                await MessageBox.Error(dataResult.Message, 5);
                _EditModel.MessageInfo = dataResult.Message;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            await MessageBox.Error(ex.Message, 5);
            _EditModel.MessageInfo = ex.Message;
        }
        finally
        {
            _loading = false;

        }
    }
   
    [Rule("删除")]
    async Task OnDeteteClick()
    {
       
        try
        {
            _loading = true;
            
            var request = new DataRequest<SaveModel<CmsCategoryEdit>>()
            {
                ObjectData = new SaveModel<CmsCategoryEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = _EditModel.Mapper<CmsCategoryEdit>()
                },
                User = User
            };

            var result = await DataService.SaveAsync(request);
            Visible = false;
        }
        catch { }
        finally {
            _loading = false;
        }
        StateHasChanged();
    }
    [Rule("保存")]
    private void HandleOk(MouseEventArgs e)
    {
        _Form.Submit();
    }
    private void HandleCancel(MouseEventArgs e)
    {
        Visible=false;
        StateHasChanged();
    }   
    private void HandleSaveCancel(MouseEventArgs e)
    {
        _Form.Submit();
        Visible = false;
        StateHasChanged();
    }
}
