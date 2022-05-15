using System.Security.Cryptography;
using System.Text;

namespace Udx.Admin.App.Pages.Account.Settings
{
    public partial class ChangePwd : UdxComponentBase
    {
        [Inject] public IUserService DataService { get; set; }
        ChangePwdModel model { get; set; } = new();

        private async Task OnFinish(EditContext editContext)
        {
            if (editContext.Validate())
            {
                model.OldPwdMd5 = Encrypt32($"{ User.Id.ToLower()}-udx-{model.OldPassword}");
                model.PwdMd5 = Encrypt32($"{ User.Id.ToLower()}-udx-{model.Password}");
                var request = new DataRequest<ChangePwdModel>()
                {
                    ObjectData = model,
                    User = User
                };
                var result = await  DataService.ChangePwdAsync(request);
                if (result.Successful)
                {
                    await MessageBox.Success("修改成功！");
                }
                else
                {
                    await MessageBox.Error(result.Message);
                }
            }
        }

        public string Encrypt32(string password = "")
        {
            if (password.IsNull())
                return null;

#pragma warning disable CA1416 // 验证平台兼容性
            using (var md5 = MD5.Create())
#pragma warning restore CA1416 // 验证平台兼容性
            {
                string pwd = string.Empty;
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (var item in s)
                {
                    pwd = string.Concat(pwd, item.ToString("X"));
                }
                return pwd;
            }
        }


    }
}
