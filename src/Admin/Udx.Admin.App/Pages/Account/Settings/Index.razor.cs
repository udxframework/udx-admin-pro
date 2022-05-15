using AntDesign;
using System.Collections.Generic;

namespace Udx.Admin.App.Pages.Account.Settings
{
    public partial class Index
    {
        private readonly Dictionary<string, string> _menuMap = new Dictionary<string, string>
        {
            {nameof(BaseView), "��������"},
           // {nameof(BindingView), "�˻���"},
            {nameof(ChangePwd), "�޸�����"},
           // {nameof(NotificationView), "����Ϣ֪ͨ"}

        }; 

        private string _selectKey = "BaseView";

        private void SelectKey(MenuItem item)
        {
            _selectKey = item.Key;
        }
    }
}