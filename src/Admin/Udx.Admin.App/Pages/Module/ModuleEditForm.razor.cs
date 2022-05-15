using Microsoft.AspNetCore.Components.Web;

namespace Udx.Admin.App.Pages.Modules;
[ReuseTabsPageTitle("ģ�����")]
[Rule(Key = "admin.modules-list", Actions = new string[] { "����", "����", "ɾ��" })]
public partial class ModuleEditForm : UdxComponentBase
{
        [Inject] public IModuleService DataService { get; set; }
    public bool Visible { get; set; }=false;
    bool _loading = false;
    [Parameter]  public EventCallback<IEnumerable<ModuleEdit>> OnOK { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnCancel { get; set; }
    Form<ModuleEdit> _Form;
    public ModuleEdit _EditModel { get; set; }
    public SaveModel<ModuleEdit> _SaveModel { get; set; }
    public string Id { get; set; }
    public SaveOperater Operater { get; set; } = SaveOperater.Add;

    public ConfigOption ModuleType { get; set; }


    public  async Task OnInit()
    {
        ModuleType = Udx.Data.ConfigOptionData.ModuleType;
        _EditModel = new ModuleEdit()
        {
            MessageInfo = ""
        };
        _SaveModel = new SaveModel<ModuleEdit>()
        {
            Operater = Operater
        };
        await Init(Id);
    }

    #region Search
    /// <summary>
    /// ģ���ѯ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task Init(string id)
    {
        try
        {
             _loading = true;
             _EditModel = new ModuleEdit()
            {

                MessageInfo = "����д������Ϣ~"

            };
            if (SaveOperater.Edit == Operater && !string.IsNullOrEmpty(id))
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = id,
                    User = User
                };
                DataResponse<ModuleModel> dataResponse = await DataService.GetAsync(request);
                if (dataResponse.Successful)
                {
                    _SaveModel = new SaveModel<ModuleEdit>()
                    {
                        Operater = SaveOperater.Edit
                    };
                    _EditModel = dataResponse.ObjectData.Mapper<ModuleEdit>();
                    _EditModel.MessageInfo = "����д������Ϣ~";
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
                _SaveModel = new SaveModel<ModuleEdit>()
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
        await MessageBox.Error("����ʧ��!");
        StateHasChanged();
    }
    [Rule("����")]
    private async Task OnSaveClick(EditContext editContext)
    {
        try
        {
            _loading = true;
            //������֤
            if (!editContext.Validate())
            {
                _EditModel.MessageInfo = "����δ��֤�����飡";
                return;
            }

            _SaveModel.Model = _EditModel;
            var request = new DataRequest<SaveModel<ModuleEdit>>()
            {
                ObjectData = _SaveModel,
                User = User
            };
            var dataResult = await DataService.SaveAsync(request);
            if (dataResult.Successful)
            {
                await MessageBox.Success("����ɹ���", 5);
                //���ص�Code���û�������Id��ͨ��Id�ڲ�ѯһ��
                Id = dataResult.Code;
                _EditModel.Id = Id;
                _EditModel.MessageInfo = "����ɹ�������������潫�༭��";
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
   
    [Rule("ɾ��")]
    async Task OnDeteteClick()
    {
       
        try
        {
            _loading = true;
            
            var request = new DataRequest<SaveModel<ModuleEdit>>()
            {
                ObjectData = new SaveModel<ModuleEdit>()
                {
                    Operater = SaveOperater.Delete,
                    Model = _EditModel.Mapper<ModuleEdit>()
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
    [Rule("����")]
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
