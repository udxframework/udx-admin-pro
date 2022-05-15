namespace Udx.Admin.App.Pages.Account.Settings;

    public partial class BaseView:UdxComponentBase
    {
        private UserEditModel _currentUser;
        private  SaveModel<UserEditModel> _saveModel;

        [Inject] protected IUserService DataService { get; set; }

       

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                _currentUser = User.Mapper<UserEditModel>();
                _saveModel = new SaveModel<UserEditModel>()
                {
                    Operater = SaveOperater.Edit,
                    Model = User.Mapper<UserEditModel>()
                };
                await OnInit(User.Id);
            }
            catch (Exception ex)
            {
               Log.Debug("OnInitializedAsync,{@ex}", ex);
             }
        }

        private async Task OnInit(string id)
        {
            try
            {
              
                    var request = new DataRequest<string>()
                    {
                        ObjectData = id,
                        User = User
                    };
                    var dataResponse = await DataService.GetAsync(request);
                    if (dataResponse.Successful)
                    {
                        _saveModel = new SaveModel<UserEditModel>()
                        {
                            Operater = SaveOperater.Edit,
                            Model = dataResponse.ObjectData.Mapper<UserEditModel>()
                        };
                        _currentUser = _saveModel.Model;

                    }
                    else
                    {

                        _currentUser.MessageInfo = dataResponse.Message;
                    }
               
            }
            catch (Exception ex)
            {

                _currentUser.MessageInfo = ex.Message;
            }
            StateHasChanged();
        }
        private async Task OnFinish(EditContext editContext)
        {
            try
            {
                if (!editContext.Validate())
                {

                    _currentUser.MessageInfo = "����δ��֤�����飡";
                    return;
                }
                _saveModel.Model = _currentUser;
                var request = new DataRequest<SaveModel<UserEditModel>>()
                {
                    ObjectData = _saveModel,
                    User = User
                };
                var dataResult = await DataService.SaveAsync(request);
                if (dataResult.Successful)
                {

                    await MessageBox.Success("����ɹ���", 5);
                    //���ص�Code���û�������Id��ͨ��Id�ڲ�ѯһ��
                   var  id = dataResult.Code;
                    await OnInit(id);
                    _currentUser.MessageInfo = "����ɹ�";
                }
                else
                {
                    await MessageBox.Error(dataResult.Message, 5);
                    _currentUser.MessageInfo = dataResult.Message;
                }

            }
            catch (Exception ex)
            {

                await MessageBox.Error(ex.Message, 5);
                _currentUser.MessageInfo = ex.Message;
            }
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await MessageBox.Error("����ʧ��!");
        }

    }