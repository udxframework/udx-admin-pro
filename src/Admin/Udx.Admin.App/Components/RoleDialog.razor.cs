using Udx.Admin.App.Models;
using Udx.Admin.IServices;
using Udx.App.Models;
using Udx.Entites;
using Udx.Utils;

namespace Udx.Admin.App.Components
{

    partial class RoleDialog
    {
       
        [Inject] public IRuleService DataService { get; set; }

        
        IEnumerable<RoleUserModel> selectedRows;       

        List<RoleUserModel> Datas = new List<RoleUserModel>();

       

        protected override async Task OnInitializedAsync()
        {
           await base.OnInitializedAsync();
           await OnSearchAsync();
        }
        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }
            else if (FeedbackRef is ModalRef modalRef)
            {
                modalRef.Config.ConfirmLoading = true;
                await modalRef.UpdateConfigAsync();
            }
            var resut = new List<RoleUserModel>();
            if (selectedRows != null)
            {
                resut = selectedRows.ToList();
            }
            
            await base.OkCancelRefWithResult?.OkAsync(resut);
            await base.OnFeedbackOkAsync(args);
        }

        public async Task OnSearchAsync()
        {
            try
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = this.Options.Id,
                    User = User
                };
                var dataResult = await DataService.GetUserRolesAsync(request);
                if (dataResult.Successful)
                {
                    Datas = dataResult.ObjectData;
                    selectedRows = Datas.Where(s => s.Checked).ToList();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                 await MessageBox.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
