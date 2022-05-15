namespace Udx.Admin.Components
{
    public partial class UdxAddUser
    {

        [Parameter]
        public List<UserModel> Items { get; set; }

        IEnumerable<string> _selectedValues { get; set; }

        [Parameter]
        public string Users { get; set; } = "";

        [Parameter]
        public bool Bordered { get; set; } = true;


        [Parameter]
        public EventCallback<string> UsersChanged { get; set; }

        [Parameter]
        public EventCallback<string> ProductChanged { get; set; }

        protected override void OnInitialized()
        {
            _selectedValues = Users.Split(",").ToList();
            base.OnInitialized();
        }

        private void OnSelectedItemsChangedHandler(IEnumerable<UserModel> values)
        {
            if (values.Any())
                //await UsersChanged.InvokeAsync(_selectedValues.ToList());
                Users = string.Join(",", _selectedValues.ToList());
            UsersChanged.InvokeAsync(Users);
        }
    }
}
