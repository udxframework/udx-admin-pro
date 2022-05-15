using AntDesign;
using System.Collections.Generic;

namespace Udx.Admin.App.Pages.Account.Settings
{
    public partial class Index
    {
        private readonly Dictionary<string, string> _menuMap = new Dictionary<string, string>
        {
            {nameof(BaseView), "基本设置"},
           // {nameof(BindingView), "账户绑定"},
            {nameof(ChangePwd), "修改密码"},
           // {nameof(NotificationView), "新消息通知"}

        }; 

        private string _selectKey = "BaseView";

        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }
    }
}