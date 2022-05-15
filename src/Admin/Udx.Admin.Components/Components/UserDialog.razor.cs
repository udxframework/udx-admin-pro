using Udx.Admin.IServices;
using Udx.Admin.Models;

namespace Udx.Admin.Components
{
    partial class UserDialog
    {
        [Inject] public IUserService DataService { get; set; }

        SearchModel<List<FilterItem>> _SearchModel;
        QueryResponse<IEnumerable<UserModel>> _QueryResponse;

        IEnumerable<UserModel> selectedRows;
        UserModel selectedSingleRow;
        ITable table;

        string Value1;

        string Value2;

        async Task OnClick()
        {
            if (_SearchModel.FilterItems == null)
            {
                _SearchModel.FilterItems = new List<FilterItem>();
            }
            var t1 = _SearchModel.FilterItems.Find(p => p.Field == "UserName");
            if (t1 == null)
            {
                _SearchModel.FilterItems.Add(new FilterItem() { Field = nameof(UserModel.UserName), Caption = "用户名", OperatorSql = "=", Value = Value1 });
            }
            else
            {
                t1.Value = Value1;
            }

            var t2 = _SearchModel.FilterItems.Find(p => p.Field == "Name");
            if (t2 == null)
            {
                _SearchModel.FilterItems.Add(new FilterItem() { Field = nameof(UserModel.Name), Caption = "名字", OperatorSql = "=", Value = Value2 });
            }
            else
            {
                t2.Value = Value2;
            }
            await _SearchModel.SearchAction();
        }

        protected override async Task OnInitializedAsync()
        {
            _SearchModel = new SearchModel<List<FilterItem>>()
            {
                FilterItems = new List<FilterItem>()
            };
            if (this.Options != null)
            {
                _SearchModel.FilterItems.AddRange(this.Options.FilterItems);
            }

            _SearchModel.SearchAction = OnSearchAsync;

            _QueryResponse = new QueryResponse<IEnumerable<UserModel>>()
            {
                Data = new List<UserModel>()
            };
            await _SearchModel.SearchAction();
            await base.OnInitializedAsync();
        }


        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            var rt = new List<UserModel>();
            if (selectedRows != null)
            {
                rt = selectedRows.ToList();
            }
            await base.OkCancelRefWithResult!.OnOk(rt);
            await base.OnFeedbackOkAsync(args);
        }

        public async Task OnSearchAsync()
        {
            try
            {
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


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        void OnRowClick(AntDesign.TableModels.RowData<UserModel> selectedRow)
        {
            table.SetSelection(new string[] { selectedRow.Data.Id });
            SetSelection();
        }
        void SetSelection()
        {
            if (selectedRows != null && selectedRows.Any())
                selectedSingleRow = selectedRows.FirstOrDefault();
            else
            {
                selectedRows = null;
                selectedSingleRow = null;
            }
        }
    }
}
