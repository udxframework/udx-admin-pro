using Microsoft.AspNetCore.Components.Web;

namespace Udx.Admin.App.Pages.Orgs;
[ReuseTabsPageTitle("组织机构管理")]
[Rule(Key = "admin.orgs-list", Actions = new string[] { "新增", "保存", "删除" })]
public partial class OrgEditForm : UdxComponentBase
{
        [Inject] public IOrgService DataService { get; set; }
    public bool Visible { get; set; }=false;
    bool _loading = false;
    [Parameter]  public EventCallback<IEnumerable<OrgEdit>> OnOK { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnCancel { get; set; }
    Form<OrgEdit> _Form;
    public OrgEdit _EditModel { get; set; }
    public SaveModel<OrgEdit> _SaveModel { get; set; }
    public string Id { get; set; }
    public SaveOperater Operater { get; set; } = SaveOperater.Add;

  


    public  async Task OnInit()
    {
        _EditModel = new OrgEdit()
        {
            MessageInfo = ""
        };
        _SaveModel = new SaveModel<OrgEdit>()
        {
            Operater = Operater
        };
        await Init(Id);
    }

    #region Search
    /// <summary>
    /// 模块查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task Init(string id)
    {
        try
        {
             _loading = true;
             _EditModel = new OrgEdit()
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
                DataResponse<OrgModel> dataResponse = await DataService.GetAsync(request);
                if (dataResponse.Successful)
                {
                    _SaveModel = new SaveModel<OrgEdit>()
                    {
                        Operater = SaveOperater.Edit
                    };
                    _EditModel = dataResponse.ObjectData.Mapper<OrgEdit>();
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
                _SaveModel = new SaveModel<OrgEdit>()
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
            await Init(null);
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
            var request = new DataRequest<SaveModel<OrgEdit>>()
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
            
            var request = new DataRequest<SaveModel<OrgEdit>>()
            {
                ObjectData = new SaveModel<OrgEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = _EditModel.Mapper<OrgEdit>()
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
