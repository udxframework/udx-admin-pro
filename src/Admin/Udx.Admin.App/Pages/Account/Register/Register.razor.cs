using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Udx.Admin.IServices;
using Udx.Admin.Models;

namespace Udx.Admin.App.Pages.Account
{

    public partial class Register
    {

        private readonly RegisterModel
            _model = new RegisterModel();

        [Inject] public IAuthService AuthService { get; set; }
        bool loading = false;
        public async void OnFinishFailed()
        {
            _model.MessageInfo = "验证不通过";
            await Task.FromResult(0);
            loading = false;
        }
        public async void OnFinish()
        {
            //loading=true;
            var request = new DataRequest<RegisterModel>() { ObjectData = _model };
            var result = await AuthService.RegisterAsync(request);
            if (result.Successful)
            {
                await MessageBox.Success("注册成功!请登录");
                NavigationManager.NavigateTo("/account/login");
            }
            else
            {
                await MessageBox.Error(result.Message);
                _model.MessageInfo = result.Message;
            }
            loading = false;
        }
        public void GotoLogin()
        {
            NavigationManager.NavigateTo("/account/login");
        }
    }
}