namespace Udx.Cms.App.Components
{
    public partial class CmsPager
    {
        [Parameter] public SearchModel<List<FilterItem>> Model { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        private async void HandlePageChange(PaginationEventArgs args)
        {
            Model.CurrentPage = args.Page;
            Model.PageSize = args.PageSize;
            await Model.OnPageChange(Model.CurrentPage);
        }
        Func<PaginationTotalContext, string> showTotal = ctx => $"总共{ctx.Total}条,当前{ctx.Range.Item1}-{ctx.Range.Item2} ";
    }
}
