using System.Net;

namespace Udx.Admin.App.Pages.Account
{

    public partial class Login: IDisposable
    {
        private  AuthLoginRequest _model = new AuthLoginRequest();
        [Inject] public IAuthService AccountService { get; set; }
        string ReturnUrl;
        bool Loading = false;
        string userStr = "";
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var query = new Uri(NavigationManager.Uri).Query;
            if (QueryHelpers.ParseQuery(query).TryGetValue("returnUrl", out var returnUrl))
            {
                ReturnUrl = WebUtility.UrlDecode(returnUrl);
            }       

        }
       
        public void Dispose()
        {
        }
        void OnItemUpdating(string fieldName, object newValue)
        {
            if (fieldName == nameof(AuthLoginRequest.UserName))
            {
                _model.UserName = newValue.ToString();
            }
            else if (fieldName == nameof(AuthLoginRequest.Password))
            {
                _model.Password = newValue.ToString();
            }           
        }
        public async void HandleInvalidSubmit() {
            _model.MessageInfo = "验证不通过";
            await Task.FromResult(0);
        }
       
        public async Task HandleSubmit()
        {
            Loading=true;
            try
            {

                await identityUserState.LogoutAsync();          
                var result = await AccountService.LoginAsync(_model);
                if (result.Successful)
                {
                    var user = result.ObjectData;
                    var authUser = user.Mapper<AuthUser>();
                    await identityUserState.LoginAsync(authUser);
                    if (string.IsNullOrEmpty(ReturnUrl))
                        NavigationManager.NavigateTo($"admin/home",true);
                    else
                        NavigationManager.NavigateTo(ReturnUrl,true);
                }
                else
                {
                    _model.MessageInfo = result.Message;
                }
            }
            catch (Exception ex) {
                _model.MessageInfo = ex.Message;
            }
            finally
            {
                Loading = false;
                base.StateHasChanged();
            }
        }
        public async Task GetUser() {
            var user = await identityUserState.GetAuthUser();
            userStr = user.ToJson();
            base.StateHasChanged();
        }
        public  void GotoRegister() {
            NavigationManager.NavigateTo("/account/register");
        }
    }
}