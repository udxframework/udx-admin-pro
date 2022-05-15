using Udx.Auth;

namespace Udx.Admin.App.Components
{

    partial class RoleModuleDialog
    {
       
        [Inject] public IRuleService DataService { get; set; }
        List<RoleModuleTree> Datas = new List<RoleModuleTree>();
        bool ModuleAllChecked = false;
        bool ActionAllChecked = false;
        bool Loading = false;

        protected override async Task OnInitializedAsync()
        {
           await base.OnInitializedAsync();
           await OnSearchAsync();
        }
        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Loading = true;
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
            var resut = new List<RoleModuleModel>();
            List<RoleModuleTree> GetModuleTree(List<RoleModuleTree> tree)
            {
                foreach (var row in tree)
                {
                    if(row.Checked){
                        resut.Add(new RoleModuleModel()
                        {
                            RoleId = this.Options.Id,
                            ModuleId = row.Id,
                            ModuleActions = row.GetRoleModuleActions(row.ActionList)
                        });
                    }
                    if (row.Items != null)
                        GetModuleTree(row.Items);
                }
                return tree;
            }
            GetModuleTree(Datas);
            await base.OkCancelRefWithResult?.OkAsync(resut);
            await base.OnFeedbackOkAsync(args);
            Loading = false;
        }
        
        public async Task OnSearchAsync()
        {
            Loading = true;
            try
            {
                var request = new DataRequest<string>()
                {
                    ObjectData = this.Options.Id,
                    User = User
                };
                var dataResult = await DataService.GetRoleModulesAsync(request);
                if (dataResult.Successful)
                {
                    Datas = dataResult.ObjectData;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                 await MessageBox.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
            Loading = false;
        }
        public void ModuleAllOnChange(RoleModuleTree item, object check)
        {
            var chk = (bool)check;
            List<RoleModuleTree> CheckedTree(List<RoleModuleTree> tree,bool ck)
            {
                foreach (var row in tree)
                {
                    row.Checked = ck;
                    if (row.Items != null)
                        CheckedTree(row.Items, ck);
                }
                return tree;
            }
            CheckedTree(Datas,chk);            
            StateHasChanged();
        }
        public void ModuleOnChange(RoleModuleTree item, object check)
        {
            item.Checked = (bool)check;
            item.ActionList.ForEach(s => s.Checked = item.Checked);
            StateHasChanged();
        }
        public void ActionAllOnChange(RoleModuleTree item, object check)
        {
            var chk = (bool)check;
            List<RoleModuleTree> CheckedTree(List<RoleModuleTree> tree, bool ck)
            {
                foreach (var row in tree)
                {
                    row.ActionList.ForEach(s=>s.Checked=ck);
                    if (row.Items != null)
                        CheckedTree(row.Items, ck);
                }
                return tree;
            }
            CheckedTree(Datas, chk);
            StateHasChanged();
        }
        public void ActionOnChange(RoleModuleTree roleitem, RuleAction item,object checkd)
        {
            item.Checked = (bool)checkd;
            if(roleitem.ActionList.Where(s => s.Checked).Any())
               roleitem.Checked= true;
            StateHasChanged();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
