using Udx.Admin.IServices;

namespace Udx.Admin.Components
{
    partial class UserDropdown: UdxComponentBase
    {
        [Inject] public IUserService DataService { get; set; }

        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<UserModel>> _QueryResponse;

        IEnumerable<UserModel> SelectedRows;

        string TxtValue = "";

        [Parameter] public string UserRole { get; set; } = "";

        [Parameter] public bool FilterUserRole { get; set; } =false;

        [Parameter] public string SelectType { get; set; } = "radio";

        [Parameter] public bool Visible { get; set; } = false;

        [Parameter]
        public EventCallback<bool> VisibleChanged { get; set; }

        [Parameter]
        public EventCallback<List<UserModel>> OnOk { get; set; }

       
        protected override async Task OnInitializedAsync()
        {
            _SearchModel = new SearchModel<List<FilterItem>>()
            {
                FilterItems = new List<FilterItem>()
            };

            _SearchModel.SearchAction = OnSearchAsync;

            await _SearchModel.SearchAction();
            await base.OnInitializedAsync();
        }

        public async Task OnSearchAsync()
        {
            try
            {
                var t1 = _SearchModel.FilterItems.Find(p => p.Field == "Name");
                if (t1 == null)
                {
                    _SearchModel.FilterItems.Add(new FilterItem() { Field = nameof(UserModel.Name), Caption = "名字", OperatorSql = "=", Value = TxtValue });
                }
                else
                {
                    t1.Value = TxtValue;
                }
                var request = new DataRequest<QueryModel>()
                {
                    ObjectData = new QueryModel()
                    {
                        CurrentPage = _SearchModel.CurrentPage,
                        PageSize = _SearchModel.PageSize,
                        FilterItems = _SearchModel.FilterItems
                    },
                    User = User
                };
                var dataResult = await DataService.PageQueryAsync(request);
                if (dataResult.Successful)
                {
                    _QueryResponse = dataResult.ObjectData.Mapper<QueryResponse<IEnumerable<UserModel>>>();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void HandleUserOk() 
        {
            if (SelectedRows == null && !SelectedRows.Any())
            {
                MessageBoxBase.Info("请选择人员");
                return;
            }
            OnOk.InvokeAsync(SelectedRows.ToList());
            Visible = false;
            VisibleChanged.InvokeAsync(Visible);
            StateHasChanged();
        }

        void OnCancle() 
        {
            Visible = false;
            VisibleChanged.InvokeAsync(Visible);
            StateHasChanged();
        }
    }
}
