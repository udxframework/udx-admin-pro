namespace Udx.Cms.App.Pages.Contents;

    [ReuseTabsPageTitle("内容查看")]
    [Rule(Key = "cms.preview", Actions = new string[] { "预览"})]
    public partial class PreView : UdxComponentBase
{ 
   [Inject] public ICmsContentService DataService { get; set; }
    CmsContentResponse _cmsModel;
    [Parameter] public string Id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _cmsModel = new CmsContentResponse()
            {
                MessageInfo = "没有内容信息~"

            };
            await GetCmsContents(Id);

        }
   
    private async  Task GetCmsContents(string id)
        {
           
            try {                
                
                    var request = new DataRequest<string>()
                    {
                        ObjectData = id,
                        User = User
                    };
                    var dataResponse = await DataService.GetAsync(request);
                    if (dataResponse.Successful)
                    {
                        _cmsModel = dataResponse.ObjectData;
                        _cmsModel.MessageInfo = $"内容信息{System.DateTime.Now}";
                    }
                    else {

                        _cmsModel.MessageInfo = dataResponse.Message;
                    }
                
            }
            catch (Exception ex)
            {

                Log.Error("Exception:{@ex}",ex);
                _cmsModel.MessageInfo = ex.Message;
            }
            StateHasChanged();
        }
       
       
    }